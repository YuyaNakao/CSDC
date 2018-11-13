using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4Anima : MonoBehaviour
{

    public TitleScene player;

    // 参加フラグ
    private bool flg = false;

    // Animator コンポーネント
    private Animator animator;

    // 待機フラグ
    private const string key_isWait = "isWait";

    // Use this for initialization
    void Start()
    {
        // プレイヤーに設定されているAnimatorコンポーネントを取得する

        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ＰＳ４のコントローラーの○ボタンもしくはキーボードの４ボタンを押している時
        if (Input.GetButtonDown("Player4_Kettei"))
        {
            // 待機モーションに入る
            this.animator.SetBool(key_isWait, true);
            if (flg == false)
            {
                // プレイヤーの参加人数を加える
                player.sanka_count++;
                flg = true;
            }
        }

        // 全員の参加を確認後、Player4の参加フラグをfalseにして
        // 再度ゲームの申し込みをできるようにする
        
        if (player.fade_flg == true)
        {
            flg = false;
        }
        
    }
}
