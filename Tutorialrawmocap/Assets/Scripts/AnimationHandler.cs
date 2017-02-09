//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AnimationHandler : MonoBehaviour {

//    Animator m_animator;
//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//    }
//    void SaveKeyFrames0() 
//    {
//        GameObject t_hips;
//        List<Transform> t_transforms = new List<Transform>();
//        m_animator.Play(m_extendHash, 0, 0);
//        m_animator.Update(0.001f);
//        t_hips = GameObject.Find("Hips");
//        t_hips.GetComponentsInChildren<Transform>(t_transforms);
//        List<Quaternion> t_quaternions = new List<Quaternion>();
//        for (int i = 0; i < t_transforms.Count; i++)
//        {
//            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
//                t_transforms[i].rotation.w));
//        }
//        poses.Add(t_quaternions);
//        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
//    }
//    void SaveKeyFrames1()
//    {
//        GameObject t_hips;
//        List<Transform> t_transforms = new List<Transform>();
//        m_animator.Play(m_crossHash, 0, 0);
//        m_animator.Update(0.001f);
//        t_hips = GameObject.Find("Hips");
//        t_hips.GetComponentsInChildren<Transform>(t_transforms);
//        List<Quaternion> t_quaternions = new List<Quaternion>();
//        for (int i = 0; i < t_transforms.Count; i++)
//        {
//            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
//                t_transforms[i].rotation.w));
//        }
//        poses.Add(t_quaternions);
//        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));


//    }
//    void SaveKeyFrames2()
//    {
//        GameObject t_hips;
//        List<Transform> t_transforms = new List<Transform>();
//        m_animator.Play(m_extendMirrorHash, 0, 0);
//        m_animator.Update(0.001f);
//        t_hips = GameObject.Find("Hips");
//        t_hips.GetComponentsInChildren<Transform>(t_transforms);
//        List<Quaternion> t_quaternions = new List<Quaternion>();
//        for (int i = 0; i < t_transforms.Count; i++)
//        {
//            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
//                t_transforms[i].rotation.w));
//        }
//        poses.Add(t_quaternions);
//        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
//    }
//    void SaveKeyFrames3()
//    {
//        GameObject t_hips;
//        List<Transform> t_transforms = new List<Transform>();
//        m_animator.Play(m_crossMirrorHash, 0, 0);
//        m_animator.Update(0.001f);
//        m_animator.Stop();
//        t_hips = GameObject.Find("Hips");
//        t_hips.GetComponentsInChildren<Transform>(t_transforms);
//        List<Quaternion> t_quaternions = new List<Quaternion>();

//        for (int i = 0; i < t_transforms.Count; i++)
//        {
//            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
//                t_transforms[i].rotation.w));
//        }
//        poses.Add(t_quaternions);
//        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
//        print(poses[0][1].eulerAngles);
//        print(poses[1][1].eulerAngles);
//        print(poses[2][1].eulerAngles);
//        print(poses[3][1].eulerAngles);

//        print(hipspos[0].y);
//        print(hipspos[1].y);
//        print(hipspos[2].y);
//        print(hipspos[3].y);

//        MakeGameObjectsForDebuggingRotations();
//    }
//	}