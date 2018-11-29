using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Push_Start : MonoBehaviour
{

    public TitleScene player;

    private Renderer renderer;

    [SerializeField]
    private float StartTime;
    
    // Use this for initialization
    void Start ()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(player.sanka_count == 4)
        {
            // 時間経過処理
            StartTime -= Time.deltaTime;
            if(StartTime < 0)
            {
                renderer.enabled = true;
            }
        }
	}
}
