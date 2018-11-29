using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hutuunoko : MonoBehaviour {
    public float max_x = 0.0f;//x軸への行動範囲(最大値)
    public float min_x = 0.0f;//x軸への行動範囲(最小値)
    public float max_z = 0.0f;//z軸への行動範囲(最大値)
    public float min_z = 0.0f;//z軸への行動範囲(最小値)
    public int izimepower;//イジメパワー
    public float Speed = 5.0f;//移動量
    public int izimekko_suu = 0;
    private Vector3 TargetPosition;//目標点
    [SerializeField]
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
        shrink      //いじめられっ子に追い詰められて縮こまる
    }
    public move move_mode;
    public move_izimekko move_mode_izimekko;
    public move_izimerarekko move_mode_izimerarekko;
    private int change_move_time = 0;//状態遷移に使用する関数
    private int overlooking_time = 0;//srachi時に使用する変数
	void Start () {
        //初期位置設定
        this.transform.position = new Vector3(Random.Range(-22, 22), 0, Random.Range(-22, 22));
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
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
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
                    object_move(this.transform.position, Speed);
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
           
    }
    public Vector3 GetPosition(){
        return new Vector3(Random.Range(-22, 22), 0, Random.Range(-22, 22));//xとｚで-22～22までのランダムな地点を設定する
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
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        float DistanceToTarget = 0.0f;//目標点との距離
        Quaternion TargetRotation;//目標点への方向
        DistanceToTarget = Vector3.Distance(this.transform.position, TargetPosition);//いじめられっ子への距離を計算
        
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
                        change_move_time = 0;
                        move_mode_izimekko = move_izimekko.sarchi;
                        break;
                    }
                    //目標点の方を向く
                    TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 2);
                    //前に進む
                    object_move(this.transform.position, Speed);
                    change_move_time--;
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true && change_move_time <= 0)
                    {
                        change_move_time = 0;
                        move_mode = move.izimerarekko;
                        break;
                    }
                    float izimekko_Ldis = 0.0f;
                    
                    if (izimekko_Ldis < 20.0f)
                    {
                        //move_mode = move.izimekko_L;
                        break;
                    }
                    break;
                case move_izimekko.sarchi://動かずにキョロキョロする
                    overlooking_time++;
                    if (overlooking_time >= 50)
                    {
                        overlooking_time = 0;
                        srach_angle *= -1.0f;
                    }
                    transform.Rotate(new Vector3(0.0f, srach_angle, 0.0f));
                    change_move_time++;
                    if (change_move_time >= 600)
                    {
                        TargetPosition = GetPosition();
                        srach_angle = 0;
                        move_mode = move.loitering;
                        break;
                    }
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true && change_move_time <= 0)
                    {
                        change_move_time = 0;
                        move_mode = move.izimerarekko;
                        break;
                    }
                    break;
                case move_izimekko.izimerarekko://いじめられっ子へ向かう
                    //目標点(いじめられっ子)の方を向く
                    TargetRotation = Quaternion.LookRotation(targetizimerarekko.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                    //目標点(いじめられっ子)に進む
                    object_move(this.transform.position, Speed);
                    //視界からいじめられっ子がいなくなると立ち止まる
                    if (srachizimerarekko() == false)
                    {
                        change_move_time = 0;
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
                    change_move_time++;
                    if (change_move_time >= 300)
                    {

                        change_move_time = 300;
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
                    object_move(this.transform.position, Speed);
                    break;
                case move_izimekko.izime:
                    break;
                case move_izimekko.stop:
                    change_move_time++;
                    if (change_move_time > 120)
                    {
                        change_move_time = 0;
                        move_mode_izimekko = move_izimekko.loitering;
                        break;
                    }
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true)
                    {
                        change_move_time = 0;
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
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
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
                object_move(this.transform.position, Speed);
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


    public void object_move(Vector3 position, float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (position.x <= min_x || max_x <= position.x || position.z <= min_z || max_z <= position.z)
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }

    }

}
