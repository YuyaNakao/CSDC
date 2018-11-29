using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Anima : MonoBehaviour
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
    void Start()
    {
        // プレイヤーに設定されているAnimatorコンポーネントを取得する
        this.animator = GetComponent<Animator>();
        this.animator.SetBool(setting_flg, true);
    }

    // Update is called once per frame
    void Update()
    {
        // ＰＳ４のコントローラーの○ボタンもしくはキーボードの２ボタンを押している時
        if (Input.GetButtonDown("Player2_Kettei"))
        {
            // 待機モーションに入る
            this.animator.SetBool(key_isStandup, true);
            if (flg == false)
            {
                // プレイヤーの参加人数を加える
                player.sanka_count++;
                flg = true;
            }
        }

        // 全員の参加を確認後、Player2の参加フラグをfalseにして
        // 再度ゲームの申し込みをできるようにする
        if (player.fade_flg == true)
        {
            flg = false;
        }
        
    }
}
