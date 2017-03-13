using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDuck : MonoBehaviour
{
    float epsilon = 0.0001f;
    float prevTrans = 0.0f;
    bool crouching = true;
    float springDamperPos = 0.0f;
    float springDamperVel = 0.0f;
    float deltatimemodifier = 0.001f;
    public float timeAdjuster = 3.46666666667f;
    public void UpdateAnimation(float p_transition, List<List<Quaternion>> p_poses, List<Vector3> p_hipspos, float p_deltaTimeIncreaser, float p_angularVelocity, float p_damper)
    {
        GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();

        if (p_transition < 0.5f && !crouching) //Stand
        {
            deltatimemodifier = Mathf.Clamp(deltatimemodifier + (Time.deltaTime * 0.7f), 0.0f, 20.0f);
            SpringDamper(ref springDamperPos, ref springDamperVel, 1, deltatimemodifier * Time.deltaTime * p_deltaTimeIncreaser, p_angularVelocity, p_damper);
            int section = Mathf.FloorToInt(Mathf.Clamp(springDamperPos, 0.0000001f, 0.9999999f) * (p_poses.Count - 1));
            float alongLine = (p_poses.Count-1) * springDamperPos - section;
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].rotation = Quaternion.SlerpUnclamped(p_poses[section][i], p_poses[section+1][i], Mathf.Clamp(alongLine, 0, 2));
            }
            Vector3 test = Vector3.Slerp(p_hipspos[section], p_hipspos[section+1], Mathf.Clamp(alongLine, 0, 2));
            t_bones[0].transform.position = new Vector3(test.x, test.y, test.z);
        }
        if (prevTrans < 0.5f && p_transition > 0.5f && !crouching)
        {
            crouching = true;
            springDamperPos = 0.0f;
            deltatimemodifier = 0.001f;
        }
        else if (p_transition > 0.5f && crouching) //crouch
        {
            deltatimemodifier = Mathf.Clamp(deltatimemodifier + (Time.deltaTime * 0.7f), 0.0f, 20.0f);
            SpringDamper(ref springDamperPos, ref springDamperVel, 1, deltatimemodifier * Time.deltaTime * p_deltaTimeIncreaser, p_angularVelocity, p_damper);
            int section = Mathf.FloorToInt(Mathf.Clamp(springDamperPos, 0.0000001f, 0.9999999f) * (p_poses.Count - 1));
            float alongLine = (p_poses.Count - 1) * springDamperPos - section;
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].rotation = Quaternion.SlerpUnclamped(p_poses[p_poses.Count-1-section][i], p_poses[p_poses.Count-2-section][i], Mathf.Clamp(alongLine, 0, 2));
            }
            Vector3 test2 = Vector3.SlerpUnclamped(p_hipspos[p_poses.Count - 1 - section], p_hipspos[p_poses.Count - 2 - section], Mathf.Clamp(alongLine, 0, 2));
            t_bones[0].transform.position = new Vector3(test2.x, test2.y, test2.z);
        }
        if (prevTrans > 0.9f && p_transition < 0.1f && crouching)
        {
            crouching = false;
            springDamperPos = 0.0f;
            deltatimemodifier = 0.001f;
        }
        prevTrans = p_transition;
    }

    /******************************************************************************
        Copyright (c) 2008-2012 Ryan Juckett
        http://www.ryanjuckett.com/

        This software is provided 'as-is', without any express or implied
        warranty. In no event will the authors be held liable for any damages
        arising from the use of this software.

        Permission is granted to anyone to use this software for any purpose,
        including commercial applications, and to alter it and redistribute it
        freely, subject to the following restrictions:

        1. The origin of this software must not be misrepresented; you must not
           claim that you wrote the original software. If you use this software
           in a product, an acknowledgment in the product documentation would be
           appreciated but is not required.

        2. Altered source versions must be plainly marked as such, and must not be
           misrepresented as being the original software.

        3. This notice may not be removed or altered from any source
           distribution.
        ******************************************************************************/
    // This method is altered from Ryan Junckett's original implementation

    //******************************************************************************
    // CalcDampedSimpleHarmonicMotion
    // This function will update the supplied position and velocity values over
    // the given time according to the spring parameters.
    // - An angular frequency is given to control how fast the spring oscillates.
    // - A damping ratio is given to control how fast the motion decays.
    //     damping ratio > 1: over damped
    //     damping ratio = 1: critically damped
    //     damping ratio < 1: under damped
    //******************************************************************************
    void SpringDamper(ref float pPos,            // position value to update
            ref float pVel,            // velocity value to update
            float equilibriumPos,   // position to approach
            float deltaTime,        // time to update over
            float angularFrequency, // angular frequency of motion
            float dampingRatio      // damping ratio of motion
        )
    {

        // if there is no angular frequency, the spring will not move
        if (angularFrequency < epsilon)
            return;

        // clamp the damping ratio in legal range
        if (dampingRatio < 0.0f)
            dampingRatio = 0.0f;

        // calculate initial state in equilibrium relative space
        float initialPos = pPos - equilibriumPos;
        float initialVel = pVel;

        // if over-damped
        if (dampingRatio > 1.0f + epsilon)
        {
            // calculate constants based on motion parameters
            // Note: These could be cached off between multiple calls using the same
            //       parameters for deltaTime, angularFrequency and dampingRatio.
            float za = -angularFrequency * dampingRatio;
            float zb = angularFrequency * Mathf.Sqrt(dampingRatio * dampingRatio - 1.0f);
            float z1 = za - zb;
            float z2 = za + zb;
            float expTerm1 = Mathf.Exp(z1 * deltaTime);  //Lite osäker om detta stämmer
            float expTerm2 = Mathf.Exp(z2 * deltaTime);  //Lite osäker om detta stämmer

            // update motion
            float c1 = (initialVel - initialPos * z2) / (-2.0f * zb); // z1 - z2 = -2*zb
            float c2 = initialPos - c1;
            pPos = equilibriumPos + c1 * expTerm1 + c2 * expTerm2;
            pVel = c1 * z1 * expTerm1 + c2 * z2 * expTerm2;
        }
        // else if critically damped
        else if (dampingRatio > 1.0f - epsilon)
        {
            // calculate constants based on motion parameters
            // Note: These could be cached off between multiple calls using the same
            //       parameters for deltaTime, angularFrequency and dampingRatio.
            float expTerm = Mathf.Exp(-angularFrequency * deltaTime);

            // update motion
            float c1 = initialVel + angularFrequency * initialPos;
            float c2 = initialPos;
            float c3 = (c1 * deltaTime + c2) * expTerm;
            pPos = equilibriumPos + c3;
            pVel = (c1 * expTerm) - (c3 * angularFrequency);
        }
        // else under-damped
        else
        {
            // calculate constants based on motion parameters
            // Note: These could be cached off between multiple calls using the same
            //       parameters for deltaTime, angularFrequency and dampingRatio. 1234 Kan lägga dessa i classen
            float omegaZeta = angularFrequency * dampingRatio;
            float alpha = angularFrequency * Mathf.Sqrt(1.0f - dampingRatio * dampingRatio);
            float expTerm = Mathf.Exp(-omegaZeta * deltaTime);
            float cosTerm = Mathf.Cos(alpha * deltaTime);
            float sinTerm = Mathf.Sin(alpha * deltaTime);

            // update motion
            float c1 = initialPos;
            float c2 = (initialVel + omegaZeta * initialPos) / alpha;
            pPos = equilibriumPos + expTerm * (c1 * cosTerm + c2 * sinTerm);
            pVel = -expTerm * ((c1 * omegaZeta - c2 * alpha) * cosTerm +
                                       (c1 * alpha + c2 * omegaZeta) * sinTerm);
        }

    }
}