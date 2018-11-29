using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hutuunoko : MonoBehaviour {
    public int izimepower;//イジメパワー
    public float Speed = 5.0f;//移動量
    public int izimekko_suu = 0;
    public MeshRenderer meshcolor;//メッシュの色
    private Vector3 TargetPosition;//目標点
    private Vector3 OldPosition;
    private float CangeTargetDistance = 1.0f;//この数値より近ければ次の目標点を決める    
    izimekko izimekko;
    float srach_angle = 4.0f;
    private GameObject[] m_izimekko;//いじめっ子タグからのデータを入れるゲームオブジェクト
    private GameObject[] m_izimerarekko;//いじめられっ子タグからのデータを入れるゲームオブジェクト
    private GameObject m_izimekko_l;//いじめっ子リーダータグからのデータを入れるゲームオブジェクト
    private GameObject targetizimerarekko;//追いかけ対象のいじめられっ子
    private Rigidbody rigid;
    public enum move{//状態
        loitering,
        izimekko,
        izimerarekko,
    }
    public enum move_izimekko{//いじめっ子の時の状態
        loitering,
        sarchi,
        izimerarekko,
        izimekko_L,
        izime,
        stop,
    }
    public enum move_izimerarekko{//いじめられっ子時の状態
        loitering,  //徘徊
        shrink,      //いじめられっ子に追い詰められて縮こまる
        escape,      //いじめっ子から逃げる
    }
    public move move_mode;
    public move_izimekko move_mode_izimekko;
    public move_izimerarekko move_mode_izimerarekko;
    private int time1, time2, time3, time4,time5;//状態遷移に使用する関数
	void Start () {
        //初期位置設定
        this.transform.position = GetPosition();
        //初期状態設定
        move_mode = move.loitering;
        move_mode_izimekko = move_izimekko.loitering;
        move_mode_izimerarekko = move_izimerarekko.loitering;
        //最初の目標点を決める
        TargetPosition = GetPosition();
        //いじめ子リーダーのデータを取得
        m_izimekko_l = GameObject.FindGameObjectWithTag("izimekko_L");
        izimepower = 30;
    }
	
	void Update () {
        float DistanceToTarget = 0.0f;//目標点との距離
        Quaternion TargetRotation;//目標点への方向
        DistanceToTarget = Vector3.Distance(this.transform.position, TargetPosition);//目標点
        //慣性をなくす
        if (rigid = GetComponent<Rigidbody>())
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
            switch (move_mode){
                case move.loitering://徘徊
                    DistanceToTarget = Vector3.SqrMagnitude(transform.position - TargetPosition);
                    //目標点の方を向く
                    TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                    //前に進む
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    if (DistanceToTarget < CangeTargetDistance){
                        TargetPosition = GetPosition();
                    }
                    
                    
                    break;
                case move.izimekko:
                  mode_izimekko();
                  break;
                case move.izimerarekko:
                  mode_izimerarekko();
                  break;
                default:
                    break;
            }
            Objectcollision();
            //Debug.Log("普通の子がいじめを受けている数"+izimekko_suu);
    }
    public Vector3 GetPosition(){
        return new Vector3(Random.Range(-18, 35), 2.0f, Random.Range(-14, 38));
    }

    private bool srachizimerarekko()
    {
        float izimerarekkodis = 0.0f;//いじめられっ子への距離
        Vector3 izimerarekkovec;//いじめられっ子への方向ベクトル
        //いじめられっ子のデータを取得
        m_izimerarekko = GameObject.FindGameObjectsWithTag("izimerarekko");
        for (int i = 0; i < m_izimerarekko.Length; i++)
        {
            izimerarekkodis = Vector3.Distance(this.transform.position, m_izimerarekko[i].transform.position);
            if (izimerarekkodis <= 5.0f)
            {//いじめられっ子との距離が5.0f以内
                izimerarekkovec = m_izimerarekko[i].transform.position - this.transform.position;//いじめられっ子への方向ベクトルを求める
                if (Anglefor2Vector(izimerarekkovec, this.transform.forward) <= 22.5f)
                {//進行方向といじめられっ子への方向ベクトルの内積が22.5f以内か
                    targetizimerarekko = m_izimerarekko[i];
                    return true;
                }
            }
        }
        return false;
    }
    //二つのベクトルの内積を０～１８０度で求める関数
    float Anglefor2Vector(Vector3 vector1, Vector3 vector2)
    {
        Vector2 vec1;
        Vector2 vec2;
        vec1.x = vector1.x;
        vec1.y = vector1.z;
        vec2.x = vector2.x;
        vec2.y = vector2.z;
        float ans_angle = 0.0f;
        //二つのベクトルの長さを計算
        float vec1_length = vec1.magnitude;
        float vec2_length = vec2.magnitude;
        //内積とベクトルの長さでcosθを求める
        float cos_sita = Vector2.Dot(vec1, vec2) / (vec1_length * vec2_length);
        //cosθからθを求める
        float sita = Mathf.Acos(cos_sita);
        //θを０～１８０の形に直す
        ans_angle = sita * 180.0f / Mathf.PI;
        return ans_angle;
    }

    private void mode_izimekko(){
        float DistanceToTarget = 0.0f;//目標点との距離
        Quaternion TargetRotation;//目標点への方向
        DistanceToTarget = Vector3.Distance(this.transform.position, TargetPosition);//いじめられっ子への距離を計算
        OldPosition = this.transform.position;
        
        if (izimepower >= 30)
        {
            switch (move_mode_izimekko)
            {
                case move_izimekko.loitering://徘徊
                    //いじめっ子と目標点との距離を求める
                    DistanceToTarget = Vector3.SqrMagnitude(transform.position - TargetPosition);
                    //目標点との距離が近ければ、その場でキョロキョロする動きをする
                    if (DistanceToTarget < CangeTargetDistance)
                    {
                        time3 = 0;
                        move_mode_izimekko = move_izimekko.sarchi;
                        break;
                    }
                    //目標点の方を向く
                    TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 2);
                    //前に進む
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    time2--;
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true && time2 <= 0)
                    {
                        time1 = 0;
                        move_mode = move.izimerarekko;
                        break;
                    }
                    float izimekko_Ldis = 0.0f;
                    //izimekko_Ldis = Vector3.Distance(this.transform.position, izimekko_l.transform.position);
                    if (izimekko_Ldis < 20.0f)
                    {
                        //move_mode = move.izimekko_L;
                        break;
                    }
                    break;
                case move_izimekko.sarchi://動かずにキョロキョロする
                    time4++;
                    if (time4 >= 50)
                    {
                        time4 = 0;
                        srach_angle *= -1.0f;
                    }
                    transform.Rotate(new Vector3(0.0f, srach_angle, 0.0f));
                    time3++;
                    if (time3 >= 600)
                    {
                        TargetPosition = GetPosition();
                        move_mode = move.loitering;
                        break;
                    }
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true && time2 <= 0)
                    {
                        time1 = 0;
                        move_mode = move.izimerarekko;
                        break;
                    }
                    break;
                case move_izimekko.izimerarekko://いじめられっ子へ向かう
                    //目標点(いじめられっ子)の方を向く
                    TargetRotation = Quaternion.LookRotation(targetizimerarekko.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                    //目標点(いじめられっ子)に進む
                    transform.Translate(Vector3.forward * Speed * 1.5f * Time.deltaTime);
                    //視界からいじめられっ子がいなくなると立ち止まる
                    if (srachizimerarekko() == false)
                    {
                        time5 = 0;
                        move_mode_izimekko = move_izimekko.stop;
                        break;
                    }
                    //いじめられっ子に近づくとその場でいじめる
                    if (Vector3.Distance(targetizimerarekko.transform.position, transform.position) <= 2.0f)
                    {
                        izimerarekko m_izimerarekko = targetizimerarekko.GetComponent<izimerarekko>();
                        m_izimerarekko.izimekko_count();
                        move_mode_izimekko = move_izimekko.izime;
                    }
                    time1++;
                    if (time1 >= 300)
                    {

                        time2 = 300;
                        TargetPosition = GetPosition();
                        move_mode_izimekko = move_izimekko.loitering;//徘徊
                        break;
                    }
                    break;
                case move_izimekko.izimekko_L://いじめっ子リーダーに付きまとう
                    //目標点(いじめっ子リーダー)の方を向く
                    TargetRotation = Quaternion.LookRotation(m_izimekko_l.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 2);
                    //目標点(いじめっ子リーダー)に進む
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    break;
                case move_izimekko.izime:
                    break;
                case move_izimekko.stop:
                    time5++;
                    if (time5 > 120)
                    {
                        time1 = 0;
                        move_mode_izimekko = move_izimekko.loitering;
                        break;
                    }
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true)
                    {
                        time1 = 0;
                        move_mode_izimekko = move_izimekko.izimerarekko;
                        break;
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void mode_izimerarekko() {
        float DistanceToTarget = 0.0f;//目標点との距離
        Quaternion TargetRotation;//目標点への方向
        DistanceToTarget = Vector3.Distance(this.transform.position, TargetPosition);//目標点

        switch (move_mode_izimerarekko)
        {
            case move_izimerarekko.loitering://徘徊
                DistanceToTarget = Vector3.SqrMagnitude(transform.position - TargetPosition);
                //目標点の方を向く
                TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                //前に進む
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                if (DistanceToTarget < CangeTargetDistance)
                {
                    TargetPosition = GetPosition();
                }
                if (izimekko_suu >= 3)
                {
                    move_mode_izimerarekko = move_izimerarekko.shrink;
                }
                break;
            case move_izimerarekko.shrink:
                break;
            case move_izimerarekko.escape:
                //いじめ子のデータを取得
                m_izimekko = GameObject.FindGameObjectsWithTag("izimekko");
                float izimekko_dis = 100.0f;
                int index = 0;
                for (int i = 0; i < m_izimekko.Length; i++)
                {
                    if (Vector3.Distance(this.transform.position, m_izimekko[i].transform.position) <= izimekko_dis)
                    {
                        izimekko_dis = Vector3.Distance(this.transform.position, m_izimekko[i].transform.position);
                        index = i;
                    }
                }
                //いじめっ子から逃げる
                TargetRotation = Quaternion.LookRotation(m_izimekko[index].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                transform.Translate(Vector3.forward * -10.0f * Time.deltaTime);
                izimekko_dis = Vector3.Distance(this.transform.position, m_izimekko[index].transform.position);
                if (izimekko_dis >= 10.0f)
                {
                    // move_mode = move.loitering;
                    break;
                }
                break;
            default:
                break;
        }
    }

    private void Objectcollision()
    {
        int i;
        m_izimerarekko = GameObject.FindGameObjectsWithTag("izimerarekko");
        for (i = 0; i < m_izimerarekko.Length; i++)
        {
            if (Vector3.Distance(m_izimerarekko[i].transform.position, transform.position) <= 2.0f)
            {
                transform.Translate(Vector3.forward * -Speed * 0.1f * Time.deltaTime);
            }
        }
        if (Vector3.Distance(m_izimekko_l.transform.position, transform.position) <= 2.0f)
        {
            transform.Translate(Vector3.forward * -Speed * 1.5f * Time.deltaTime);
        }

    }

    public void izimekko_count()
    {
        izimekko_suu++;

    }
}
