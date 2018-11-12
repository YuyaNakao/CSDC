using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Input : MonoBehaviour
{
    SpriteRenderer Render;

    // プレイヤー待機のスプレイと画像
    public Sprite sprite_wait;

    // プレイヤースタートのスプライト画像
    public Sprite sprite_start;

    // プレイヤーのステータス
    private bool player2_changeflg = false;

    // Use this for initialization
    void Start()
    {
        Render = gameObject.GetComponent<SpriteRenderer>();
        Render.sprite = sprite_wait;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤー入力関数
        ChangeInput();
    }

    public void ChangeInput()
    {
        // プレイヤー２が入力を読み取った時
        if (player2_changeflg == true)
        {
            // プレイヤー２の状態をスタートにする。
            Render.sprite = sprite_start;
        }
        else
        {
            // プレイヤー２の状態を待機。
            Render.sprite = sprite_wait;
        }
        // ＰＳ４のコントローラーの○ボタンもしくはキーボードの２ボタンを押している時
        if (Input.GetButtonDown("Player2_Kettei"))
        {
            player2_changeflg = true;
        }
    }
}
