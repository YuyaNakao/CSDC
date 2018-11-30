using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSingleton : MonoBehaviour
{
    /*
    // インスタンスの実体
    private static ObjectSingleton instance = null;

    // 実体が存在しない時
    public static ObjectSingleton Instance => instance ?? ( instance = GameObject.FindWithTag( "Enemy" ).GetComponent<ObjectSingleton>() );

    private void Awake()
    {
        // インスタンスが複数存在した場合　破棄する。
        if(this != instance)
        {
            Destroy(this.gameObject);
            return;
        }

        // インスタンスが１つしかない場合シーンを移行しても残す
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        if(this == Instance)
        {
            instance = null;
        }
    }
    */
}

