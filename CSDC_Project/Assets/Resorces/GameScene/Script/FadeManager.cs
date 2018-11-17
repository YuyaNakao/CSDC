//**************************************************
//! @file   FadeManager.cs
//! @brief  フェードマネージャ
//! @data   2018 / 11 / 05
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

    public class FadeManager : MonoBehaviour {

        /*--- enum ---*/
        public enum FadeState {

            WAIT = 0,
            FADE_IN,
            FADE_OUT

        } // End of enum.

        /*--- メンバ変数宣言 ---*/
        [SerializeField]
        private FadeState m_fade_state = FadeState.WAIT;
        private Image m_fade_image;
        private Color m_fade_color = Color.black;
        private float m_fade_alpha = 1f;
        private bool m_fade_flag = false;

        [SerializeField, Range( 0.001f, 100f )]
        private float m_fade_time = 5f;
        private float m_fade_count = 0f;

        /// <summary>
        /// @brief  初期化処理
        /// </summary>
        void Start() {

            // ImageComponentの取得
            m_fade_image = GetComponent<Image>();

            // フェードのカラー初期化
            this.ChangeColor();

        } // End of Start.

        /// <summary>
        /// @brief  更新処理
        /// </summary>
        void Update() {

            // ステータスで処理を分岐
            switch( m_fade_state ) {
                
                case FadeState.FADE_IN:     this.FadeIn();  break;
                case FadeState.FADE_OUT:    this.FadeOut(); break;
                default: break;

            }

        } // End of Update.

        /// <summary>
        /// @brief  フェード・イン
        /// </summary>
        private void FadeIn() {

            // フェードカウントを回す
            m_fade_count += Time.deltaTime;
            m_fade_alpha = 1f - m_fade_count / m_fade_time;
            
            // 条件が来たらフェード・イン処理を止める
            if( m_fade_count >= m_fade_time ) {

                m_fade_alpha = 0f;
                m_fade_flag = true;
                m_fade_state = FadeState.WAIT;

            }

            // 色更新
            this.ChangeColor();

        } // End of FadeIn.

        /// <summary>
        /// @brief  フェード・アウト
        /// </summary>
        private void FadeOut() {

            // フェードカウントを回す
            m_fade_count += Time.deltaTime;
            m_fade_alpha = m_fade_count / m_fade_time;

            // 条件が来たらフェード・アウト処理を止める
            if( m_fade_count >= m_fade_time ) {

                m_fade_alpha = 1f;
                m_fade_flag = true;
                m_fade_state = FadeState.WAIT;

            }

            // 色更新
            this.ChangeColor();

        } // End of FadeOut.

        /// <summary>
        /// @brief  色を変更
        /// </summary>
        private void ChangeColor() {

            m_fade_color.a = m_fade_alpha;
            m_fade_image.color = m_fade_color;

        } // End of ChangeColor.

        /// <summary>
        /// @brief  ステータスを変更
        /// @brief  [in]FadeState : フェードステータス
        /// </summary>
        public void ChangeState( FadeState in_state ) {

            // フェードステートを取得
            m_fade_state = in_state;

            // フェード情報を初期化
            m_fade_count = 0f;
            m_fade_flag = false;
            switch( in_state ) {

                case FadeState.FADE_IN:
                    m_fade_alpha = 1f;
                    break;

                case FadeState.FADE_OUT:
                    m_fade_alpha = 0f;
                    break;

            }

        } // End of ChangeState.

        /// <summary>
        /// @brief  フェードの終了判定
        /// @param  none
        /// @return false - Play / true - End
        /// </summary>
        public bool IsFade() {

            return m_fade_flag;

        } // End of IsFade.

    } // End of class.

} // End of namespace.

//**************************************************
//! End of File.
//**************************************************