using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;      // カメラの座標

    [SerializeField]
    private float camera_x; // カメラのＸ座標

    [SerializeField]
    private float camera_y; // カメラのＹ座標

    [SerializeField]
    private float camera_z; // カメラのＺ座標

    [SerializeField]
    private float camera_zoom_speed;        // カメラのズーム速度

    [SerializeField]
    private float camera_rotate_spped;      // カメラの回転速度

    [SerializeField]
    private float camera_y_max; // カメラのＹ軸を動かす時の最大値

    [SerializeField]
    private float camera_z_max; // カメラのＺ軸を動かす時の最大値

    [SerializeField]
    private float remove_camera_y;  // カメラのＹ軸を元の位置に戻す

    [SerializeField]
    private float remove_camera_z;  // カメラのＺ軸を元の位置に戻す

    [SerializeField]
    private float rotate_cameramax_x;  // カメラの回転座標を動かす

    [SerializeField]
    private Vector3 camera_object;  // カメラの座標オブジェクト

    [SerializeField]
    private Vector3 camera_rotate_object;       // カメラの回転オブジェクト

    [SerializeField]
    private float StartTime;       // カメラの持続時間

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 時間経過処理
        StartTime -= Time.deltaTime;
        // 一定時間経過すると処理に入る
        if (StartTime <= 17 && StartTime >= 10)
        {
            Camera_Move();
        }

        if (StartTime <= 10 && StartTime >= 5)
        {
            Camera_Remove();
        }

        if(StartTime <= 5)
        {
            Camera_Endmove();
        }

        // 時間が０になった時
        if(StartTime < 0)
        {
            // タイトルへシーンが遷移する。
            Fade.FadeOut(0);
        }

    }

    void Camera_Move()
    {
        // 一定の距離までズームする
        if (camera_object.z <= camera_z_max)
        {
            camera_object = camera.transform.position;
            camera_z += camera_zoom_speed;
            camera_object.z += camera_z;
            camera.transform.position = camera_object;
        }

        if (camera_object.y <= camera_y_max)
        {
            camera_object = camera.transform.position;
            camera_y += camera_zoom_speed;
            camera_object.y += camera_y;
            camera.transform.position = camera_object;
        }
    }

    void Camera_Remove()
    {
        // 一定距離までズームアップする
        if (camera_object.z >= remove_camera_z)
        {
            camera_object = camera.transform.position;
            camera_z += camera_zoom_speed;
            camera_object.z -= camera_z;
            camera.transform.position = camera_object;
        }

        if (camera_object.y >= remove_camera_y)
        {
            camera_object = camera.transform.position;
            camera_y += camera_zoom_speed;
            camera_object.y -= camera_y;
            camera.transform.position = camera_object;
        }
    }

    void Camera_Endmove()
    {
        if(camera_rotate_object.x >= rotate_cameramax_x )
        {
            camera.transform.Rotate(new Vector3(camera_rotate_spped,0,0));
            camera_rotate_object.x += camera_rotate_spped;
        }
    }
}
