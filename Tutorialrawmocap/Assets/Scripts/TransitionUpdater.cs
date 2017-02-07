﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransitionUpdater : MonoBehaviour
{
    Animator m_animator;
    float m_transition;
    float m_prevTrans;
    int m_extendHash = Animator.StringToHash("WalkFWD_Extend");
    int m_extendMirrorHash = Animator.StringToHash("WalkFWD_Extend_Mirror");
    int m_crossHash = Animator.StringToHash("WalkFWD_Cross");
    int m_crossMirrorHash = Animator.StringToHash("WalkFWD_Cross_Mirror");
    //List<List<Transform>> poses = new List<List<Transform>>();
    List<List<Quaternion>> poses = new List<List<Quaternion>>();
    List<List<Vector3>> positions = new List<List<Vector3>>();
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_transition = 0.0f;

        Invoke("SaveKeyFrames0", 0.25f);
        Invoke("SaveKeyFrames1", 0.5f);
        Invoke("SaveKeyFrames2", 0.75f);
        Invoke("SaveKeyFrames3", 1.0f);
        //m_animator.Play(m_crossHash, 0, 0);
        //m_animator.Update(0.0f);
        ////Provar med bara rotations. Lägg till Positions också ifall vi har deformations
        //t_hips = GameObject.Find("Hips");
        //t_transforms = t_hips.GetComponentsInChildren<Transform>();
        //for (int i = 0; i < t_transforms.Length; i++)
        //{
        //    print(t_transforms[i].name + " " + t_transforms[i].rotation.eulerAngles);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        m_prevTrans = m_transition;
        m_transition += Time.deltaTime;
        if (m_transition > 1.0f)
        {
            m_transition -= 1.0f;
        }
        if (poses.Count > 3)
        {
            UpdateAnimationASD();
        }
        else
            m_transition = 0;

    }
    void UpdateAnimationASD()
    {
        GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 0.0f).eulerAngles);
        //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 1.0f).eulerAngles);

        for (int i = 0; i < t_bones.Length; i++)
        {
            if (i == 2 && m_transition > 0.5f && m_prevTrans < 0.5f)
            {
                //print(t_bones[i].rotation.eulerAngles);
                //testStruct d = new  testStruct();
                SQUAD.testStruct d = new SQUAD.testStruct();
                d = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_prevTrans);
                print(d.alongLine);
                d = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_transition);
                t_bones[i].rotation = d.quat;
                print(d.alongLine);
            }
            else
            {
                t_bones[i].rotation = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_transition).quat;
            }
        }

        //for (int i = 0; i < t_bones.Length; i++)
        //{
        //    if (i == 2 && m_transition > 0.5f && m_prevTrans < 0.5f)
        //    {
        //        print(t_bones[i].rotation.eulerAngles);
        //        t_bones[i].rotation = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_transition);
        //        print(t_bones[i].rotation.eulerAngles);
        //    }
        //    else
        //        t_bones[i].rotation = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_transition);
        //}
        // if (m_transition < 0.25f)
        // {
        //     for (int i = 0; i < t_bones.Length; i++)
        //     {
        //         t_bones[i].position = Vector3.Slerp(positions[0][i], positions[1][i],
        //          m_transition / 0.25f);
        //     }
        // }
        // 
        // else if (m_transition < 0.5f)
        // {
        //     for (int i = 0; i < t_bones.Length; i++)
        //     {
        //         t_bones[i].position = Vector3.Slerp(positions[1][i], positions[2][i],
        //             (m_transition - 0.25f) / 0.25f);
        //     }
        //     //print(poses[1][1].name + poses[1][1].transform.rotation.eulerAngles);
        // }
        // else if (m_transition < 0.75f)
        // {
        //     for (int i = 0; i < t_bones.Length; i++)
        //     {
        //         
        //         t_bones[i].position = Vector3.Slerp(positions[2][i], positions[3][i],
        //             (m_transition - 0.5f) / 0.25f);
        //     }
        // }
        // else if (m_transition < 1.0f)
        // {
        //     for (int i = 0; i < t_bones.Length; i++)
        //     {                    
        //         t_bones[i].position = Vector3.Slerp(positions[3][i], positions[0][i],
        //             (m_transition - 0.75f) / 0.25f);
        //     }
        // }
    }


    void SaveKeyFrames0()
    {
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_extendHash, 0, 0);
        m_animator.Update(0.001f);
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        List<Vector3> t_positions = new List<Vector3>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
            t_positions.Add(new Vector3(t_transforms[i].position.x, t_transforms[i].position.y, t_transforms[i].position.z));
        }
        poses.Add(t_quaternions);
        positions.Add(t_positions);
    }
    void SaveKeyFrames1()
    {
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_crossHash, 0, 0);
        m_animator.Update(0.001f);
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        List<Vector3> t_positions = new List<Vector3>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
            t_positions.Add(new Vector3(t_transforms[i].position.x, t_transforms[i].position.y, t_transforms[i].position.z));
        }
        poses.Add(t_quaternions);
        positions.Add(t_positions);

    }
    void SaveKeyFrames2()
    {
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_extendMirrorHash, 0, 0);
        m_animator.Update(0.001f);
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        List<Vector3> t_positions = new List<Vector3>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
            t_positions.Add(new Vector3(t_transforms[i].position.x, t_transforms[i].position.y, t_transforms[i].position.z));
        }
        poses.Add(t_quaternions);
        positions.Add(t_positions);
    }
    void SaveKeyFrames3()
    {
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_crossMirrorHash, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        List<Vector3> t_positions = new List<Vector3>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
            t_positions.Add(new Vector3(t_transforms[i].position.x, t_transforms[i].position.y, t_transforms[i].position.z));
        }
        poses.Add(t_quaternions);
        positions.Add(t_positions);
        print(poses[0][1].eulerAngles);
        print(poses[1][1].eulerAngles);
        print(poses[2][1].eulerAngles);
        print(poses[3][1].eulerAngles);
        GameObject hello = new GameObject();
        hello.name = "hello";
        GameObject rot1 = new GameObject();
        rot1.transform.rotation = poses[0][1];
        rot1.transform.parent = hello.transform;
        GameObject rot2 = new GameObject();
        rot2.transform.rotation = poses[1][1];
        rot2.transform.parent = hello.transform;

        GameObject rot3 = new GameObject();
        rot3.transform.rotation = poses[2][1];
        rot3.transform.parent = hello.transform;

        GameObject rot4 = new GameObject();
        rot4.transform.rotation = poses[3][1];
        rot4.transform.parent = hello.transform;
    }
}