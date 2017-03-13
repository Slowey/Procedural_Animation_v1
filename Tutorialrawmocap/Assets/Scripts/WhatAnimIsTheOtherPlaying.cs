using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatAnimIsTheOtherPlaying : MonoBehaviour
{
    GameObject otherGameObject;
    public TransitionUpdater transUpdaterScript;
    enum AnimationClips { Walking, Runnning, Crouching, Idle};
    AnimationClips m_activeClip;
    AnimationClips prevClip;
    Animator animator;
    // Use this for initialization
    void Start ()
    {
        otherGameObject = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        m_activeClip = (AnimationClips)transUpdaterScript.activeClip;
        switch (m_activeClip)
        {
            case AnimationClips.Walking:
                animator.Play("WalkFWD", -1, 0.75f);
                break;
            case AnimationClips.Runnning:
                animator.Play("RunCycle 0", -1, 0.0f);

                break;
            case AnimationClips.Crouching:
                animator.Play("Crouch2Idle", -1, 0.2f);

                break;
            case AnimationClips.Idle:
                animator.Play("IdleCycle", -1, 0.5f);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        m_activeClip = (AnimationClips)transUpdaterScript.activeClip;
        animator.SetInteger("whatAnim", (int)m_activeClip);
        if(m_activeClip != prevClip)
        {
            switch (m_activeClip)
            {
                case AnimationClips.Walking:
                    animator.Play("WalkFWD", -1, 0.75f);
                    break;
                case AnimationClips.Runnning:
                    animator.Play("RunCycle 0", -1, 0.0f);

                    break;
                case AnimationClips.Crouching:
                    animator.Play("Crouch2Idle", -1, 0.0f);
                    break;
                case AnimationClips.Idle:
                    animator.Play("IdleCycle", -1, 0.5f);
                    break;
                default:
                    break;
            }
        }


        prevClip = m_activeClip;
    }
}
