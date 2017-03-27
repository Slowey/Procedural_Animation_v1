using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransitionUpdater : MonoBehaviour
{
    public enum AnimationClips { Walking, Runnning, Crouching, Idle};
    [Range(0.0f, 20.0f)]
    public float deltaTimeIncreaser = 2.3f;
    [Range(0.0f, 2.0f)]
    public float damper = 0.6f;
    [Range(0.0f, 10.0f)]
    public float angularVelo = 6.0f;
    public AnimationClips activeClip;
    [Range(0, 2)]
    public int m_framesToAdd = 0;
    int m_prevFramesToAdd = 0;
    AnimationClips prevClip;
    Animator m_animator;
    AnimWalkFWD m_animWalk;
    AnimDuck m_animDuck;
    AnimRun m_animRun;
    AnimIdle m_animIdle;
    float m_transition;
    float m_prevTrans;
    int m_extendHash;// = Animator.StringToHash("WalkFWD_Extend");
    int m_extendMirrorHash;// = Animator.StringToHash("WalkFWD_Extend_Mirror");
    int m_crossHash;// = Animator.StringToHash("WalkFWD_Cross");
    int m_crossMirrorHash;// = Animator.StringToHash("WalkFWD_Cross_Mirror");
    int m_between1_1;
    int m_between2_1;
    int m_between2_2;
    int m_crouchingHash = Animator.StringToHash("Crouching");
    int m_standingHash = Animator.StringToHash("Standing");
    //List<List<Transform>> poses = new List<List<Transform>>();
    List<List<Quaternion>> poses = new List<List<Quaternion>>();
    List<Vector3> hipspos = new List<Vector3>();
    public bool headBob = true;
    public bool m_flip = false;
    bool loadAnims = true;
    float m_timeAdjuster = 1.0f;
    public static TransitionUpdater m_instance;
    // Use this for initialization
    void Start()
    {
        m_instance = this;
        m_animator = GetComponent<Animator>();
        m_transition = 0.0f;
        m_animWalk  = new AnimWalkFWD();
        m_animDuck = new AnimDuck();
        m_animRun = new AnimRun();
        m_animIdle = new AnimIdle();
        //m_animIdle.SetFrames(m_framesToAdd);
    }
    public void ChangeClips(int p_clip, int p_increment, int p_flip)
    {
        //kommer enbart använda rightsideclippet så ändras left också. Kanske inte ska funka så senare så därför finns elftscreen
        activeClip = (AnimationClips)p_clip;
        m_framesToAdd = p_increment;
        if (p_flip == 1)
        {
            m_flip = true;
        }
        else
        {
            m_flip = false;
        }
    }
    // Update is called once per frame
    void Update()
    {   
        if(prevClip!=activeClip)
        {
            loadAnims = true;
        }
        
        m_prevTrans = m_transition;
        m_transition += Time.deltaTime / m_timeAdjuster;
        //print(m_transition);///// PRINTHÄR asdasdsaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        if (m_transition > 1.0f)
        {
            m_transition -= 1.0f;
        }
        
        if (m_prevFramesToAdd != m_framesToAdd)
        {
            GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
            GameObject t_original1 = GameObject.Find("OriginalAnimation");
            if (m_flip)
            {
                t_hybrid1.transform.position = new Vector3(1, 0, 0);
                t_original1.transform.position = new Vector3(-1, 0, 0);
            }
            else
            {
                t_hybrid1.transform.position = new Vector3(-1, 0, 0);
                t_original1.transform.position = new Vector3(1, 0, 0);
            }
            switch (activeClip)
            {

                case AnimationClips.Idle:
                    poses.Clear();
                    hipspos.Clear();
                    m_animator.applyRootMotion = true;
                    if (m_framesToAdd == 0)
                    {
                        SaveKeyFramesWalkFWD0();
                        SaveKeyFramesWalkFWD2();
                    }
                    else if (m_framesToAdd == 1)
                    {
                        SaveKeyFramesWalkFWD0();
                        SaveKeyFramesIdleBetween1_1();
                        SaveKeyFramesWalkFWD2();
                    }
                    else if (m_framesToAdd == 2)
                    {
                        SaveKeyFramesWalkFWD0();
                        SaveKeyFramesIdleBetween2_1();
                        SaveKeyFramesIdleBetween1_1();
                        SaveKeyFramesIdleBetween2_2();
                        SaveKeyFramesWalkFWD2();
                    }
                    m_animIdle.ChangeKeyFrames(m_framesToAdd);
                    m_timeAdjuster = m_animIdle.timeAdjuster;
                    break;
                case AnimationClips.Crouching:
                    poses.Clear();
                    hipspos.Clear();
                    m_animator.applyRootMotion = false;
                    if (m_framesToAdd == 0)
                    {
                        SaveKeyFramesCrouching();
                        SaveKeyFramesStanding();
                        deltaTimeIncreaser = 0.86f;
                    }
                    else if (m_framesToAdd == 2)
                    {
                        SaveKeyFramesCrouching();
                        m_between1_1 = Animator.StringToHash("Crouching_2_1");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("Crouching_1_1");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("Crouching_2_2");
                        SaveKeyFramesBetween1_1();
                        SaveKeyFramesStanding();
                        deltaTimeIncreaser = 0.7f;
                    }
                    else if (m_framesToAdd == 1)
                    {
                        SaveKeyFramesCrouching();
                        m_between1_1 = Animator.StringToHash("Crouching_1_1");
                        SaveKeyFramesBetween1_1();
                        SaveKeyFramesStanding();
                        deltaTimeIncreaser = 0.7f;
                    }
                    break;
                case AnimationClips.Walking:
                    poses.Clear();
                    hipspos.Clear();
                    m_animator.applyRootMotion = false;
                    if (m_framesToAdd == 0)
                    {
                        SaveKeyFramesWalkFWD0();
                        SaveKeyFramesWalkFWD1();
                        SaveKeyFramesWalkFWD2();
                        SaveKeyFramesWalkFWD3();
                    }
                    else if (m_framesToAdd == 1)
                    {
                        SaveKeyFramesWalkFWD0();
                        m_between1_1 = Animator.StringToHash("WalkFWD_Extend1_1");
                        SaveKeyFramesBetween1_1();
                        SaveKeyFramesWalkFWD1();
                        m_between1_1 = Animator.StringToHash("WalkFWD_Cross1_1");
                        SaveKeyFramesBetween1_1();
                        SaveKeyFramesWalkFWD2();
                        m_between1_1 = Animator.StringToHash("WalkFWD_Extend_Mirror1_1");
                        SaveKeyFramesBetween1_1();
                        SaveKeyFramesWalkFWD3();
                        m_between1_1 = Animator.StringToHash("WalkFWD_Cross_Mirror1_1");
                        SaveKeyFramesBetween1_1();
                    }
                    else if (m_framesToAdd == 2)
                    {
                        InvokeWalkSecondIncrement();
                    }
                    break;
                case AnimationClips.Runnning:
                    poses.Clear();
                    hipspos.Clear();
                    m_animator.applyRootMotion = false;
                    if (m_framesToAdd == 0)
                    {
                        SaveKeyFramesWalkFWD0();
                        SaveKeyFramesWalkFWD1();
                        SaveKeyFramesWalkFWD2();
                        SaveKeyFramesWalkFWD3();
                    }
                    else if (m_framesToAdd == 1)
                    {
                        m_between1_1 = Animator.StringToHash("1");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("2");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("3");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("4");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("5");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("6");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("7");
                        SaveKeyFramesBetween1_1();
                        m_between1_1 = Animator.StringToHash("8");
                        SaveKeyFramesBetween1_1();
                    }
                    break;
                default:
                    break;

            }
        }
        if (loadAnims)
        {
            poses.Clear();
            hipspos.Clear();
            switch (activeClip)
            {
                case AnimationClips.Walking:
                    m_extendHash = m_animWalk.GetExtendHash();
                    m_extendMirrorHash = m_animWalk.GetExtendMirrorHash();
                    m_crossHash = m_animWalk.GetCrossHash();
                    m_crossMirrorHash = m_animWalk.GetCrossMirrorHash();
                    m_animator.applyRootMotion = false;
                    if (m_framesToAdd == 0)
                    {
                        InvokeKeyFramesWalkingFWD();
                    }
                    else if (m_framesToAdd == 1)
                    {
                        Invoke("InvokeWalkFirstIncrement",0.25f);
                    }
                    else if (m_framesToAdd == 2)
                    {
                        Invoke("InvokeWalkSecondIncrement", 0.25f);
                    }
                    m_timeAdjuster = m_animWalk.timeAdjuster;
                    break;
                case AnimationClips.Runnning:
                    m_extendHash = m_animRun.GetExtendHash();
                    m_extendMirrorHash = m_animRun.GetExtendMirrorHash();
                    m_crossHash = m_animRun.GetCrossHash();
                    m_crossMirrorHash = m_animRun.GetCrossMirrorHash();
                    m_animator.applyRootMotion = false;
                    if (m_framesToAdd == 0)
                    {
                        InvokeKeyFramesWalkingFWD();
                    }
                    else if (m_framesToAdd == 1)
                    {
                        InvokeRunFirstIncrement();
                    }
                    m_timeAdjuster = m_animRun.timeAdjuster;
                    break;
                case AnimationClips.Crouching:
                    m_animator.applyRootMotion = false;
                    if (m_framesToAdd == 0)
                    {
                        Invoke("SaveKeyFramesCrouching", 0.25f);
                        Invoke("SaveKeyFramesStanding", 0.5f);
                        deltaTimeIncreaser = 0.86f;
                    }
                    else if (m_framesToAdd == 1)
                    {
                        Invoke("InvokeCrouchFirstIncrement", 0.25f);
                        deltaTimeIncreaser = 0.7f;
                    }
                    
                    else if (m_framesToAdd == 2)
                    {
                        Invoke("InvokeCrouchSecondIncrement", 0.25f);
                        deltaTimeIncreaser = 0.7f;
                    }
                    m_timeAdjuster = m_animDuck.timeAdjuster;
                    break;
                case AnimationClips.Idle:
                    m_extendHash = m_animIdle.GetIdleIdleHash();
                    m_extendMirrorHash = m_animIdle.GetExtendHash();
                    m_between1_1 = m_animIdle.GetIdleInbetweenOneOne();
                    m_between2_1 = m_animIdle.GetIdleInbetweenTwoOne();
                    m_between2_2 = m_animIdle.GetIdleInbetweenTwoTwo();
                    m_animator.applyRootMotion = true;
                    if (m_framesToAdd == 0)
                    {
                        InvokeTwoKeyFrames();
                    }
                    else if (m_framesToAdd == 1)
                    {
                        Invoke("InvokeIdleFirstIncrement", 0.25f);
                    }
                    else if (m_framesToAdd == 2)
                    {
                        InvokeFiveKeyFrames();
                    }
                    m_timeAdjuster = m_animIdle.timeAdjuster;
                    m_animIdle.ChangeKeyFrames(m_framesToAdd);
                    break;
                default:
                    break;
            }
            loadAnims = false;
            m_transition = 0;
        }
        switch (activeClip)
        {
            case AnimationClips.Walking:
                if (poses.Count > 3 + m_framesToAdd)
                {
                    m_animWalk.WalkingUpdate(m_transition, m_prevTrans, poses, hipspos, headBob, gameObject);//UpdateAnimationASD();
                }
                break;
            case AnimationClips.Runnning:
                if (poses.Count > 3)
                {
                m_animRun.RunningUpdate(m_transition, m_prevTrans, poses, hipspos, headBob, gameObject);//UpdateAnimationASD();
                }
                break;
            case AnimationClips.Crouching:
                if (poses.Count > 1)
                {
                    m_animDuck.UpdateAnimation(m_transition, poses, hipspos, deltaTimeIncreaser, angularVelo, damper);
                }
                break;
            case AnimationClips.Idle:
                if (poses.Count > 1 + m_framesToAdd)
                {
                    m_animIdle.IdleUpdate(m_transition, m_prevTrans, poses, hipspos, headBob, gameObject);
                }
                break;
            default:
                break;
        }
        GameObject t_hybrid = GameObject.Find("HybridAnimation");
        GameObject t_original = GameObject.Find("OriginalAnimation");
        if (m_flip)
        {
            t_hybrid.transform.position = new Vector3(1, 0, 0);
            t_original.transform.position = new Vector3(-1, 0, 0);
        }
        else
        {
            t_hybrid.transform.position = new Vector3(-1, 0, 0);
            t_original.transform.position = new Vector3(1, 0, 0);
        }
        prevClip = activeClip;
        m_prevFramesToAdd = m_framesToAdd;
    }
    //void UpdateAnimationASD()
    //{
    //    GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
    //    Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
    //    //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 0.0f).eulerAngles);
    //    //print(SQUAD.Spline(poses[0][1], poses[1][1], poses[2][1], poses[3][1], 1.0f).eulerAngles);
    //
    //    for (int i = 0; i < t_bones.Length; i++)
    //    {
    //        if (i == 2 && m_transition > 0.5f && m_prevTrans < 0.5f)
    //        {
    //            //print(t_bones[i].rotation.eulerAngles);
    //            //testStruct d = new  testStruct();
    //            SQUAD.testStruct d = new SQUAD.testStruct();
    //            d = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_prevTrans);
    //            //print(d.alongLine);
    //            d = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_transition);
    //            t_bones[i].rotation = d.quat;
    //            //print(d.alongLine);
    //        }
    //        else
    //        {
    //            t_bones[i].rotation = SQUAD.Spline(poses[0][i], poses[1][i], poses[2][i], poses[3][i], m_transition).quat;
    //        }
    //    }
    //    if (!headBob)
    //    {
    //        if (m_transition < 0.25f)
    //        {
    //            t_bones[0].position = Vector3.Slerp(hipspos[0], hipspos[1],
    //                m_transition / 0.25f);
    //        }
    //
    //        else if (m_transition < 0.5f)
    //        {
    //            t_bones[0].position = Vector3.Slerp(hipspos[1], hipspos[2],
    //                (m_transition - 0.25f) / 0.25f);
    //            //print(poses[1][1].name + poses[1][1].transform.rotation.eulerAngles);
    //        }
    //        else if (m_transition < 0.75f)
    //        {
    //            t_bones[0].position = Vector3.Slerp(hipspos[2], hipspos[3],
    //                (m_transition - 0.5f) / 0.25f);
    //        }
    //        else if (m_transition < 1.0f)
    //        {
    //            t_bones[0].position = Vector3.Slerp(hipspos[3], hipspos[0],
    //                (m_transition - 0.75f) / 0.25f);
    //        }
    //    }
    //}

    void SaveKeyFramesWalkFWD0()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_extendHash, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
    }
    void SaveKeyFramesWalkFWD1()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_crossHash, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));


    }
    void SaveKeyFramesWalkFWD2()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_extendMirrorHash, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
    }
    void SaveKeyFramesWalkFWD3()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
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
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
        //print(poses[0][1].eulerAngles);
        //print(poses[1][1].eulerAngles);
        //print(poses[2][1].eulerAngles);
        //print(poses[3][1].eulerAngles);
        //
        //print(hipspos[0].y);
        //print(hipspos[1].y);
        //print(hipspos[2].y);
        //print(hipspos[3].y);

        //MakeGameObjectsForDebuggingRotations();
    }
    void SaveKeyFramesStanding()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_standingHash, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
        //print(hipspos+"crouchinvoke");
    }
    void SaveKeyFramesCrouching()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();

        m_animator.Play(m_crouchingHash, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        //print(hipspos+"crouchinvoke");
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
    }
    void SaveKeyFramesIdleBetween1_1()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_between1_1, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
    }
    void SaveKeyFramesBetween1_1()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_between1_1, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
    }
    void SaveKeyFramesIdleBetween2_1()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_between2_1, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
    }
    void SaveKeyFramesIdleBetween2_2()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        GameObject t_hips;
        List<Transform> t_transforms = new List<Transform>();
        m_animator.Play(m_between2_2, 0, 0);
        m_animator.Update(0.001f);
        m_animator.Stop();
        t_hips = GameObject.Find("Hips");
        t_hips.GetComponentsInChildren<Transform>(t_transforms);
        List<Quaternion> t_quaternions = new List<Quaternion>();
        for (int i = 0; i < t_transforms.Count; i++)
        {
            t_quaternions.Add(new Quaternion(t_transforms[i].rotation.x, t_transforms[i].rotation.y, t_transforms[i].rotation.z,
                t_transforms[i].rotation.w));
        }
        poses.Add(t_quaternions);
        hipspos.Add(new Vector3(t_hips.transform.position.x, t_hips.transform.position.y, t_hips.transform.position.z));
    }
    void MakeGameObjectsForDebuggingRotations()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
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
    void InvokeKeyFramesWalkingFWD()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        Invoke("SaveKeyFramesWalkFWD0", 0.25f);
        Invoke("SaveKeyFramesWalkFWD1", 0.5f);
        Invoke("SaveKeyFramesWalkFWD2", 0.75f);
        Invoke("SaveKeyFramesWalkFWD3", 1.0f);
    }
    void InvokeTwoKeyFrames()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        Invoke("SaveKeyFramesWalkFWD0", 0.25f);
        Invoke("SaveKeyFramesWalkFWD2", 0.5f);
    }
    void InvokeFiveKeyFrames()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        Invoke("SaveKeyFramesWalkFWD0", 0.25f);
        Invoke("SaveKeyFramesIdleBetween2_1", 0.5f);
        Invoke("SaveKeyFramesIdleBetween1_1", 0.75f);
        Invoke("SaveKeyFramesIdleBetween2_2", 1.0f);
        Invoke("SaveKeyFramesWalkFWD2", 1.25f);
    }
    void InvokeIdleFirstIncrement()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        SaveKeyFramesWalkFWD0();
        SaveKeyFramesIdleBetween1_1();
        SaveKeyFramesWalkFWD2();
    }
    void InvokeCrouchFirstIncrement()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        SaveKeyFramesCrouching();
        m_between1_1 = Animator.StringToHash("Crouching_1_1");
        SaveKeyFramesBetween1_1();
        SaveKeyFramesStanding();
    }
    void InvokeCrouchSecondIncrement()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        SaveKeyFramesCrouching();
        m_between1_1 = Animator.StringToHash("Crouching_2_1");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("Crouching_1_1");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("Crouching_2_2");
        SaveKeyFramesBetween1_1();
        SaveKeyFramesStanding();
    }
    void InvokeWalkFirstIncrement()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        SaveKeyFramesWalkFWD0();
        m_between1_1 = Animator.StringToHash("WalkFWD_Extend1_1");
        SaveKeyFramesBetween1_1();
        SaveKeyFramesWalkFWD1();
        m_between1_1 = Animator.StringToHash("WalkFWD_Cross1_1");
        SaveKeyFramesBetween1_1();
        SaveKeyFramesWalkFWD2();
        m_between1_1 = Animator.StringToHash("WalkFWD_Extend_Mirror1_1");
        SaveKeyFramesBetween1_1();
        SaveKeyFramesWalkFWD3();
        m_between1_1 = Animator.StringToHash("WalkFWD_Cross_Mirror1_1");
        SaveKeyFramesBetween1_1();
    }
    void InvokeWalkSecondIncrement()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        m_between1_1 = Animator.StringToHash("11");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("12");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("13");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("14");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("15");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("16");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("17");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("18");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("19");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("20");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("21");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("22");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("23");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("24");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("25");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("26");
        SaveKeyFramesBetween1_1();
    }
    void InvokeRunFirstIncrement()
    {
        GameObject t_hybrid1 = GameObject.Find("HybridAnimation");
        if (m_flip)
        {
            t_hybrid1.transform.position = new Vector3(1, 0, 0);
        }
        else
        {
            t_hybrid1.transform.position = new Vector3(-1, 0, 0);
        }
        //SaveKeyFramesWalkFWD0();
        //m_between1_1 = Animator.StringToHash("RunExtend1_1");
        //SaveKeyFramesBetween1_1();
        //SaveKeyFramesWalkFWD1();
        //m_between1_1 = Animator.StringToHash("RunCross1_1");
        //SaveKeyFramesBetween1_1();
        //SaveKeyFramesWalkFWD2();
        //m_between1_1 = Animator.StringToHash("RunExtendMirrored1_1");
        //SaveKeyFramesBetween1_1();
        //SaveKeyFramesWalkFWD3();
        //m_between1_1 = Animator.StringToHash("RunCrossMirrored1_1");
        //SaveKeyFramesBetween1_1();

        m_between1_1 = Animator.StringToHash("1");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("2");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("3");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("4");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("5");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("6");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("7");
        SaveKeyFramesBetween1_1();
        m_between1_1 = Animator.StringToHash("8");
        SaveKeyFramesBetween1_1();

    }
}
