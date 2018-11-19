/* 製作者:岩見龍
 * CSDCAnimator制御用関数郡
 * 
 * [共通操作関数]
 * 1.ムーブ関数 (正面移動ベクトル(0～1),カーブ数値(-1~1))
 * 
 * [プレイヤー操作専用関数]
 * 1.投げる待機(ture/false)
 * 2.投げる()
 * 3.投げるoff()
 * 4.吹っ飛び()

 * [NPC操作専用関数]
 * 1.着席()
 * 2.起立()
 * 3.いじめ行動(true/false)
 * 4.いじめられ行動(true/false)
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {

	//Animator格納用変数
	Animator animaCSDC;


	// Use this for initialization
	void Start () {
		animaCSDC = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

/*ムーブアニメーション用関数
 * 引数１：移動スピードの割合(0~1の範囲内)
 * 引数２：曲がる時のキャラの傾き(-1～+1の範囲内)
 * [内容]
 * 移動の時のモーションを再生、微調整する関数。
 * 引数1にはスピードの比率を0~1の範囲内で入れると良い
 * 引数2にはコントローラーの左右の傾きを-1～+1の範囲内で入れると良い
 */
	public void AnimaMove(float charForward,float charTurn){
		//Animator.Forward = cahrForward  移動Animationの再生。 走らない 0⇔1　走る
		//Animator.Turn = charTurn	　　左ターン　-1 ~ +1 右ターン
		animaCSDC.SetFloat("Forward",charForward);
		animaCSDC.SetFloat ("Turn", charTurn);

	}

/*投げる時の待機モーション用関数
 * 引数１：TossIdleを再生するかしないかを設定する。
 * [内容]
 * tureに設定することで、投げる待機モーションになる。
 */
	public void AnimaTossIdle(bool tossIdleFlag){
		//TossIdleのフラグを変更する
		animaCSDC.SetBool("tossIdle",tossIdleFlag);
		
	}

/*投げるモーション用関数
 * 引数１：Tossを再生するかしないかを設定する。
 * [内容]
 * tureに設定することで、投げる待機モーションになる。
 * また、tossIdleを止める。
 */
	public void AnimaToss(bool tossFlag){
		//Tossのフラグを変更する。
		animaCSDC.SetBool("tossIdle",false);
		animaCSDC.SetBool("toss",tossFlag);

	}
/*投げるモーションとセットで使う。
 * [内容]
 * １フレームだけTossフラグをOnにするために
 * マイフレーム一番最初に必ず入れる。
 */
	public void AnimaTossOff(){
		animaCSDC.SetBool ("tossIdle", false);
	}
		

/*吹っ飛びモーション用関数
 * 引数１：popを再生するかしないかを設定する。
 * [内容]
 * tureに設定することで、popモーションになる。
 * また、他のフラグをoffにする。
 */
	public void AnimaPop(bool popFlag){
		//Popのフラグを変更する。
		animaCSDC.SetBool("popFlag",popFlag);
		animaCSDC.SetBool ("tossIdle", false);
		animaCSDC.SetBool ("toss", false);

	}
/*吹っ飛びモーションをオフにする
 * [内容]
 * １フレームだけ、Popフラグを入れるために
 * マイフレーム一番最初に必ず入れる
 */
	public void AnimaPopOff(){
		//Popのフラグを変更する。
		animaCSDC.SetBool("popFlag",false);
	}

/*座るモーションの設定
 * [内容]
 * tureにすることで、座るモーションになる。
 */
	public void AnimaSetting(bool settingFlag){
		//SettingFlagを変更する
		animaCSDC.SetBool("settingFlag",settingFlag);
	}

/*起立モーションの設定
 * [内容]
 * tureにすることで、起立モーションになる。
 */
	public void AnimaStandupFlag(bool standupFlag){
		//standupFlgを変更する
		animaCSDC.SetBool("settingFlag",false);
	}

/*いじめモーションの設定
 * [内容]
 * tureにすることで、いじめモーションになる。
 */
	public void AnimaIzime(bool izimeFlag){
		//IzimeFlagを変更する
		animaCSDC.SetBool("IzimeFlag",izimeFlag);
	}

/*いじめられモーションの設定
 * [内容]
 * tureにすることで、いじめられモーションになる。
 */
	public void AnimaIzimerare(bool izimerareFlag){
		//IzimerareFlagを変更する。
		animaCSDC.SetBool("IzimerareFlag",izimerareFlag);
	}
		

}
