using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    public float speed;
    CharacterController con;
    GameObject shot;
    Vector3 aim;
    Rigidbody rid;
    Vector3 shot_pos;
    public GameObject obj;
    public float losttime;
    ScoreManager manager;
    public int playerNo;

    // Use this for initialization
    void Start () {
        rid = GetComponent<Rigidbody>();
        //rid.AddForce(transform.forward * speed);
        rid.AddForce((transform.forward) * speed, ForceMode.VelocityChange);
        manager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }
	
	// Update is called once per frame
	void Update () {
        int enemycount = transform.childCount;
        if(enemycount < 0){
            Destroy(obj, losttime);
        }
        // rid.velocity = transform.forward * speed;

    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("izimekko"))
        {
            manager.AddScore(playerNo-1);
        }
    }

}
