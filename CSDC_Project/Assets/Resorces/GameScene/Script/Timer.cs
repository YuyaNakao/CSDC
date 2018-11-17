//**************************************************
//! @file   Timer.cs
//! @brief  タイマー
//! @data   2018 / 11 / 10
//! @author Ponkiti1582
//**************************************************

//--------------------------------------------------
//! Declare Using.
//--------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pon {

    public class Timer : StatusControl {

        /*--- enum ---*/


        /*--- メンバ変数宣言 ---*/
        [SerializeField] private float m_minute = 0f;   // 分
        [SerializeField] private float m_second = 0f;   // 秒

        private float m_finish_time = 0f;               // 終了時間 ( 秒 )

        [SerializeField]
        private Sprite[] m_timer_sprites = new Sprite[10];  // タイマーの画像
        private Image[] m_timer_elements = new Image[4];    // タイマーの要素
        
        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void Start() {

            // 親の処理を実行
            base.Start();

            // タイマー要素を取得
            m_timer_elements[0] = transform.GetChild( 1 ).GetComponent<Image>();
            m_timer_elements[1] = transform.GetChild( 2 ).GetComponent<Image>();
            m_timer_elements[2] = transform.GetChild( 3 ).GetComponent<Image>();
            m_timer_elements[3] = transform.GetChild( 4 ).GetComponent<Image>();
            
            // 終了時間設定 ( 秒変換 )
            m_finish_time = m_minute * 60f + m_second;

            // スプライトの更新
            this.UpdateSprite();

        } // End of Start.

        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void Update() {

            // 親の処理を実行
            base.Update();

        } // End of Update.

        /// <summary>
        /// 待ち処理
        /// </summary>
        protected override void Wait() {

            // 親の処理を実行
            base.Wait();

        } // End of Wait.

        /// <summary>
        /// 実行処理
        /// </summary>
        protected override void Run() {
            
            // タイマーをカウントダウンする
            m_finish_time -= Time.deltaTime;
            
            // 指定条件を達成したら処理
            if( m_finish_time <= 0f ) {
                
                // 実行処理終了関数呼び出し
                base.FinishRun();

            }

            // スプライトの更新
            this.UpdateSprite();

        } // End of Run.

        /// <summary>
        /// スプライトの更新
        /// </summary>
        private void UpdateSprite() {

            // 描画スプライトの更新
            int minute = ( int )( m_finish_time / 60f );
            int second = ( int )( m_finish_time - minute * 60f );

            m_timer_elements[0].sprite = m_timer_sprites[minute / 10];
            m_timer_elements[1].sprite = m_timer_sprites[minute % 10];
            m_timer_elements[2].sprite = m_timer_sprites[second / 10];
            m_timer_elements[3].sprite = m_timer_sprites[second % 10];

        } // End of UpdateSprite.

    } // End of class.

} // End of namespace.

//**************************************************
//! End of File.
//**************************************************