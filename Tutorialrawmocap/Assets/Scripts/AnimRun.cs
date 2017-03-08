using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRun : MonoBehaviour
{

    Animator m_animator;
    int m_extendHash = Animator.StringToHash("RunExtend");
    int m_extendMirrorHash = Animator.StringToHash("RunExtendMirrored");
    int m_crossHash = Animator.StringToHash("RunCross");
    int m_crossMirrorHash = Animator.StringToHash("RunCrossMirrored");
    public float timeAdjuster = 0.7f;

    // Use this for initialization
    public void InitAnim(Animator p_animator)
    {
        m_animator = p_animator;

    }
    //public void LoadKeyFramesHashValues()
    //{

    //    //o_extend = m_extendHash;
    //    //o_extendMirror = m_extendMirrorHash;
    //    //o_cross = m_crossHash;
    //    //o_crossMirrored = m_crossMirrorHash;
    //}
    public int GetExtendHash()
    {
        return m_extendHash;
    }
    public int GetExtendMirrorHash()
    {
        return m_extendMirrorHash;
    }
    public int GetCrossHash()
    {
        return m_crossHash;
    }
    public int GetCrossMirrorHash()
    {
        return m_crossMirrorHash;
    }
    public void RunningUpdate(float p_transition, float p_prevTrans, List<List<Quaternion>> p_poses,
        List<Vector3> p_hipspos, bool p_headbob, GameObject p_gameObject)
    {
        GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 0.0f).eulerAngles);
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 1.0f).eulerAngles);

        for (int i = 0; i < t_bones.Length; i++)
        {
            List<Quaternion> t_quatList = new List<Quaternion>();
            for (int j = 0; j < p_poses.Count; j++)
            {
                t_quatList.Add(p_poses[j][i]);
            }
            t_bones[i].rotation = SQUAD.SplineFromListLoop(t_quatList, p_transition);
            //t_bones[i].rotation = SQUAD.Spline(p_poses[0][i], p_poses[1][i], p_poses[2][i], p_poses[3][i], p_transition).quat;
        }
        if (!p_headbob)
        {
            if (p_transition < 0.25f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[0], p_hipspos[1],
                    p_transition / 0.25f);
            }

            else if (p_transition < 0.5f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[1], p_hipspos[2],
                    (p_transition - 0.25f) / 0.25f);
                //print(poses[1][1].name + poses[1][1].transform.rotation.eulerAngles);
            }
            else if (p_transition < 0.75f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[2], p_hipspos[3],
                    (p_transition - 0.5f) / 0.25f);
            }
            else if (p_transition < 1.0f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[3], p_hipspos[0],
                    (p_transition - 0.75f) / 0.25f);
            }
        }
        else
        {
            HeadbobUpdate(p_transition, p_gameObject);
        }
    }
    public void HeadbobUpdate(float p_transition, GameObject p_gameObject)
    {
        if (p_transition > 0.25 && p_transition < 0.75f)
        {
            float ourX = ((p_transition - 0.25f) * 4) - 1;
            float ourXTwo = (0.5f * ourX) * -(0.5f * ourX) + 0.25f;
            // float theX = ourXTwo
            p_gameObject.transform.position = new Vector3(0, ourXTwo * 0.1f, 0);
        }
        else if (p_transition < 0.25f)
        {
            float ourX = ((p_transition + 0.25f) * 4) - 1;
            float ourXTwo = (0.5f * ourX) * -(0.5f * ourX) + 0.25f;
            // float theX = ourXTwo
            p_gameObject.transform.position = new Vector3(0, ourXTwo * 0.1f, 0);
            //print(ourX + "under .25");
        }
        else if (p_transition > 0.75f)
        {
            float ourX = ((p_transition - 0.75f) * 4) - 1;
            float ourXTwo = (0.5f * ourX) * -(0.5f * ourX) + 0.25f;
            // float theX = ourXTwo
            p_gameObject.transform.position = new Vector3(0, ourXTwo * 0.1f, 0);
            //print(ourX + "över .75");
        }
    }
}
