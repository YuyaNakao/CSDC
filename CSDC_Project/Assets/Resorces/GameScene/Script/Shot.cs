using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    public GameObject targetBeacon;
    public float speed;
    CharacterController con;
    GameObject shot;
    Vector3 aim;
    Rigidbody rid;
    Vector3 shot_pos;

    // Use this for initialization
    void Start () {
        rid = GetComponent<Rigidbody>();
        targetBeacon = GameObject.Find("Beacon");
        shot = GameObject.Find("Shot");
        rid.AddForce(transform.forward * speed);

    }
	
	// Update is called once per frame
	void Update () {


    }

}
