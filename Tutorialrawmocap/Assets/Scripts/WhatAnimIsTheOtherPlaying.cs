using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatAnimIsTheOtherPlaying : MonoBehaviour
{
    GameObject otherGameObject;
    public TransitionUpdater transUpdaterScript;
    enum AnimationClips { Walking, Runnning, Crouching };
    AnimationClips m_activeClip;
    AnimationClips prevClip;
    Animator animator;
    // Use this for initialization
    void Start ()
    {
        otherGameObject = GameObject.FindGameObjectWithTag("Player");
        //transUpdaterScript = otherGameObject.GetComponent<TransitionUpdater>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update ()
    {
        m_activeClip = (AnimationClips)transUpdaterScript.activeClip;
        animator.SetInteger("whatAnim", (int)m_activeClip);
        // print("hej");
        // print((int)m_activeClip);


        switch (m_activeClip)
        {
            case AnimationClips.Walking:

                break;
            case AnimationClips.Runnning:
                break;
            case AnimationClips.Crouching:

                break;
            default:
                break;
        }
        //prevClip = m_activeClip;
    }
}
