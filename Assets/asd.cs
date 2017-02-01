using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Director;
public class asd : MonoBehaviour {

    public AnimationClip m_animClip;
	// Use this for initialization
	void Start () {
        AnimationState state = GetComponent<AnimationState>();
        state.enabled = true;
        state.weight = 1;
        state.normalizedTime = 8.0f / 100;
        
	}
	
	// Update is called once per frame
	void Update () {
        var clipPlayable = AnimationClipPlayable.Create(m_animClip);
        GetComponent<Animator>().Play(clipPlayable);
	}
}
