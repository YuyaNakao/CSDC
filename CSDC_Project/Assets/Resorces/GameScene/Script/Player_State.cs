//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;


public class Player_State : MonoBehaviour {
    public GameObject obj_Shot;
    Vector3 move;//移動量
    Vector3 direction; //向き

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

        GamepadState keyState = GamePad.GetState(playerStatus.playerNo);
        if (GamePad.GetButtonDown(GamePad.Button.A, playerStatus.playerNo))
        {
            m_state = STATE.CHARGE;
        }
        if (keyState.LeftStickAxis.x>0.2f)
        {
            m_state = STATE.RUN;
        }

        if (keyState.LeftStickAxis.x <-0.2f)
        {
            m_state = STATE.RUN;
        }

        if (keyState.LeftStickAxis.y > 0.2f) { 
            m_state = STATE.RUN;
        }

        if (keyState.LeftStickAxis.y <-0.2f)
        {
            m_state = STATE.RUN;
        }
    }

    //移動中の処理
    private void Run()
    {
        Move();
        m_state = STATE.WAIT;
        GamepadState keyState = GamePad.GetState(playerStatus.playerNo);
        if (GamePad.GetButtonDown(GamePad.Button.A, playerStatus.playerNo))
        {
            m_state = STATE.CHARGE;
        }


    }

    //攻撃時の処理
    private void Attack()
    {
        // 弾を作成
        GameObject obj =Instantiate(obj_Shot,transform.position,transform.rotation)as GameObject;
        obj.name = obj_Shot.name;
        m_state = STATE.WAIT;
    }

    //溜め中の処理
    private void Charge()
    {
        ChargeMove();
        GamepadState keyState = GamePad.GetState(playerStatus.playerNo, false);

        if (GamePad.GetButtonUp(GamePad.Button.A, playerStatus.playerNo))
        {
            m_state = STATE.ATTACK;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_state = STATE.ATTACK;
        }
    }

    public void SetNumber( int num ) {
        m_number = num;
    }

    //通常時の移動
    public void Move()
    {
        GamepadState keyState = GamePad.GetState(playerStatus.playerNo);
        //プレイヤー移動
        move.x = keyState.LeftStickAxis.x * playerStatus.speed * Time.deltaTime;
        move.z = keyState.LeftStickAxis.y * playerStatus.speed * Time.deltaTime;

        direction = new Vector3(move.x, move.y, move.z);
        this.transform.Rotate(direction);
        m_character_controller.Move(direction);

        // 向きを変更
        transform.localRotation = Quaternion.LookRotation(direction);
    }
    //溜め中の移動
    public void ChargeMove()
    {
        //移動量から向きを計算
        //Vector3 diff = transform.position - playerStatus.pos;
        // 向きを変更
        transform.localRotation = Quaternion.LookRotation(direction);
    }
}