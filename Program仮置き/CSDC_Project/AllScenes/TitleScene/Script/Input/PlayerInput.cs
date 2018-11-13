using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // プレイヤーの最大人数
    private int max_player = 4;

    SpriteRenderer[] Render;

    // プレイヤー待機のスプレイと画像
    public  Sprite[]  sprite_wait;

    // プレイヤースタートのスプライト画像
    public Sprite[] sprite_start;

    // プレイヤーのステータス
    private bool player1_changeflg = false;
    private bool player2_changeflg = false;
    private bool player3_changeflg = false;
    private bool player4_changeflg = false;

    void Start()
    {
        // 4プレイヤー分のコンポーネント作成
        for(int i=0;i<max_player;i++)
        {
            Render[i] = gameObject.GetComponent<SpriteRenderer>();
            Render[i].sprite = sprite_wait[i];
        }
    }

    // Use this for initialization

    void Update()
    {
        // それぞれのプレイヤー入力関数
        ChangeInput();
    }

    public void ChangeInput()
    {
        // プレイヤー１が入力を読み取った時
        if (player1_changeflg == true)
        {
            // プレイヤー１の状態をスタートにする。
            Render[0].sprite = sprite_start[0];
        }
        else
        {
            // プレイヤー１の状態を待機。
            Render[0].sprite = sprite_wait[0];
        }
        // 1ボタンが押された時（コントローラー操作時の実装は後日）
        if (Input.GetKey(KeyCode.Alpha1))
        {
            player1_changeflg = true;
        }

        // プレイヤー２が入力を読み取った時
        if (player2_changeflg == true)
        {
            // プレイヤー２の状態をスタートにする。
            Render[1].sprite = sprite_start[1];
        }
        else
        {
            // プレイヤー２の状態を待機
            Render[1].sprite = sprite_wait[1];
        }
        // ２ボタンが押された時（コントローラー操作時の実装は後日）
        if (Input.GetKey(KeyCode.Alpha2))
        {
            player2_changeflg = true;
        }

        // プレイヤー３が入力を読み取った時
        if (player3_changeflg == true)
        {
            // プレイヤー３の状態をスタートにする。
            Render[2].sprite = sprite_start[2];
        }
        else
        {
            // プレイヤー３の状態を待機
            Render[2].sprite = sprite_wait[2];
        }
        // ３ボタンが押された時（コントローラー操作時の実装は後日）
        if (Input.GetKey(KeyCode.Alpha3))
        {
            player3_changeflg = true;
        }

        // プレイヤー４が入力を読み取った時
        if (player4_changeflg == true)
        {
            // プレイヤー４の状態をスタートにする。
            Render[3].sprite = sprite_start[3];
        }
        else
        {
            // プレイヤー４の状態を待機
            Render[3].sprite = sprite_wait[3];
        }
        // ４ボタンが押された時（コントローラー操作時の実装は後日）
        if (Input.GetKey(KeyCode.Alpha4))
        {
            player1_changeflg = true;
        }
    }
}
