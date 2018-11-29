using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class izimekko_L : MonoBehaviour {
    public float max_x = 0.0f;//x軸への行動範囲(最大値)
    public float min_x = 0.0f;//x軸への行動範囲(最小値)
    public float max_z = 0.0f;//z軸への行動範囲(最大値)
    public float min_z = 0.0f;//z軸への行動範囲(最小値)
    public int izimepower;//イジメパワー
    public float Speed = 5.0f;//移動量
    private Vector3 TargetPosition;//目標点
    private Vector3 OldPosition;
    private float CangeTargetDistance = 1.0f;//この数値より近ければ次の目標点を決める
    izimekko_L leader;
    private GameObject[] izimerarekko;//いじめられっ子タグからのデータを入れるゲームオブジェクト
    private Rigidbody rigid;
    private int time1 = 0, time2 = 0;//状態遷移に使用する関数
    public enum move{
        loitering,
        izimerarekko,
    }
    move move_mode;
    void Start()
    {
        //初期位置設定
        this.transform.position = new Vector3(Random.Range(-22, 22), 0, Random.Range(-22, 22));
        izimepower = 60;
        //最初の目標点を決める
        TargetPosition = GetPosition();
        //いじめられっ子のデータを取得
        izimerarekko = GameObject.FindGameObjectsWithTag("izimerarekko");
        //慣性をなくす
        rigid = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        float DistanceToTarget = 0.0f;//目標点との距離
        Quaternion TargetRotation;//目標手への方向
        DistanceToTarget = Vector3.Distance(this.transform.position, TargetPosition);//いじめられっ子への距離を計算
        OldPosition = this.transform.position;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        if (izimepower >= 0)
        {
            switch (move_mode)
            {
                case move.loitering://徘徊
                    //いじめっ子と目標点との距離を求める
                    DistanceToTarget = Vector3.SqrMagnitude(transform.position - TargetPosition);
                    //目標点との距離が近ければ次の目標点を決める
                    if (DistanceToTarget < CangeTargetDistance)
                    {
                        TargetPosition = GetPosition();

                    }
                    //目標点の方を向く
                    TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                    //前に進む
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    time2--;
                    //いじめられっ子との距離を計算し、近かったらいじめられっ子を目標点にして近寄る
                    for (int i = 0; i < izimerarekko.Length; i++)
                    {
                        DistanceToTarget = Vector3.Distance(this.transform.position, izimerarekko[i].transform.position);
                        if (DistanceToTarget <= 10.0f && time2 <= 0 && 2.0f < DistanceToTarget)
                        {
                            TargetPosition = izimerarekko[i].transform.position;
                            time1 = 0;
                            move_mode = move.izimerarekko;//いじめられっ子へ向かう
                            break;
                        }
                    }
                    break;
                case move.izimerarekko://いじめられっ子へ向かう
                    //目標点の方を向く
                    TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                    //前に進む
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    //いじめられっ子に近づきすぎたらそれ以上すすめないようにする
                    if (DistanceToTarget <= 5.0f)
                    {
                        this.transform.position = OldPosition;
                    }

                    if (time1 >= 600)
                    {
                        time2 = 600;
                        TargetPosition = GetPosition();
                        move_mode = move.loitering;//徘徊
                        break;
                    }
                    break;

                default:
                    break;
                   
            }
            time1++;
        }else {
            Destroy(this);
        }
    }
    public Vector3 GetPosition()
    {
        return new Vector3(Random.Range(-22, 22), 0, Random.Range(-22, 22));//xとｚで-22～22までのランダムな地点を設定する
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("izimekko"))
        {
            if (move_mode == move.loitering)
            {
                TargetPosition = GetPosition();
            }

        }
    }

}

