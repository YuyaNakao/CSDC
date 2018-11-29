using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 制作者：松田
public class Input_PushB : MonoBehaviour
{

    private Renderer renderer;

    public bool draw_flg;

    [SerializeField]
    private float StartTime;       // カメラの持続時間
                                   // Use this for initialization
    void Start ()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        draw_flg = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 時間経過処理
        StartTime -= Time.deltaTime;
        if(StartTime < 0)
        {
            renderer.enabled = true;
        }
    }
}
