using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField]
    GameObject[] enemys;
    [SerializeField]
    float appearNextTime;
    [SerializeField]
    int maxNumOfEnemys;
    private int numberOfEnemys;
    private float elapsedTime;
    

    // Use this for initialization
    void Start () {
        numberOfEnemys = 0;
        elapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (numberOfEnemys >= maxNumOfEnemys)
        {
            return;
        }
        elapsedTime += Time.deltaTime;

	//　経過時間が経ったら

    if (elapsedTime > appearNextTime)
        {
            elapsedTime = 0f;

            AppearEnemy();
        }
    }

    //　敵出現メソッド
    void AppearEnemy()
    {
        //　出現させる敵をランダムに選ぶ
        var randomValue = Random.Range(0, enemys.Length);
        //　敵の向きをランダムに決定
        var randomRotationY = Random.value * 360f;

        GameObject.Instantiate(enemys[randomValue], transform.position, Quaternion.Euler(0f, randomRotationY, 0f));

        numberOfEnemys++;
        elapsedTime = 0f;
    }

}
