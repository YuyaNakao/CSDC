using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour {
    public GameObject Shot;
    public GameObject Beacon;
    GameObject newBeacon;
    Vector3 move;
    Vector3 playerBpos;
    Shot shot;
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
        //m_player_status.GetHP();
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
        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S))
            m_state = STATE.RUN;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            m_state = STATE.RUN;

        if (Input.GetKey(KeyCode.Space))
        {
            m_state = STATE.CHARGE;
            newBeacon=Instantiate(Beacon, this.transform.position, this.transform.rotation);
            newBeacon.name = Beacon.name;
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
        Instantiate(Shot, this.transform.position, this.transform.rotation);
        Destroy(newBeacon);
        m_state = STATE.WAIT;

    }

    //溜め中の処理
    private void Charge()
    {
        m_state = STATE.CHARGE;
        ChargeMove();

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
        move.x = Input.GetAxis("Horizontal") * playerStatus.speed;
        move.z = Input.GetAxis("Vertical") * playerStatus.speed;

        Vector3 direction = new Vector3(move.x, move.y, move.z);
        m_character_controller.Move(direction);
        transform.localRotation = Quaternion.LookRotation(direction);
    }

    //溜め中の移動
    public void ChargeMove()
    {
        move.x = Input.GetAxis("Horizontal") * playerStatus.chargespeed;
        move.z = Input.GetAxis("Vertical") * playerStatus.chargespeed;

        Vector3 direction = new Vector3(move.x, move.y, move.z);
        m_character_controller.Move(direction);
        transform.localRotation = Quaternion.LookRotation(direction);
    }
}