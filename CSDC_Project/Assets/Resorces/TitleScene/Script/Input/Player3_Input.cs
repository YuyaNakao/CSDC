using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3_Input : MonoBehaviour
{
    SpriteRenderer Render;

    // プレイヤー待機のスプレイと画像
    public Sprite sprite_wait;

    // プレイヤースタートのスプライト画像
    public Sprite sprite_start;

    // プレイヤーのステータス
    private bool player3_changeflg = false;

    private bool player3_button_push = false;

    private AudioSource AudioSource;

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
        // プレイヤー３が入力を読み取った時
        if (player3_changeflg == true)
        {
            // プレイヤー３の状態をスタートにする。
            Render.sprite = sprite_start;
            if (player3_button_push == false)
            {
                AudioSource.Play();
                player3_button_push = true;
            }
        }
        else
        {
            // プレイヤー３の状態を待機。
            Render.sprite = sprite_wait;
        }
        // ＰＳ４のコントローラーの○ボタンもしくはキーボードの３ボタンを押している時
        if (Input.GetButtonDown("Player3_Kettei"))
        {
            player3_changeflg = true;
        }
    }
}
