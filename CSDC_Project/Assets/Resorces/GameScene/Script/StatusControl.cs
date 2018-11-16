//**************************************************
//! @file   StatusControl.cs
//! @brief  ステータス制御
//! @data   2018 / 11 / 10
//! @author Ponkiti1582
//**************************************************

//--------------------------------------------------
//! Declare Using.
//--------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pon {

    public class StatusControl : MonoBehaviour {

        /*--- enum ---*/
        private enum State {

            STOP = 0,
            WAIT,
            RUN

        } // End of enum.

        /*--- メンバ変数宣言 ---*/
        private State m_state = State.STOP;     // ステート変数

        [SerializeField]
        private float m_wait_time = 0f;         // 待ち時間
        private float m_wait_time_count = 0f;   // 待ち時間カウント

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected virtual void Start() {} // End of Start.

        /// <summary>
        /// 更新処理
        /// </summary>
        protected virtual void Update() {

            // ステータスで処理を分岐
            switch( m_state ) {

                case State.WAIT:    this.Wait();    break;
                case State.RUN:     this.Run();     break;
                default:                            break;

            }

        } // End of Update.

        /// <summary>
        /// 待ち処理
        /// </summary>
        protected virtual void Wait() {

            // 待ち時間カウントの更新
            m_wait_time_count += Time.deltaTime;

            // 指定条件を達成したら処理
            if( m_wait_time_count >= m_wait_time ) {

                // 待ち時間カウントを初期化
                m_wait_time_count = 0f;

                // ステータスを実行状態に遷移
                m_state = State.RUN;

            }

        } // End of Wait.

        /// <summary>
        /// 実行処理
        /// </summary>
        protected virtual void Run() {} // End of Run.

        /// <summary>
        /// 実行処理終了
        /// </summary>
        protected void FinishRun() {

            // ステータスを停止状態に遷移
            m_state = State.STOP;

        } // End of FinishRun.

        /// <summary>
        /// スタートする
        /// </summary>
        public void Beginning() {

            // ステータスを待ち状態に遷移
            m_state = State.WAIT;

        } // End of Beginning.

        /// <summary>
        /// 終了判定
        /// </summary>
        /// <returns>false - Run / true - End</returns>
        public bool IsFinish() {

            return ( m_state == State.STOP );

        } // End of IsFinish.

    } // End of class.

} // End of namespace.

//**************************************************
//! End of File.
//**************************************************