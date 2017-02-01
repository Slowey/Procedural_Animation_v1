 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionUpdater : MonoBehaviour {
    Animator m_animator;
    float m_transition;
    int m_extendHash = Animator.StringToHash("WalkFWD_Extend");
    int m_extendMirrorHash = Animator.StringToHash("WalkFWD_Extend_Mirror");
    int m_crossHash = Animator.StringToHash("WalkFWD_Cross");
    int m_crossMirrorHash= Animator.StringToHash("WalkFWD_Cross_Mirror");
    //List<List<Transform>> poses = new List<List<Transform>>();
    List<List<Quaternion>> poses = new List<List<Quaternion>>();
    // Use this for initialization
    void Start () {
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
        //    print(t_transforms[i].name + " " + t_transforms[i].localRotation.eulerAngles);
        //}
    }
	
	// Update is called once per frame
	void Update () {
        m_transition += Time.deltaTime;
        if (poses.Count>3)
        {
            UpdateAnimationASD();
        }
        if (m_transition > 1.0f)
        {
            m_transition = 0.0f;
        }
    }

    void UpdateAnimationASD()
    {
        GameObject t_hips = GameObject.Find("Hips");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
        if (m_transition < 0.25f)
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].localRotation = Quaternion.Slerp(poses[0][i], poses[1][i],
                 m_transition / 0.25f);
            }
        }

        else if (m_transition < 0.5f)
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].localRotation = Quaternion.Slerp(poses[1][i], poses[2][i],
                    (m_transition - 0.25f) / 0.25f);
            }
            //print(poses[1][1].name + poses[1][1].transform.localRotation.eulerAngles);
        }
        else if (m_transition < 0.75f)
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].localRotation = Quaternion.Slerp(poses[2][i], poses[3][i],
                    (m_transition - 0.5f) / 0.25f);
            }
        }
        else if (m_transition < 1.0f)
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].localRotation = Quaternion.Slerp(poses[3][i], poses[0][i],
                    (m_transition - 0.75f) / 0.25f);
            }
        }
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
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].localRotation.x, t_transforms[i].localRotation.y, t_transforms[i].localRotation.z,
                t_transforms[i].localRotation.w));
        }
        poses.Add(t_quaternions);

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
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].localRotation.x, t_transforms[i].localRotation.y, t_transforms[i].localRotation.z,
                t_transforms[i].localRotation.w));
        }
        poses.Add(t_quaternions);

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
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].localRotation.x, t_transforms[i].localRotation.y, t_transforms[i].localRotation.z,
                t_transforms[i].localRotation.w));
        }
        poses.Add(t_quaternions);
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
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].localRotation.x, t_transforms[i].localRotation.y, t_transforms[i].localRotation.z,
                t_transforms[i].localRotation.w));
        }
        poses.Add(t_quaternions);
        print(poses[0][1].eulerAngles);
        print(poses[1][1].eulerAngles);
        print(poses[2][1].eulerAngles);
        print(poses[3][1].eulerAngles);
    }
}
