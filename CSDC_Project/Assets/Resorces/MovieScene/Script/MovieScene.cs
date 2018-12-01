using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieScene : MonoBehaviour
{

    [SerializeField]
    private float MovieTime;        // 動画の再生時間

    public AudioSource audiosource;

	// Use this for initialization
	void Start ()
    {
        // マウスカーソル非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audiosource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 時間経過処理
        MovieTime -= Time.deltaTime;

        // 動画が終了した時、次のシーンに入る
        if(MovieTime <= 0)
        {
//            audiosource.Stop();
            Fade.FadeOut(2);
        }

        if(Input.GetButtonDown("Player1_Kettei"))
        {
            Fade.FadeOut(2);
//            SceneNavigator.Instance.Change("Priproment");
        }

        if (Input.GetButtonDown("Player2_Kettei"))
        {
            audiosource.Stop();
//            Fade.FadeOut(2);
            //            SceneNavigator.Instance.Change("Priproment");
        }

        if (Input.GetButtonDown("Player3_Kettei"))
        {
            audiosource.Stop();
            Fade.FadeOut(2);
            //            SceneNavigator.Instance.Change("Priproment");
        }

        if (Input.GetButtonDown("Player4_Kettei"))
        {
            audiosource.Stop();
            Fade.FadeOut(2);
            //            SceneNavigator.Instance.Change("Priproment");
        }
    }
}
