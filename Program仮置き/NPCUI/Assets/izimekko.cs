using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class izimekko : MonoBehaviour{
    public int izimepower;//イジメパワー
    public float Speed = 5.0f;//移動量
    private Vector3 TargetPosition;//目標点
    private Vector3 OldPosition;
    private float CangeTargetDistance = 1.0f;//この数値より近ければ次の目標点を決める
    private GameObject[] izimerarekko;//いじめられっ子タグからのデータを入れるゲームオブジェクト
    private GameObject targetizimerarekko;//追いかけ対象のいじめられっ子
    private GameObject izimekko_l;//いじめっ子リーダータグからのデータを入れるゲームオブジェクト
    private GameObject[] hutuunoko;//普通の子タグからのデータを入れるゲームオブジェクト
    private Rigidbody rigid;
    private int time1, time2 ,time3,time4,time5;//状態遷移に使用する関数
    private Vector3 tmpvec;
    float srach_angle = 4.0f;
   
    public enum move {
        loitering,
        sarchi,
        izimerarekko,
        izimekko_L,
        izime,
        stop,
    }
    move move_mode;
    void Start(){
        //初期位置設定
        this.transform.position = new Vector3(Random.Range(-22, 22), 0, Random.Range(-22, 22));
        izimepower = 30;
        //最初の目標点を決める
        TargetPosition = GetPosition();
        //初期状態設定
        move_mode = move.loitering;
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        float DistanceToTarget = 0.0f;//目標点との距離
        Quaternion TargetRotation;//目標点への方向
        DistanceToTarget = Vector3.Distance(this.transform.position, TargetPosition);//いじめられっ子への距離を計算
        OldPosition = this.transform.position;
        //いじめられっ子のデータを取得
        izimerarekko = GameObject.FindGameObjectsWithTag("izimerarekko");
        //いじめ子リーダーのデータを取得
        izimekko_l = GameObject.FindGameObjectWithTag("izimekko_L");
        //普通の子のデータを取得
        hutuunoko = GameObject.FindGameObjectsWithTag("hutuunoko");
        if (izimepower >= 30)
        {
            switch (move_mode)
            {
                case move.loitering://徘徊
                    //いじめっ子と目標点との距離を求める
                    DistanceToTarget = Vector3.SqrMagnitude(transform.position - TargetPosition);
                    //目標点との距離が近ければ、その場でキョロキョロする動きをする
                   if (DistanceToTarget < CangeTargetDistance)
                    {
                       time3 = 0;
                       move_mode = move.sarchi;
                       break;
                    }
                    //目標点の方を向く
                    TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 2);
                    //前に進む
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    time2--;
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true && time2 <= 0){
                        time1 = 0;
                        move_mode = move.izimerarekko;
                        break;
                    }
                    float izimekko_Ldis = 0.0f;//いじめっ子リーダーとの距離
                    //izimekko_Ldis = Vector3.Distance(this.transform.position, izimekko_l.transform.position);
                    if (izimekko_Ldis < 20.0f){
                        //move_mode = move.izimekko_L;
                        break;
                    }
                    break;
                case move.sarchi://動かずにキョロキョロする
                    time4++;
                    if(time4>=50){
                        time4 = 0;
                        srach_angle *= -1.0f;
                    }
                    transform.Rotate(new Vector3(0.0f,srach_angle,0.0f));
                    time3++;
                    if (time3 >= 600) {//10秒経過したら探索に遷移
                        TargetPosition = GetPosition();
                        move_mode = move.loitering;
                        break;
                    }
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true && time2 <= 0){
                        time1 = 0;
                        move_mode = move.izimerarekko;
                        break;
                    }
                    break;
                case move.izimerarekko://いじめられっ子へ向かう
                    //目標点(いじめられっ子)の方を向く
                    TargetRotation = Quaternion.LookRotation(targetizimerarekko.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 10);
                    //目標点(いじめられっ子)に進む
                    transform.Translate(Vector3.forward * Speed * 1.5f * Time.deltaTime);
                    //視界からいじめられっ子がいなくなると立ち止まる
                    if (srachizimerarekko() == false){
                         time5=0;
                         move_mode = move.stop;
                         break;
                    }
                    //いじめられっ子に近づくとその場でいじめる
                    if (Vector3.Distance(targetizimerarekko.transform.position,transform.position) <= 2.0f)
                    {
                        switch (targetizimerarekko.gameObject.name)
                        {
                            case (string)"izimerarekko"://ずっといじめられっ子の場合
                            izimerarekko m_izimerarekko1=targetizimerarekko.GetComponent<izimerarekko>();
                            m_izimerarekko1.izimekko_count();
                            break;

                            case (string)"hutuunoko"://元が普通の子のいじめられっ子場合
                            hutuunoko m_izimerarekko2=targetizimerarekko.GetComponent<hutuunoko>();
                            m_izimerarekko2.izimekko_count();
                            break;
                        }
           

                        move_mode = move.izime;
                    }
                    time1++;
                    if (time1 >= 300){
                        time2 = 300;
                        TargetPosition = GetPosition();
                        move_mode = move.loitering;//徘徊
                        break;
                    }
                    break;
                case move.izimekko_L://いじめっ子リーダーに付きまとう
                    //目標点(いじめっ子リーダー)の方を向く
                    TargetRotation = Quaternion.LookRotation(izimekko_l.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 2);
                    //目標点(いじめっ子リーダー)に進む
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    break;
                case move.izime:
                    break;
                case move.stop:
                    time5++;
                    if (time5 > 120){
                        time1 = 0;
                        move_mode = move.loitering;
                        break;
                    }
                    //いじめられっ子が視界内にいるか
                    if (srachizimerarekko() == true)
                    {
                        time1 = 0;
                        move_mode = move.izimerarekko;
                        break;
                    }
                    break;
                default:
                    break;
            }
        }else{
            Destroy(this);
        }
        Objectcollision();
        Debug.Log(move_mode);
    }
    public Vector3 GetPosition(){
        return new Vector3(Random.Range(-22, 22), 0, Random.Range(-22, 22));//xとｚで-22～22までのランダムな地点を設定する
    }
    protected void OnCollisionStay(Collision collision){
        if (collision.gameObject.CompareTag("izimekko")){
                transform.Translate(Vector3.forward * -Speed * 1.5f * Time.deltaTime);
        }
    }
    public void mode_izimekko(){
        Update();
    }
    private bool srachizimerarekko(){
        float izimerarekkodis = 0.0f;//いじめられっ子への距離
        Vector3 izimerarekkovec;//いじめられっ子への方向ベクトル
        for (int i = 0; i < izimerarekko.Length; i++){
            izimerarekkodis = Vector3.Distance(this.transform.position, izimerarekko[i].transform.position);
            if (izimerarekkodis <= 5.0f){//いじめられっ子との距離が5.0f以内
                izimerarekkovec = izimerarekko[i].transform.position - this.transform.position;//いじめられっ子への方向ベクトルを求める
                if (Anglefor2Vector(izimerarekkovec, this.transform.forward) <= 22.5f){//進行方向といじめられっ子への方向ベクトルの内積が22.5f以内か
                    targetizimerarekko = izimerarekko[i];
                    
                    return true;
                }
            }
        }
        return false;
    }
    //二つのベクトルの内積を０～１８０度で求める関数
    float Anglefor2Vector(Vector3 vector1, Vector3 vector2){
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
    private void Objectcollision(){
        int i;
       
        for (i = 0; i < izimerarekko.Length; i++)
        {
            if (Vector3.Distance(izimerarekko[i].transform.position, transform.position) <= 2.0f)
            {
                Vector3 izimerrarekkovec = izimerarekko[i].transform.position - this.transform.position;
                transform.Translate(izimerrarekkovec * Speed * 0.1f * Time.deltaTime);
            }
        }
        for (i = 0; i < hutuunoko.Length; i++)
        {
            if (Vector3.Distance(hutuunoko[i].transform.position, transform.position) <= 2.0f)
            {
                Vector3 hutuunokovec = hutuunoko[i].transform.position - this.transform.position;
                transform.Translate(hutuunokovec * Speed * 0.1f * Time.deltaTime);
            }
        }
        if (Vector3.Distance(izimekko_l.transform.position, transform.position) <= 2.0f)
        {
            transform.Translate(Vector3.forward * -Speed * 1.5f * Time.deltaTime);
        }
        
    }

}
