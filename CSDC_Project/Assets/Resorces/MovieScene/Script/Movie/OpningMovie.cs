using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class OpningMovie : MonoBehaviour {

    // VideoPlayerコンポーネント
    private VideoPlayer mPlayer;

    // AudioSourceコンポーネント
    private AudioSource mAudioSource;

    // テクスチャを表示するＵＩ
    public RawImage mImage;

    // テクスチャがセットされているか確認する
    private bool mcheck = false;

	// Use this for initialization
	void Start ()
    {
        // プレイヤー用のコンポーネントを取得
        mPlayer = GetComponent<VideoPlayer>();
        // スクリプトでAudioSourceに変更
        mPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //
        mAudioSource = GetComponent<AudioSource>();
        // オーディオトラックを有効にする
        mPlayer.EnableAudioTrack(0, true); ;
        mPlayer.SetTargetAudioSource(0, mAudioSource);
        // 再生する
        mPlayer.Play();

	}
	
	// Update is called once per frame
	void Update ()
    {
		// テクスチャの設定
        if(mPlayer.texture != null && !mcheck)
        {
            mImage.texture = mPlayer.texture;
            mcheck = true;
        }
        

        // オープニングシーンの時のみ再生。

        //　Ａボタンが押されたら動画が停止する。
        if(Input.GetKeyDown(KeyCode.A))
        {
            //再生中でなければ再生
            if (!mPlayer.isPlaying)
            {   
                mPlayer.Play();
            }
                // 再生中であれば停止
            else
            {  
                mPlayer.Pause();
            }
        }
	}
}
