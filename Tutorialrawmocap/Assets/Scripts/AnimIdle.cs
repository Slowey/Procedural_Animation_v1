using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimIdle : MonoBehaviour {

    
    int m_IdleIdleHash = Animator.StringToHash("IdleIdle");
    int m_IdleExtendHash = Animator.StringToHash("IdleExtend");
    int m_IdleBetween1_1 = Animator.StringToHash("IdleBetween1_1");
    int m_IdleBetween2_1 = Animator.StringToHash("IdleBetween2_1");
    int m_IdleBetween2_2 = Animator.StringToHash("IdleBetween2_2");
    public float timeAdjuster = 5.733333333333333f;// / 4;//2.866666666666667f*2.0f;
    bool m_switch = true;
    float hipstest = 1;
    int m_frames = 2;
    // Use this for initialization
    
    public void ChangeKeyFrames(int p_nrKeyFrames)
    {
        if(p_nrKeyFrames == 0)
        {
            SetFrames(p_nrKeyFrames);
            SetTimeAdjuster(5.733333333333333f);
        }
        else if(p_nrKeyFrames == 2)
        {
            SetFrames(p_nrKeyFrames);
            SetTimeAdjuster(5.733333333333333f);// * 0.25f);
        }
        else if (p_nrKeyFrames == 1)
        {
            SetFrames(p_nrKeyFrames);
            SetTimeAdjuster(5.733333333333333f);// * 0.25f);
        }

    }
    public void SetTimeAdjuster(float p_newTimeAdjuster)
    {
        timeAdjuster = p_newTimeAdjuster;
    }
    public void SetFrames(int p_framesToAdd)
    {
        m_frames = p_framesToAdd + 2;
    }
    public int GetIdleIdleHash()
    {
        return m_IdleIdleHash;
    }
    public int GetExtendHash()
    {
        return m_IdleExtendHash;
    }
    public int GetIdleInbetweenOneOne()
    {
        return m_IdleBetween1_1;
    }
    public int GetIdleInbetweenTwoOne()
    {
        return m_IdleBetween2_1;
    }
    public int GetIdleInbetweenTwoTwo()
    {
        return m_IdleBetween2_2;
    }
    public void IdleUpdate(float p_transition, float p_prevTrans, List<List<Quaternion>> p_poses,
        List<Vector3> p_hipspos, bool p_headbob, GameObject p_gameObject)
    {
        GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
        List<List<Quaternion>> t_poses = new List<List<Quaternion>>();
        List<Vector3> t_hipspos = new List<Vector3>();
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 0.0f).eulerAngles);
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 1.0f).eulerAngles);
        float t_tempTransition = p_transition* 2;
        float t_tempTransitionNew = p_transition + 0.5f;
        float t_prevTransNew = p_prevTrans + 0.5f;
        if (t_tempTransition > 1.0f)
        {
            t_tempTransition -= 1;
        }
        //if (p_transition < 0.1f && p_prevTrans > 0.9f)
        //{
        //    m_switch = !m_switch;
        //}   
       

        if (p_transition > 1)
        {
            p_transition -= 1.0f;
        }
        if ((p_transition < 0.1f && p_prevTrans > 0.9f) || (p_transition > 0.25f && p_prevTrans < 0.25f) || (p_transition > 0.5f && p_prevTrans < 0.5f) || (p_transition > 0.75f && p_prevTrans < 0.75f))
        {
            m_switch = !m_switch;
        }
        if (m_frames != 2)
        {

            //if ((p_transition < 0.1f && p_prevTrans > 0.9f) || (p_transition > 0.25f && p_prevTrans < 0.25f) || (p_transition > 0.5f && p_prevTrans < 0.5f) || (p_transition > 0.75f && p_prevTrans < 0.75f))
            //{
            //    m_switch = !m_switch;
            //}

            p_transition = p_transition * 4.0f;
            if (p_transition > 1.0f)
            {
                p_transition -= 1;
            }
            if (p_transition > 1.0f)
            {
                p_transition -= 1;
            }
            if (p_transition > 1.0f)
            {
                p_transition -= 1;
            }
            if (p_transition > 1.0f)
            {
                p_transition -= 1;
            }
        }

        if (m_switch)
        {
            for (int i = 0; i < p_poses.Count; i++)
            {
                t_poses.Add(p_poses[p_poses.Count-1- i]);
                t_hipspos.Add(p_hipspos[p_hipspos.Count - 1 - i]);
            }
        }
        else
        {
            for (int i = 0; i < p_poses.Count; i++)
            {
                t_poses.Add(p_poses[i]);
                t_hipspos.Add(p_hipspos[i]);
            }
        }
        hipstest += 0.01f;
        //float mod = (hipstest % (Mathf.PI * 2));
        //if (mod < 0.01f || mod > ((Mathf.PI *2) -0.01f))
        //{
        //    yrand = Random.Range(0.5f, 1);
        //    xrand = Random.Range(0.5f, 1);
        //    zrand = Random.Range(0.5f, 1);
        //}
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
        int poseListSize = p_poses.Count;
        if (m_frames != 2)
        {
        //print(m_frames + "inte 2");
            List<Quaternion> t_qList = new List<Quaternion>();
           //p_transition += 0.25f;
           //if (p_transition > 1)
           //{
           //    p_transition -= 1.0f;
           //}

            for (int i = 0; i < t_bones.Length; i++)
            {
                //print(t_bones[i].name + " " + i);
                //if (t_bones[i].name.Contains("RightHand"))
                //{
                //    // This really shouldnt work. But for some reason a mistake made it look good and now we don't wanna change it
                //    if (t_tempTransition > 0.5f)
                //    {
                //        t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[1][i], p_poses[0][i], t_tempTransition / 0.5f);
                //    }
                //    else
                //    {
                //        t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[0][i], p_poses[1][i], (t_tempTransition - 0.5f) / 0.5f);
                //    }
                //    //t_bones[i].rotation = SQUAD.Spline(p_poses[0][i], p_poses[1][i], p_poses[0][i], p_poses[1][i], p_transition).quat;
                //}
               // else
                {

                    for (int k = 0; k < poseListSize; k++)
                    {
                        t_qList.Add(t_poses[k][i]);
                    }
                    //print(t_qList.Count);
                    //print(SQUAD.SplineMoreThanFivePoints(t_qList, p_transition).m_section); //SQUAD.Spline(p_poses[0][i], p_poses[1][i], p_poses[2][i], p_poses[3][i], p_transition).quat;
                    t_bones[i].rotation = SQUAD.SplineMoreThanFivePoints(t_qList, p_transition).quat; //SQUAD.Spline(p_poses[0][i], p_poses[1][i], p_poses[2][i], p_poses[3][i], p_transition).quat;
                    t_qList.Clear();
                }
            }
        }

        if (m_frames == 2)
        {
            //p_transition += 0.5f;
            //if(p_transition>1)
            //{
            //    p_transition -= 1.0f;
            //}
            for (int i = 0; i < t_bones.Length; i++)
            {
                //print(t_bones[i].name + " " + i);
                if (t_bones[i].name.Contains("RightHand"))
                {
                    // This really shouldnt work. But for some reason a mistake made it look good and now we don't wanna change it
                    if (t_tempTransition > 0.5f)
                    {

                        t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[poseListSize-1][i], p_poses[0][i], t_tempTransition / 0.5f);
                    }
                    else
                    {
                        t_bones[i].rotation = QuaternionExtensionsC.SlerpNoInvertForceShortWay(p_poses[0][i], p_poses[poseListSize-1][i], (t_tempTransition - 0.5f) / 0.5f);
                    }
                    //t_bones[i].rotation = SQUAD.Spline(p_poses[0][i], p_poses[1][i], p_poses[0][i], p_poses[1][i], p_transition).quat;
                }
                else
                {
                    t_bones[i].rotation = SQUAD.Spline(p_poses[1][i], p_poses[0][i], p_poses[1][i], p_poses[0][i], p_transition).quat;
                }
            }
        }


        //t_bones[0].rotation = t_bones[0].rotation *  Quaternion.AngleAxis(5 * Mathf.Sin(t_tempTransition*3.14f*2+(3.14f/2.0f)), new Vector3(0, 0, 1));
        //t_bones[1].rotation = t_bones[1].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(t_tempTransition*3.14f*2+(3.14f/2.0f)), new Vector3(0, 0, 1));
        //t_bones[5].rotation = t_bones[5].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(t_tempTransition*3.14f*2+(3.14f/2.0f)), new Vector3(0, 0, 1));
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



        /// added skön effekt
        //t_bones[0].rotation = t_bones[0].rotation *  Quaternion.AngleAxis(5 * Mathf.Sin(hipstest), new Vector3(0, 0, 1));
        //t_bones[1].rotation = t_bones[1].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest), new Vector3(0, 0, 1));
        //t_bones[5].rotation = t_bones[5].rotation * Quaternion.AngleAxis(-5 * Mathf.Sin(hipstest), new Vector3(0, 0, 1));
        //m_prevTransition = p_transition;
        if (true)
        {
            //if (p_transition < 0.5f)
            //{
            //    t_bones[0].position = Vector3.Slerp(p_hipspos[0], p_hipspos[1],
            //        p_transition / 0.5f);
            //}
            //else if (p_transition < 1.0f)
            //{
            //    t_bones[0].position = Vector3.Slerp(p_hipspos[1], p_hipspos[0],
            //        (p_transition - 0.5f) / 0.5f);
            //
            //}
            //int section = (int)Mathf.Floor((t_hipspos.Count - 1) * p_transition);
            //float alongLine = (t_hipspos.Count - 1) * p_transition - section;
            //t_bones[0].position = Vector3.Lerp(t_hipspos[section], t_hipspos[section + 1],
            //    alongLine);
            //
            
            //t_bones[0].position = new Vector3(, t_bones[0].position.y, t_bones[0].position.z);
        }
    }

}
