using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// 作成者　松田

public class PlayerAnima : MonoBehaviour
{
    public TitleScene player;

    // 参加フラグ
    private bool flg = false;

    // Animator コンポーネント
    private Animator animator;

    // 立つフラグ
    private const string key_isStandup = "standupFlg";

    // 待機フラグ
    private const string setting_flg = "settingFlg";

    // Use this for initialization
    void Start ()
    {
        // プレイヤーに設定されているAnimatorコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        this.animator.SetBool(setting_flg, true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // ＰＳ４のコントローラーの○ボタンもしくはキーボードの１ボタンを押している時
        if (Input.GetButtonDown("Player1_Kettei"))
        {
            // 待機モーションに入る
            this.animator.SetBool(key_isStandup, true);
            this.animator.SetBool(setting_flg, false);
            if (flg == false)
            {
                // プレイヤーの参加人数を加える
                player.sanka_count+=1;
                flg = true;
            }
        }

        // 参加人数が4人になったとき
        
        if(player.sanka_count == 4)
        {
            if (Input.GetButtonDown("Player1_Kettei"))
            {
                player.fade_flg = true;
            }
        }
        

        // 全員の参加を確認後、Player1の参加フラグをfalseにして
        // 再度ゲームの申し込みをできるようにする
        
        if (player.fade_flg == true)
        {
            flg = false;
        }
    }
}
