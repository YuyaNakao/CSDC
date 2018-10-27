using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour {

	//キャラクターコントローラー宣言
	private CharacterController characterController;
	//Animator宣言
	private Animator animator;

	// Use this for initialization
	void Start () {
		//Animatorを確保
		characterController = GetComponent <CharacterController> ();
		animator = GetComponent <Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.P)) {
			animator.SetBool ("pullFlag", true);
		} else {
			animator.SetBool ("pullFlag", false);
		}

		if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f ) {
			animator.SetFloat("AnimationSpeed", 1.0f);
		} else if(Input.GetKey (KeyCode.P)){
			animator.SetFloat("AnimationSpeed", 0.0f);
		}

		
	}
}
