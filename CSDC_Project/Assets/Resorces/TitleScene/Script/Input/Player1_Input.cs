using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Input : MonoBehaviour
{

    SpriteRenderer Render;

    // プレイヤー待機のスプレイと画像
    public Sprite sprite_wait;

    // プレイヤースタートのスプライト画像
    public Sprite sprite_start;

    // プレイヤーのステータス
    private bool player1_changeflg = false;

    private bool player1_button_push = false;

    private AudioSource AudioSource;

    PlaySE script;

    // Use this for initialization
    void Start()
    {
        Render = gameObject.GetComponent<SpriteRenderer>();
        Render.sprite = sprite_wait;
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤー入力関数
        ChangeInput();
    }

    public void ChangeInput()
    {
        // プレイヤー１が入力を読み取った時
        if (player1_changeflg == true)
        {
            // プレイヤー１の状態をスタートにする。
            Render.sprite = sprite_start;
            
            if(player1_button_push == false)
            {
                AudioSource.Play();
                player1_button_push = true;
            }
        }
        else
        {
            // プレイヤー１の状態を待機。
            Render.sprite = sprite_wait;
        }
        // ＰＳ４のコントローラーの○ボタンもしくはキーボードの１ボタンを押している時
        if (Input.GetButtonDown("Player1_Kettei"))
        {
            player1_changeflg = true;
        }
    }
}

