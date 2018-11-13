using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Fade : MonoBehaviour {
    private static Canvas fade_Canvas;          //フェード用のCanvas
    private static Image fade_Image;            //フェード用のImage
    private static float a = 0.0f;              //フェード用のアルファ値
    private static bool fedein_flag = false;    //フェードイン用フラグ
    private static bool fedeout_flag = false;   //フェードアウト用フラグ
    private static float fade_time = 0.2f;      //フェードしたい時間（単位は秒）
    private static int nextscene = 1;           //遷移先のシーン番号
    static void FadeInit()
    {
        //フェード用のCnvasを生成
        
        GameObject FadeCanvas = new GameObject("FadeCanvas");
        fade_Canvas = FadeCanvas.AddComponent<Canvas>();
        FadeCanvas.AddComponent<GraphicRaycaster>();
        fade_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvas.AddComponent<Fade>();
        
        //最前面になるようにソートオーダを設定をする
        fade_Canvas.sortingOrder = 100;
        //フェード用のImageを生成
        fade_Image = new GameObject("FadeImage").AddComponent<Image>();
        fade_Image.transform.SetParent(FadeCanvas.transform, false);
        fade_Image.rectTransform.anchoredPosition = Vector3.zero;
        //Imageのサイズを指定
        fade_Image.rectTransform.sizeDelta = new Vector2(1920, 1080);
    }
    //フェードイン関数
    public static void FadeIn()
    {
        if (fade_Image == null) FadeInit(); //Imageを生成されていなかったらInit関数へ
        fade_Image.color = Color.black;     //Imageの色を黒にする
        fedein_flag = true;                 //フェードイン用フラグをオンにする
    }
    //フェードアウト関数
    public static void FadeOut(int n)
    {
        if (fade_Image == null) FadeInit(); //imageを生成されていなかったらInit関数へ
        nextscene = n;                      //遷移先のシーンの番号を代入
        fade_Image.color = Color.clear;     //Imageの色を消す
        fade_Canvas.enabled = true;         //フェードアウト用フラグをオンにする
        fedeout_flag = true;
    }

    void Update()
    {
        if (fedein_flag == true)
        {
            a -= Time.deltaTime / fade_time;//経過時間から透明度を計算する
            //透明度が0以下ならフェードイン終了する
            if (a <= 0.0f)
            {
                fedein_flag = false;
                a = 0.0f;
                fade_Canvas.enabled = false;
            }
            fade_Image.color = new Color(0.0f, 0.0f, 0.0f, a);//フェード用Imageの透明度設定
        }
        else if (fedeout_flag)
        {
            a += Time.deltaTime / fade_time;//経過時間から透明度を計算する
           //透明度が１以上ならフェードアウトを終了する
            if (a >= 1.0f)
            {
                fedeout_flag = false;
                a = 1.0f;
                SceneManager.LoadScene(nextscene);//次のシーン遷移する
            }
            fade_Image.color = new Color(0.0f, 0.0f, 0.0f, a);//フェード用Imageの透明度設定
        }
    }
}
