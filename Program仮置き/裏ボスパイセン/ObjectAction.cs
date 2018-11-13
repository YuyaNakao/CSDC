using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAction : MonoBehaviour {
    [SerializeField, Tooltip("参照する場所")]
    int placeNo;

    [SerializeField, Tooltip("何フレーム間動かすか")]
    float action_sec;

    [SerializeField, Tooltip("振り幅")]
    float swing_wigth;

    [SerializeField, Tooltip("移動速度")]
    float speed;

    private CameraMove cameraMove;      // カメラスクリプトの取得
    private float frame = 0;            // フレーム数
    private Vector3 firstPos;           // 初期位置
    private bool near = false;          // カメラに近づいてるかのフラグ

    // Use this for initialization
    void Start () {
        // カメラスクリプトの取得
        cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        // 初期位置設定
        firstPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // 参照カメラ状態が待機状態かどうか
        if (CameraMove.CAMERA_FLG.STAY == cameraMove.GetCameraFlg(placeNo))
        {
            // 指定された時間を過ぎてない時
            if(frame < action_sec)
            {
                // 対象オブジェクトを動かす
                ObjMove();
            }
            else
            {
                // 初期位置に戻す
                this.transform.position = firstPos;
            }
        }
	}


    //=====================================================
    //  対象オブジェクトの移動処理
    //=====================================================
    void ObjMove()
    {
        // カメラから遠のくフラグである、かつ、最大距離に到達していない時
        if (!near && this.transform.position.z < firstPos.z + swing_wigth)
        {
            // 対象オブジェクトを動かす
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + speed);
        }
        else
        {
            // カメラに近づくフラグに変更する
            near = true;
        }

        // カメラから遠のくフラグである、かつ、最大距離に到達していない時
        if (near && this.transform.position.z > firstPos.z - swing_wigth)
        {
            // 対象オブジェクトを動かす
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - speed);
        }
        else
        {
            // カメラから遠のくフラグに変更する
            near = false;
        }

        // フレーム数を足す
        frame++;
    }
}
