using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviorを継承して、初期化メソッドを備えたシングルトンクラス
public class SingletonMonobehaviour<T> : MonoBehaviourWithInit where T : MonoBehaviourWithInit
{
    // インスタンス
    private static T instance;

    // インスタンスを外部から参照する
    public static T Instance
    {
        get
        {
            // インスタンスが生成されていない場合
            if(instance == null)
            {
                // シーン内からインスタンスを取得
                instance = (T)FindObjectOfType(typeof(T));

                // シーン内に存在しない場合はエラー
                if(instance == null)
                {
                    Debug.LogError(typeof(T) + "is nothing");
                }
                // 発見した場合は初期化
                else
                {
                    instance.InitNeed();
                }
            }
            return instance;
        }
    }

    // 初期化
    protected sealed override void Awake()
    {
        // 存在しているインスタンスが自分であれば問題なし
        if(this == Instance)
        {
            return;
        }

        // 本人ではない場合重複して存在しているので、エラー
        Debug.LogError(typeof(T) + "is duplicated");
    }

}

// 初期化メソッドを備えたMonoBehaviour
public class MonoBehaviourWithInit : MonoBehaviour
{
    // 初期化したかどうかのフラグ
    private bool _isInitialized = false;

    // 必要な場合は初期化
    public void InitNeed()
    {
        if(_isInitialized)
        {
            return;
        }

        _isInitialized = true;
    }

    // 初期化
    protected virtual void Init()
    {

    }

    // overrideするためのvirtual作成
    protected virtual void Awake()
    {

    }
}