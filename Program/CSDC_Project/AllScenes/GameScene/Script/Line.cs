using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer lRend;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        lRend = GetComponent<LineRenderer>();
        lRend.startWidth = 2;
        lRend.endWidth = 2;
        player = GameObject.Find("Player1");
    }

    // Update is called once per frame
    void Update()
    {
        lRend.SetPosition(0, this.transform.position);
        lRend.SetPosition(1, player.transform.position);
    }

}
