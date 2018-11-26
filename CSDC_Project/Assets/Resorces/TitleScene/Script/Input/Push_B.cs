using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_B : MonoBehaviour
{
    SpriteRenderer Render;

    public TitleScene push_player;

    public Sprite sprite_push_b;

    // Use this for initialization
    void Start ()
    {
        Render = gameObject.GetComponent<SpriteRenderer>();
        Render.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (push_player.sanka_count == 4)
        {
            Render.enabled = true;
        }
	}
}
