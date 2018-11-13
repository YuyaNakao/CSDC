using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Use this for initialization

    [SerializeField]
    private float camera_x; // カメラのＸ座標

    [SerializeField]
    private float camera_y; // カメラのＹ座標

    [SerializeField]
    private float camera_z; // カメラのＺ座標

    [SerializeField]
    private float camera_z_max; // カメラのＺ軸を動かす時の最大値

    [SerializeField]
    private Vector3 camera_object;

    [SerializeField]
    private GameObject camera;      // カメラの座標
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Camera_Move();
    }

    void Camera_Move()
    {
        
        camera_object = camera.transform.position;
        camera_object.z += camera_z;
        camera.transform.position = camera_object;
    }
}
