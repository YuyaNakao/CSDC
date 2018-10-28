using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    public GameObject targetBeacon;
    public float speed;
    Vector3 aim;
    Rigidbody rid;

    // Use this for initialization
    void Start () {
        rid = GetComponent<Rigidbody>();
        targetBeacon = GameObject.Find("Beacon");

        //向き取得
        aim = this.targetBeacon.transform.position - this.transform.position;
        var look = Quaternion.LookRotation(aim);
        this.transform.localRotation = look;
    }
	
	// Update is called once per frame
	void Update () {


        rid.AddForce(aim*speed);
	}
}
