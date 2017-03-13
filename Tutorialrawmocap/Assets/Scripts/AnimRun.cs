﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRun : MonoBehaviour
{
    
    int m_extendHash = Animator.StringToHash("RunExtend");
    int m_crossHash = Animator.StringToHash("RunCross");
    int m_extendMirrorHash = Animator.StringToHash("RunExtendMirrored");
    int m_crossMirrorHash = Animator.StringToHash("RunCrossMirrored");
    public float timeAdjuster = 0.7f;
    
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

        for (int i = 0; i < t_bones.Length; i++)
        {
            List<Quaternion> t_quatList = new List<Quaternion>();
            for (int j = 0; j < p_poses.Count; j++)
            {
                t_quatList.Add(p_poses[j][i]);
            }
            t_bones[i].rotation = SQUAD.SplineFromListLoop(t_quatList, p_transition);
        }

        //if (!p_headbob)
        //{
        //    if (p_transition < 0.25f)
        //    {
        //        t_bones[0].position = Vector3.Slerp(p_hipspos[0], p_hipspos[1],
        //            p_transition / 0.25f);
        //    }
        //
        //    else if (p_transition < 0.5f)
        //    {
        //        t_bones[0].position = Vector3.Slerp(p_hipspos[1], p_hipspos[2],
        //            (p_transition - 0.25f) / 0.25f);
        //    }
        //    else if (p_transition < 0.75f)
        //    {
        //        t_bones[0].position = Vector3.Slerp(p_hipspos[2], p_hipspos[3],
        //            (p_transition - 0.5f) / 0.25f);
        //    }
        //    else if (p_transition < 1.0f)
        //    {
        //        t_bones[0].position = Vector3.Slerp(p_hipspos[3], p_hipspos[0],
        //            (p_transition - 0.75f) / 0.25f);
        //    }
        //}
        //else
        //{
        //    HeadbobUpdate(p_transition, p_gameObject);
        //}
        if (!p_headbob)
        {

            int t_listSize = p_hipspos.Count;
            int section = (int)Mathf.Floor((t_listSize) * p_transition);
            float alongLine = (t_listSize) * p_transition - section;
            if (section == t_listSize - 1)
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[section], p_hipspos[0], alongLine);
            }
            else
            {
                t_bones[0].position = Vector3.Slerp(p_hipspos[section], p_hipspos[section + 1], alongLine);
            }
        }
        else
        {
            HeadbobUpdate(p_transition, p_gameObject); //Vet inte om funkar! Har modifierat !p_headbob för att passa fler keyframes
        }
    }
    public void HeadbobUpdate(float p_transition, GameObject p_gameObject)
    {
        if (p_transition > 0.25 && p_transition < 0.75f)
        {
            float ourX = ((p_transition - 0.25f) * 4) - 1;
            float ourXTwo = (0.5f * ourX) * -(0.5f * ourX) + 0.25f;
            p_gameObject.transform.position = new Vector3(0, ourXTwo * 0.1f, 0);
        }
        else if (p_transition < 0.25f)
        {
            float ourX = ((p_transition + 0.25f) * 4) - 1;
            float ourXTwo = (0.5f * ourX) * -(0.5f * ourX) + 0.25f;
            p_gameObject.transform.position = new Vector3(0, ourXTwo * 0.1f, 0);
        }
        else if (p_transition > 0.75f)
        {
            float ourX = ((p_transition - 0.75f) * 4) - 1;
            float ourXTwo = (0.5f * ourX) * -(0.5f * ourX) + 0.25f;
            p_gameObject.transform.position = new Vector3(0, ourXTwo * 0.1f, 0);
        }
    }
}
