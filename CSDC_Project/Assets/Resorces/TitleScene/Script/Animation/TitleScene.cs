using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// 作成者　松田

public class TitleScene : MonoBehaviour
{
    // 参加人数のカウント
    public int sanka_count;
    // フェードフラグ
    public bool fade_flg = false;
    // 参加フラグ
    public bool sanka_flg;

	// Use this for initialization
	void Start ()
    {
        sanka_count = 0;
        sanka_flg = false;
        // マウスカーソル非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(fade_flg == true)
        {
            fade_flg = false;
            // 参加人数の初期化
            sanka_count = 0;
            // フェードアウト
            Fade.FadeOut(1);          
        }
	}
}
