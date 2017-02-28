using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimIdle : MonoBehaviour {


    Animator m_animator;
    int m_IdleIdleHash = Animator.StringToHash("IdleIdle");
    int m_IdleExtendHash = Animator.StringToHash("IdleExtend");
    public float timeAdjuster = 5.733333333333333f;//2.866666666666667f*2.0f;
    float m_prevTransition = 0.0f;
    bool m_switch = false;

    // Use this for initialization
    public void InitAnim(Animator p_animator)
    {
        m_animator = p_animator;

    }

    public int GetIdleIdleHash()
    {
        return m_IdleIdleHash;
    }
    public int GetExtendHash()
    {
        return m_IdleExtendHash;
    }

    public void IdleUpdate(float p_transition, float p_prevTrans, List<List<Quaternion>> p_poses,
        List<Vector3> p_hipspos, bool p_headbob, GameObject p_gameObject)
    {
        GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 0.0f).eulerAngles);
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 1.0f).eulerAngles);
        
        for (int i = 0; i < t_bones.Length; i++)
        {
            if (t_bones[i].name.Contains("RightHand"))
            {
                if (p_transition < 0.25f)
                {
                    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[0][i], p_poses[1][i], (p_transition - 0.25f) / 0.25f);
                }
                else if(p_transition < 0.5f)
                {
                    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[1][i], p_poses[0][i], (p_transition - 0.5f)/0.25f);
                }
                else if(p_transition<0.75f)
                {
                    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[0][i], p_poses[1][i], (p_transition - 0.75f) / 0.25f);
                }
                else if (p_transition <1.0f)
                {
                    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[1][i], p_poses[0][i], (p_transition -1.0f)  / 0.25f);
                }
                //if (p_transition > 0.5f)
                //{
                //    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[0][i], p_poses[1][i], p_transition / 0.5f);
                //}
                //else
                //{
                //    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[1][i], p_poses[0][i], (p_transition-0.5f) / 0.5f);
                //}
                //t_bones[i].rotation = SQUAD.Spline(p_poses[0][i], p_poses[1][i], p_poses[0][i], p_poses[1][i], p_transition).quat;
            }
            else
            {
                t_bones[i].rotation = SQUAD.Spline(p_poses[1][i], p_poses[0][i], p_poses[1][i], p_poses[0][i], p_transition).quat;
            }
        }
        //print(p_transition);
        m_prevTransition = p_transition;
        if (true) 
        {
            if (p_transition < 0.5f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[0], p_hipspos[1],
                    p_transition / 0.5f);
            }
           
            //else if (p_transition < 0.5f)
            //{
            //    t_bones[0].position = Vector3.Slerp(p_hipspos[1], p_hipspos[2],
            //        (p_transition - 0.25f) / 0.25f);
            //    //print(poses[1][1].name + poses[1][1].transform.rotation.eulerAngles);
            //}
            //else if (p_transition < 0.75f)
            //{
            //    t_bones[0].position = Vector3.Slerp(p_hipspos[2], p_hipspos[3],
            //        (p_transition - 0.5f) / 0.25f);
            //}
            else if (p_transition < 1.0f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[1], p_hipspos[0],
                    (p_transition - 0.5f) / 0.5f);
            }
        }
    }

}
