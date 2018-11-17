using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour {

    public AudioSource AudioSource;
    public AudioClip[] Number = new AudioClip[10];

    private int     oldBgmNumber;     //再生時間のカウント
    private bool    bgmOnFlag;      //再生中ならON
    private float   duration;       //経過時間

    // Use this for initialization
    void Start () {
        AudioSource = this.GetComponent<AudioSource>();

        //ループフラグOFF
        bgmOnFlag = false;

        //経過時間初期化
        duration = 0;

        //ループ用BGM使用中ナンバー初期化
        oldBgmNumber = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Number[1].length);
        //Debug.Log(duration);

        //Loop再生するか
        if (bgmOnFlag)
        {
            //フレーム書き換えごとに経過時間を累積
            duration += Time.deltaTime;

            if (Number[oldBgmNumber].length < duration) {
                //いちようBGM停止
                stop_bgm();

                //BGM再び再生
                play_bgm(oldBgmNumber);

                //経過時間初期化
                duration = 0;
            }
        }

        //*************************************************
        //      サンプル用キー入力
        //*************************************************

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //デバッグログ
            Debug.Log("BGM再生　0番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　0番
            play_bgm(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //デバッグログ
            Debug.Log("BGM再生　1番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　1番
            play_bgm(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //デバッグログ
            Debug.Log("BGM再生　2番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　2番
            play_bgm(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //デバッグログ
            Debug.Log("BGM再生　3番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　3番
            play_bgm(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //デバッグログ
            Debug.Log("BGM再生　4番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　4番
            play_bgm(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //デバッグログ
            Debug.Log("BGM再生　5番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　5番
            play_bgm(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //デバッグログ
            Debug.Log("BGM再生　6番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　6番
            play_bgm(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //デバッグログ
            Debug.Log("BGM再生　7番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　7番
            play_bgm(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //デバッグログ
            Debug.Log("BGM再生　8番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　8番
            play_bgm(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //デバッグログ
            Debug.Log("BGM再生　9番");

            //前のBGM停止
            stop_bgm();

            //BGM再生　9番
            play_bgm(9);
        }



        //-------------------------------------------------

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //デバッグログ
            Debug.Log("一時停止");

            //一時停止
            onPointer_bgm(true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //デバッグログ
            Debug.Log("一時停止解除");

            //一時停止解除
            onPointer_bgm(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //デバッグログ
            Debug.Log("BGM停止 ※ループONの時,再び再生されます");
         
            //BGM停止
            stop_bgm();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //デバッグログ
            Debug.Log("音量を0.1上げる");

            //音量を0.1上げる
            changeVolumeUD_bgm(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //デバッグログ
            Debug.Log("音量を-0.1下げる");

            //音量を-0.1下げる
            changeVolumeUD_bgm(-0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //デバッグログ
            Debug.Log("音量を0.8にする");

            //音量を0.8にする
            changeVolume_bgm(0.8f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //デバッグログ
            Debug.Log("音量を0.3にする");

            //音量を0.3にする
            changeVolume_bgm(0.3f);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //デバッグログ
            Debug.Log("ループON");

            //ループON
            changeLoop_bgm(true);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //デバッグログ
            Debug.Log("ループOFF");

            //ループOFF
            changeLoop_bgm(false);
        }
    }

    //*****************************************************
    //  BGM再生 　曲番号[0 ~]
    //*****************************************************
    public void play_bgm(int number)
    {
        //BGMループ用に番号を保存しておく
        oldBgmNumber = number;

        //BGM種類
        AudioSource.PlayOneShot(Number[number]);
        
    }

    //*****************************************************
    //  BGM停止
    //*****************************************************
    public void stop_bgm()
    {

        //BGM停止
        this.AudioSource.Stop();

        //経過時間初期化
        duration = 0;
    }

    //*****************************************************
    //  BGM一時停止[1] / 解除[0]
    //*****************************************************
    public void onPointer_bgm(bool stopFlag)
    {
        //一時停止
        if (stopFlag) {

            this.AudioSource.Pause();
        }
        //解除
        else {
            this.AudioSource.UnPause();
        }

    }

    //*****************************************************
    //  BGM音量調整  音量変更値 [0.0f~1.0f]
    //*****************************************************
    public void changeVolume_bgm(float volume)
    {

        if (volume < 0.0f)
        {

            AudioSource.volume = 0.0f;
        }
        else if (volume > 1.0f)
        {

            AudioSource.volume = 1.0f;
        }
        else
        {
            //音量
            AudioSource.volume = volume;
        }
    }

    //*****************************************************
    //  BGM音量調整  音量変更値  UP[+0.01~]  DOWN[-0.01~]
    //*****************************************************
    public void changeVolumeUD_bgm(float volume)
    {

        if (AudioSource.volume + volume < 0.0f)
        {

            AudioSource.volume = 0.0f;
        }
        else if (AudioSource.volume + volume > 1.0f)
        {

            AudioSource.volume = 1.0f;
        }
        else
        {
            //音量
            AudioSource.volume += volume;
        }       
    }

    //*****************************************************
    //Loop変更   on[true] / off[false]
    //*****************************************************
    public void changeLoop_bgm(bool loop)
    {
        //ループフラグ
        bgmOnFlag = loop;

        //※見た目のみ　(ループ設定)
        AudioSource.loop = loop;
    }


}
