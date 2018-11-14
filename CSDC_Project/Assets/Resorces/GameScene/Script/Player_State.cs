using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;


public class Player_State : MonoBehaviour {
    public GameObject obj_Shot;
    Vector3 move;//移動量
    public float speed;

    //ステート管理
    public enum STATE {
        WAIT = 0,
        RUN,
        ATTACK,
        CHARGE
    }

    private STATE m_state = STATE.WAIT;

    private int m_number = 0;
    private CharacterController m_character_controller;
    PlayerStatus playerStatus;

    // Use this for initialization
    void Start () {
        m_character_controller = GetComponent<CharacterController>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update () {
        switch ( m_state ) {
            case STATE.WAIT:
                Wait();
                break;
            case STATE.RUN:
                Run();
                break;
            case STATE.ATTACK:
                Attack();
                break;
            case STATE.CHARGE:
                Charge();
                break;
        }
	}

    //待機時の処理
    private void Wait()
    {
        print("Wait");
        var keyState = GamePad.GetState(playerStatus.playerNo);

        m_state = STATE.RUN;

        if (keyState.A)
        {
            m_state = STATE.CHARGE;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_state = STATE.CHARGE;
        }
    }

    //移動中の処理
    private void Run()
    {
        print("Run");
        Move();
        m_state = STATE.WAIT;
    }

    //攻撃時の処理
    private void Attack()
    {
        // 弾を作成
        GameObject obj =Instantiate(obj_Shot, this.transform.position, this.transform.rotation)as GameObject;
        obj.name = obj_Shot.name;
        m_state = STATE.WAIT;
    }

    //溜め中の処理
    private void Charge()
    {
        ChargeMove();
        var keyState = GamePad.GetState(playerStatus.playerNo, false);

        if (keyState.A)
        {
            m_state = STATE.ATTACK;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_state = STATE.CHARGE;
        }
    }

    public void SetNumber( int num ) {
        m_number = num;
    }

    //通常時の移動
    public void Move()
    {
        var keyState = GamePad.GetState(playerStatus.playerNo, false);
        //プレイヤー移動
        move.x = keyState.LeftStickAxis.x * playerStatus.speed * Time.deltaTime;
        move.z = keyState.LeftStickAxis.y * playerStatus.speed * Time.deltaTime;

        Vector3 direction = new Vector3(move.x, move.y, move.z);
        m_character_controller.Move(direction);

        // 向きを変更
        transform.localRotation = Quaternion.LookRotation(direction);
    }

    //溜め中の移動
    public void ChargeMove()
    {

    //    //プレイヤー移動
    //    move.x = Input.GetAxis("Horizontal") * playerStatus.chargespeed;
    //    move.z = Input.GetAxis("Vertical") * playerStatus.chargespeed;
    //    Vector3 direction = new Vector3(move.x, move.y, move.z);
    //    m_character_controller.Move(direction);
    //    // 向きを変更
    //    transform.localRotation = Quaternion.LookRotation(direction);
    }
}