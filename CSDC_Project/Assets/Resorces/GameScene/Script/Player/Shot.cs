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
    public GameObject enemy;
    public float losttime;
    ScoreManager manager;
    public int playerNo;
    int count;
    bool enemyflag;
    Vector3 enemypos;

    // Use this for initialization
    void Start () {
        rid = GetComponent<Rigidbody>();
        rid.AddForce((transform.forward) * speed, ForceMode.VelocityChange);
        manager = GameObject.Find("Score").GetComponent<ScoreManager>();
        count = 0;
        Destroy(obj, 5);
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyflag)
        {
            enemyflag = false;
        }
    }

    private void OnTriggerEnter(Collider hit)
    {

       if (hit.CompareTag("izimekko"))
        {
            count++;
        }

        if (hit.CompareTag("PointZone"))
        {
            manager.AddScore(playerNo - 1, count);
            Destroy(obj);
        }
    }

    private void OnDestroy()
    {
        enemyflag = true;
    }


}
