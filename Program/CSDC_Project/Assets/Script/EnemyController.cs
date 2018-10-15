using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    ObjManager obj;
    public GameObject test;
    PlayerController player;
    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Face")
        {

            this.transform.position = test.transform.position;
        }
    }
    void Start () {
        player = test.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
