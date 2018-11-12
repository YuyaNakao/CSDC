using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Score : MonoBehaviour
{
    private float move_score;       // スコアの移動量
    private float player_max_move;  // 数値が大きくなるので調整用の変数
    private Vector3 player_size;
    [SerializeField]
    private Vector3 init_position;  // スコアの初期座標

    [SerializeField]
    private float x;

    [SerializeField]
    private float z;

    [SerializeField]
    private float player_score;     // プレイヤーのスコア

    [SerializeField]
    private GameObject player;      // それぞれのプレイヤーオブジェクト

    [SerializeField]
    private int number;

    [SerializeField]
    int numbers1;
    [SerializeField]
    int numbers2;
    [SerializeField]
    int numbers3;
    [SerializeField]
    int numbers4;

    private Vector3 py;//Y座標一時保管

    // Use this for initialization
    void Start()
    {
        // オブジェクトのスケールサイズが大きくなるので調整する
        player_max_move = player_score / 40;
        // 移動量の初期値
        move_score = 0;
        py = init_position;
        
    }

    // Update is called once per frame
    void Update()
    {
        // スコアの増加量を関数見る
        Move_Score();
    }

    void Move_Score()
    {
        //プレイヤーのスコアがスコアの移動量に達していない時
        if (move_score < player_max_move)
        {
            // グラフが上昇し続ける
            move_score += 0.01f;
            // 数値を直接代入できないので、Vector3型のplayer_sizeに変換する
            player_size = player.transform.localScale;

            player_size.y = move_score;
            // グラフのオブジェクトのＹ軸も加算する。
            player.transform.localScale = player_size;

            py.y = move_score / 2 + init_position.y;

            player.transform.position = (new Vector3(x, py.y, z));

        }
    }
}
