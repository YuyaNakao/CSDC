using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Access : MonoBehaviour
{

    public GameObject gameobject;

    void Start()
    {

        //Reseaver 内の number という変数を取得する
        int num = gameobject.GetComponent<Reseaver>().number;
        Debug.Log(num);

        //Reseaver 内の　ReturnAccess という関数を使用する
        gameobject.GetComponent<Reseaver>().ReturnAccess();
    }
}
