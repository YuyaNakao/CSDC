using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Shot"))
        {
            Destroy(this.gameObject);
        }
    }
    


}