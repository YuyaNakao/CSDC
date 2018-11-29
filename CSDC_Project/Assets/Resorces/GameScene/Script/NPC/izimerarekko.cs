using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class izimerarekko : MonoBehaviour {
    public float max_x = 0.0f;//x軸への行動範囲(最大値)
    public float min_x = 0.0f;//x軸への行動範囲(最小値)
    public float max_z = 0.0f;//z軸への行動範囲(最大値)
    public float min_z = 0.0f;//z軸への行動範囲(最小値)
    [SerializeField]private  float Speed = 5.0f;//移動量
    public MeshRenderer meshcolor;
    private Vector3 TargetPosition;//目標点
    public int izimekko_suu=0;//いじめを受けている数
    private Vector3 OldPosition;
    GameObject[] m_izimekko;       //いじめっ子タグからのデータを入れるゲームオブジェクト
    [SerializeField]
    private float CangeTargetDistance = 1.0f;//この数値より近ければ次の目標点を決める
    private int change_move_time = 0;//状態遷移に使用する変数
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
                object_move(this.transform.position, Speed);
                if (DistanceToTarget < CangeTargetDistance) {
                    TargetPosition = GetPosition();
                }
                if (izimekko_suu >= 1) {
                    move_mode = move.escape;
                }
                break;
            case move.shrink:
                break;
            case move.escape:
                //いじめ子のデータを取得
                m_izimekko = GameObject.FindGameObjectsWithTag("izimekko");
                float izimekko_dis=100.0f;//一番近いいじめっ子の距離
                int index = 0;//一番近いいじめっこの添え字
                for (int i = 0; i < m_izimekko.Length; i++)
                {
                    if (Vector3.Distance(this.transform.position, m_izimekko[i].transform.position) <= izimekko_dis)
                    {
                        izimekko_dis = Vector3.Distance(this.transform.position, m_izimekko[i].transform.position);
                        index = i;
                    }
                }
                //一番近いいじめっ子から逃げる
                TargetRotation = Quaternion.LookRotation(m_izimekko[index].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime);
                //前に進む
                object_move(this.transform.position, Speed * -2.0f);
                change_move_time++;
                if(change_move_time>600){
                    move_mode=move.shrink;
                    break;
                }
                izimekko_dis = Vector3.Distance(this.transform.position, m_izimekko[index].transform.position);
                if (izimekko_dis >= 10.0f)//いじめっ子から離れたら
                {
                    move_mode = move.loitering;
                    break;
                }

                break;
            default:
                break;
        }

	}

    public Vector3 GetPosition() {
        return new Vector3(UnityEngine.Random.Range(min_x, max_x), 0, UnityEngine.Random.Range(min_z, max_z));//xとｚで-22～22までのランダムな地点を設定する
    }

    public void object_move(Vector3 position, float speed){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (position.x <= min_x || max_x <= position.x || position.z <= min_z || max_z <= position.z)
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }

    }

    public void izimekko_count(){//いじめを受けている数をカウントする別クラスで使用する
        izimekko_suu++;  
    }
}
