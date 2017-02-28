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
    float hipstest = 1;

    float yrand = 0;
    float xrand = 0;
    float zrand = 0;
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
        float t_tempTransition = p_transition * 2;
        if (t_tempTransition > 1.0f)
        {
            t_tempTransition -= 1;
        }
        hipstest += 0.01f;
        float mod = (hipstest % (Mathf.PI * 2));
        if (mod < 0.01f || mod > ((Mathf.PI *2) -0.01f))
        {
            print("Urin!!!");
            yrand = Random.Range(0.5f, 1);
            xrand = Random.Range(0.5f, 1);
            zrand = Random.Range(0.5f, 1);
        }
        //if (m_switch)
        //{
        //    hipstest -= 0.01f;
        //}
        //else
        //{
        //    hipstest += 0.01f;
        //}
        //if (hipstest < -1)
        //{
        //    m_switch = !m_switch;
        //    hipstest = -1;
        //}
        //else if (hipstest > 1)
        //{
        //    m_switch = !m_switch;
        //    hipstest = 1;
        //}
        //print(t_tempTransition + " "+p_transition);
        for (int i = 0; i < t_bones.Length; i++)
        {
            //print(t_bones[i].name + " " + i);
            if (t_bones[i].name.Contains("RightHand"))
            {
                // This really shouldnt work. But for some reason a mistake made it look good and now we don't wanna change it
                if (t_tempTransition > 0.5f)
                {
                    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[1][i], p_poses[0][i], t_tempTransition / 0.5f);
                }
                else
                {
                    t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[0][i], p_poses[1][i], (t_tempTransition - 0.5f) / 0.5f);
                }
                //t_bones[i].rotation = SQUAD.Spline(p_poses[0][i], p_poses[1][i], p_poses[0][i], p_poses[1][i], p_transition).quat;
            }
            else
            {
                t_bones[i].rotation = SQUAD.Spline(p_poses[1][i], p_poses[0][i], p_poses[1][i], p_poses[0][i], p_transition).quat;
            }
        }
        //t_bones[0].rotation = t_bones[0].rotation *  Quaternion.AngleAxis(5 * Mathf.Sin(t_tempTransition*3.14f*2+(3.14f/2.0f)), new Vector3(0, 0, 1));
        //t_bones[1].rotation = t_bones[1].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(t_tempTransition*3.14f*2+(3.14f/2.0f)), new Vector3(0, 0, 1));
        //t_bones[5].rotation = t_bones[5].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(t_tempTransition*3.14f*2+(3.14f/2.0f)), new Vector3(0, 0, 1));
        t_bones[0].rotation = t_bones[0].rotation *  Quaternion.AngleAxis(5 * Mathf.Sin(hipstest), new Vector3(0, 0, 1));
        t_bones[1].rotation = t_bones[1].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest), new Vector3(0, 0, 1));
        t_bones[5].rotation = t_bones[5].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest), new Vector3(0, 0, 1));
        //t_bones[0].rotation = t_bones[0].rotation * Quaternion.AngleAxis(5 * Mathf.Sin(hipstest) * zrand, new Vector3(0, 0, 1));
        //t_bones[1].rotation = t_bones[1].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest)* zrand, new Vector3(0, 0, 1));
        //t_bones[5].rotation = t_bones[5].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest) * zrand, new Vector3(0, 0, 1));
        //t_bones[0].rotation = t_bones[0].rotation * Quaternion.AngleAxis(5 * Mathf.Sin(hipstest) * yrand, new Vector3(0, 1, 0));
        //t_bones[1].rotation = t_bones[1].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest) * yrand, new Vector3(0, 1,0));
        //t_bones[5].rotation = t_bones[5].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest) * yrand, new Vector3(0, 1 ,0));
        //t_bones[0].rotation = t_bones[0].rotation * Quaternion.AngleAxis(5 * Mathf.Sin(hipstest) * xrand, new Vector3(1, 0, 0));
        //t_bones[1].rotation = t_bones[1].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest) * xrand, new Vector3(1, 0, 0));
        //t_bones[5].rotation = t_bones[5].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest) * xrand, new Vector3(1, 0, 0));
        //t_bones[0].rotation = QuaternionExtensionsC.Add(t_bones[0].rotation,  Quaternion.AngleAxis(20 * hipstest, new Vector3(0, 0, 1)));
        m_prevTransition = p_transition;
        if (true) 
        {
            if (p_transition < 0.5f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[0], p_hipspos[1],
                    p_transition / 0.5f);
            }
           
            else if (p_transition < 1.0f)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[1], p_hipspos[0],
                    (p_transition - 0.5f) / 0.5f);
                
            }
        }
    }

}
