using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour{
    [SerializeField, Tooltip("場所リスト")]
    List<PLACE> placeList = new List<PLACE>();

    [SerializeField, Tooltip("カメラと映す物体の距離")]
    float distance;

    // カメラの状態
    public enum CAMERA_FLG
    {
        NOT_SET = 0,    // 未設定状態
        MOVE,           // 移動状態
        END,            // 終了状態
        STAY            // 待機状態
    }
    
    [System.Serializable]
    // 移動場所の構造体
    private struct PLACE
    {
        public Vector3 changePlace; // 移動後のポジション（1番最初は初期位置になる）
        public float moveSec;       // 何フレームで次のポジションに移るか
        public float staySec;       // 何フレーム間待機するか
        public CAMERA_FLG flg;      // カメラの状態
    }
    
    private int frame = 0;
    private int placeNo = 0;

	// Use this for initialization
	void Start () {
        // カメラの初期位置設定
        if(placeList.Count > 0)
        {
            this.transform.position = new Vector3(placeList[0].changePlace.x, placeList[0].changePlace.y, placeList[0].changePlace.z - distance);
        }
	}
	
	// Update is called once per frame
	void Update () {
        // リストが終わるまで繰り返す
        if(placeNo < placeList.Count - 1)
        {
            Stay(); // 待機処理
            Move(); // 移動処理
        }
    }


    //=====================================================
    //  カメラの移動処理
    //=====================================================
    void Move()
    {
        Vector3 newPlace = new Vector3(0, 0, 0);
        // 移動中処理
        if (placeList[placeNo].flg == CAMERA_FLG.MOVE)
        {
            // 線形補間
            newPlace.x = placeList[placeNo].changePlace.x + (placeList[placeNo + 1].changePlace.x - placeList[placeNo].changePlace.x) * frame / placeList[placeNo].moveSec;
            newPlace.y = placeList[placeNo].changePlace.y + (placeList[placeNo + 1].changePlace.y - placeList[placeNo].changePlace.y) * frame / placeList[placeNo].moveSec;
            newPlace.z = placeList[placeNo].changePlace.z + (placeList[placeNo + 1].changePlace.z - placeList[placeNo].changePlace.z) * frame / placeList[placeNo].moveSec;

            // オブジェクトとカメラの距離を追加
            newPlace.z -= distance;

            // カメラの位置を移動
            this.transform.position = newPlace;
            frame++;

            // 移動時間が終了したときの処理
            if (frame > placeList[placeNo].moveSec)
            {
                // frameを初期化
                frame = 0;

                // Listの構造体は直で値を書き換えられないので書き換え用変数を宣言
                PLACE placeData;

                // 現在のデータを書き換え用変数に入れ込む
                placeData = placeList[placeNo];
                // 移動状態から終了状態へ
                placeData.flg = CAMERA_FLG.END;
                // 値の書き換え
                placeList[placeNo] = placeData;

                // 次の移動場所へ
                placeNo++;
                // 現在のデータを書き換え用変数に入れ込む
                placeData = placeList[placeNo];
                // 未設定から待機状態へ
                placeData.flg = CAMERA_FLG.STAY;
                // 値の書き換え
                placeList[placeNo] = placeData;
            }
        }
    }


    //=====================================================
    //  カメラの待機処理
    //=====================================================
    void Stay()
    {
        if (placeList[placeNo].flg == CAMERA_FLG.STAY)
        {
            // 待機時間を超えてない時
            if (frame < placeList[placeNo].staySec)
            {
                frame++;
            }
            else
            {
                // frameを初期化
                frame = 0;

                // Listの構造体は直で値を書き換えられないので書き換え用変数を宣言
                PLACE placeData;
                
                // 現在のデータを書き換え用変数に入れ込む
                placeData = placeList[placeNo];
                // 待機状態から移動状態へ
                placeData.flg = CAMERA_FLG.MOVE;
                // 値の書き換え
                placeList[placeNo] = placeData;
            }
        }
    }



    //=====================================================
    //  カメラ状態の取得
    //=====================================================
    public CAMERA_FLG GetCameraFlg(int no)
    {
        return placeList[no].flg;
    }

    
    //=====================================================
    //  初期位置の状態を未設定から待機状態に
    //=====================================================
    public void SetStay()
    {
        // Listの構造体は直で値を書き換えられないので書き換え用変数を宣言
        PLACE placeData;
        // 現在のデータを書き換え用変数に入れ込む
        placeData = placeList[placeNo];
        // 設定なしから待機状態へ
        placeData.flg = CAMERA_FLG.STAY;
        // 値の書き換え
        placeList[placeNo] = placeData;
    }
}
