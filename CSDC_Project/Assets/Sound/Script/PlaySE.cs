using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaySE : MonoBehaviour {

    private AudioSource AudioSource;
    public AudioClip[] Number = new AudioClip[10];

    // Use this for initialization
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        /*
         Input.GetKey
         Input.GetKeyDown
         Input.GetKeyUp
         */

        //サンプル用キー入力
        if (Input.GetKeyDown(KeyCode.A))
        {
            play_se(0,0.3f);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            play_se(1,1.0f);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            play_se(2, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            play_se(3, 1.0f);
        }

    }

    //SEを呼び出す関数
    //　※SEの番号を呼び出して使う
    public void play_se(int number, float volume)
    {
        //音量error回避
        if (volume < 0.0f) {
            volume = 0;
        }
        if (volume > 1.0f)
        {
            volume = 1.0f;
        }
        //音量
        AudioSource.volume = volume;
        //SE種類
        AudioSource.PlayOneShot(Number[number]);
    }

}
