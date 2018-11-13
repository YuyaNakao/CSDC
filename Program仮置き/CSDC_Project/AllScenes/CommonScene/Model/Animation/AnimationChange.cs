using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour {

	//キャラクター
	public GameObject mainCharacter;
	public GameObject target;

	//キャラクターコントローラー宣言
	private CharacterController characterController;
	//Animator宣言
	private Animator animator;

	//引張ハンティングフラグ関連
	public bool pullFlag = false;	//引っ張り始めたフラグ
	public bool popFlag = false;	//弾け飛ばすフラグ



	// Use this for initialization
	void Start () {
		//Animatorを確保
		characterController = GetComponent <CharacterController> ();
		animator = GetComponent <Animator> ();
	}

	// Update is called once per frame
	void Update () {
		//1フレームだけTrueにしたいため、毎回最初にfalseを入れる。
		popFlag = false;


		//スペースの入力で引張ハンティングスタート
		if (Input.GetKey (KeyCode.Space)) {
			animator.SetBool ("pullFlag", true);
			pullFlag = true;	//ひっぱり開始の合図
		} else {
			animator.SetBool ("pullFlag", false);
			pullFlag = false;	//ひっぱり終了
			popFlag = true;		//発射フラグを立てる。マイフレームリセットしてるから、一フレームだけ通るはず？？？
		}

		//pull中はキャラクターの向きを反転させる　→　☓
		//ターゲットの向きを向いて歩きたい →　○
		//pullFlgを持っていればターゲットの方を向き続ける。
		if (pullFlag) {
			mainCharacter.transform.LookAt (target.transform);
		} 


		//移動中はAnimationを再生させておく。
		//移動指定なくて、かつ、引っ張っているならばAnimationを止める。
		//AnimationSpeed->Speed->MultPlierに影響させている。
		if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f ) {	
			animator.SetFloat("AnimationSpeed", 1.0f);
		} else if(pullFlag){
			animator.SetFloat("AnimationSpeed", 0.0f);
		}

		//pull中専用の移動


		
	}
}
