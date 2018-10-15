using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject test;
    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Face")
        {

            this.transform.position = test.transform.position;
        }
    }
}
