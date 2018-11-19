using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTest : MonoBehaviour {
	MotionController MC;
	Animator animaCSDC;

	// Use this for initialization
	void Start () {
		MC = GetComponent<MotionController> ();
		animaCSDC = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//オフ用
		MC.AnimaTossOff();
		MC.AnimaPopOff ();


		//投げるモーション
		if(Input.GetKey(KeyCode.T)){
			MC.AnimaTossIdle(true);		
		}else{
			MC.AnimaToss (true);
		}

		//弾けるモーション
		if (Input.GetKey (KeyCode.P)) {
			MC.AnimaPop (true);
		}

		//いじめ
		if (Input.GetKey (KeyCode.I) && !animaCSDC.GetBool("IzimeFlag")) {
			MC.AnimaIzime(true);
		}
		if (Input.GetKey (KeyCode.I) && animaCSDC.GetBool("IzimeFlag")) {
			MC.AnimaIzime(false);
		}

		//いじめられ
		if (Input.GetKey (KeyCode.U) && !animaCSDC.GetBool("IzimerareFlag")) {
			MC.AnimaIzimerare(true);
		}
		if (Input.GetKey (KeyCode.U) && animaCSDC.GetBool("IzimerareFlag")) {
			MC.AnimaIzimerare(false);
		}



	}
}
