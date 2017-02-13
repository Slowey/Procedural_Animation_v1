using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDuck : MonoBehaviour
{
    float prevTrans = 0.0f;
    bool crouching = true;
    bool timeToChange = false;
    float timer = 0.0f;
    float timerTransition = 0.0f;
    public void UpdateAnimation(float p_transition, List<List<Quaternion>> p_poses, List<Vector3> p_hipspos)
    {
        GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
        float offset = 0f;
        float p_transitionExp1 = Mathf.Sqrt(p_transition/2);
        float p_transitionExp2 = 2 * Mathf.Pow(p_transition-0.5f, 2);

        if (!timeToChange)
        {
            timer += Time.deltaTime;
        }
        if (timer > 2.0f && !timeToChange)
        {
            timeToChange = true;
            timer = 0.0f;
        }
        
        if (p_transition < 0.5f && timeToChange && !crouching) //crouch
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].rotation = Quaternion.Slerp(p_poses[0][i], p_poses[1][i], p_transition / 0.5f);
                //t_bones[i].rotation = poses[1][i];
            }
            Vector3 test = Vector3.Slerp(p_hipspos[0], p_hipspos[1], p_transitionExp1 / 0.5f);
            t_bones[0].transform.position = new Vector3(test.x, test.y + offset, test.z);
        }
        if (prevTrans < 0.5f && p_transition > 0.5f && timeToChange && !crouching)
        {
            timeToChange = false;
            crouching = true;
        }
        else if (p_transition > 0.5f && timeToChange && crouching) //stand
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].rotation = Quaternion.Slerp(p_poses[1][i], p_poses[0][i], (p_transition - 0.5f) / 0.5f);
            }
            Vector3 test = Vector3.Slerp(p_hipspos[1], p_hipspos[0], (p_transitionExp2) / 0.5f);
            t_bones[0].transform.position = new Vector3(test.x, test.y+offset, test.z);
            //t_bones[0].transform.position = new Vector3(p_hipspos[1].x, p_hipspos[1].y, p_hipspos[1].z);
        }
        if (prevTrans > 0.9f && p_transition < 0.1f && timeToChange && crouching)
        {
            timeToChange = false;
            crouching = false;
        }
        prevTrans = p_transition;
    }

}