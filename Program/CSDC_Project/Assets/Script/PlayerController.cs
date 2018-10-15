using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    float moveX = 0.0f;
    float moveZ = 0.0f;

    public GameObject[] Beacon;
    public GameObject obj;
    public GameObject face;
    Vector3 face_size;

    int beacon_count = 0;
    CharacterController controller;

    void UpdateKey()
    {
        //ビーコン設置上限
        if (beacon_count < Beacon.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //ビーコン設置
                SetBeacon();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //ビーコン消去
            Reset();
        }
    }
    void SetBeacon()
    {
        if (beacon_count > Beacon.Length) return;
        //ビーコン生成
        Beacon[beacon_count]=Instantiate(obj, this.transform.position, Quaternion.identity);
        Beacon[beacon_count].name = "beacon" + beacon_count;
        beacon_count++;

        //　矩形生成
        if (beacon_count == Beacon.Length)
        {
            face.SetActive(true);
            face_size = Beacon[0].transform.position - Beacon[1].transform.position;
            face.transform.localScale = face_size;
            face.transform.position = Beacon[1].transform.position + (face_size / 2.0f);
        }
    }

    void Reset()
    {
        for (int i = 0; i < beacon_count; i++) {
            //ビーコン消去
            Destroy(Beacon[i]);
        }
        face.SetActive(false);
        beacon_count = 0;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        face.SetActive(false);
    }

    void Update()
    {
        UpdateKey();
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        controller.SimpleMove(direction);
    }


}