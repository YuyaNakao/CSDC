using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class izimerarekko : MonoBehaviour {
    [SerializeField]private  float Speed = 5.0f;//移動量
    public MeshRenderer meshcolor;
    public int izimekko_suu=0;
    private Vector3 TargetPosition;//目標点
    private Vector3 OldPosition;
    GameObject[] m_izimekko;       //いじめっ子タグからのデータを入れるゲームオブジェクト
    private float CangeTargetDistance = 1.0f;//この数値より近ければ次の目標点を決める
    private Rigidbody rigid;
    public enum move {//状態
        loitering,  //徘徊
        shrink,      //いじめられっ子に追い詰められて縮こまる
        escape,      //いじめっ子から逃げる
    }
    move move_mode;
    void Start () {
        //初期位置設定
        this.transform.position = new Vector3(UnityEngine.Random.Range(-22, 22), 0, UnityEngine.Random.Range(-22, 22));
        //初期状態設定
        move_mode = move.loitering;
        //慣性をなくす
        if (rigid = GetComponent<Rigidbody>())
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
        //最初の目標点を決める
        TargetPosition = GetPosition();
	}

	void Update () {
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        float DistanceToTarget = 0.0f;//目標点との距離
        Quaternion TargetRotation;//目標点への方向
        DistanceToTarget = Vector3.Distance(this.transform.position, TargetPosition);//目標点

        switch (move_mode) {
            case move.loitering://徘徊
                DistanceToTarget = Vector3.SqrMagnitude(transform.position - TargetPosition);
                //目標点の方を向く
                TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                //前に進む
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                if (DistanceToTarget < CangeTargetDistance) {
                    TargetPosition = GetPosition();
                }
                if (izimekko_suu >= 3) {
                    move_mode = move.shrink;
                }
                break;
            case move.shrink:
                break;
            case move.escape:
                //いじめ子のデータを取得
                m_izimekko = GameObject.FindGameObjectsWithTag("izimekko");
                float[] izimekko_dis=new float[m_izimekko.Length];
                for (int i = 0; i < m_izimekko.Length; i++)
                {
                    izimekko_dis[i] = Vector3.Distance(this.transform.position , m_izimekko[i].transform.position);
               
                }
                break;
            default:
                break;
        }
	}

    public Vector3 GetPosition() {
        return new Vector3(UnityEngine.Random.Range(-22, 22), 0, UnityEngine.Random.Range(-22, 22));//xとｚで-22～22までのランダムな地点を設定する
    }

    public void izimekko_count(){
        izimekko_suu++;
        
    }
}
