using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4_Input : MonoBehaviour
{
    SpriteRenderer Render;

    // プレイヤー待機のスプレイと画像
    public Sprite sprite_wait;

    // プレイヤースタートのスプライト画像
    public Sprite sprite_start;

    // プレイヤーのステータス
    private bool player4_changeflg = false;

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
        // プレイヤー４が入力を読み取った時
        if (player4_changeflg == true)
        {
            // プレイヤー４の状態をスタートにする。
            Render.sprite = sprite_start;
        }
        else
        {
            // プレイヤー４の状態を待機。
            Render.sprite = sprite_wait;
        }
        // ＰＳ４のコントローラーの○ボタンもしくはキーボードの４ボタンを押している時
        if (Input.GetButtonDown("Player4_Kettei"))
        {
            player4_changeflg = true;
        }
    }

}
