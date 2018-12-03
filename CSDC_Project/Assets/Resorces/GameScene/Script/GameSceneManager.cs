//**************************************************
//! @file   GameSceneManager.cs
//! @brief  ゲームシーンマネージャ
//! @data   2018 / 11 / 05
//! @author Ponkiti1582
//**************************************************

//--------------------------------------------------
//! Declare Using.
//--------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace pon {

    public class GameSceneManager : MonoBehaviour {

        /*--- Enum ---*/
        public enum SceneState {

            FADE_IN = 0,
            PLAY,
            TIME_UP,
            FADE_OUT

        } // End of enum.

        /*--- メンバ変数宣言 ---*/
        private SceneState m_scene_state = SceneState.FADE_IN;
        private pon.FadeManager m_fade_manager;
        private pon.Timer m_timer;
        
        /// <summary>
        /// @brief  初期化処理
        /// </summary>
        private void Start() {
            
            // タイマーの取得
            m_timer = GameObject.Find( "Timer" ).GetComponent<pon.Timer>();
            
            // フェードマネージャの取得
            m_fade_manager = GameObject.Find( "FadeManager" ).GetComponent<pon.FadeManager>();

            // フェード・インの処理を起動
            m_fade_manager.ChangeState( FadeManager.FadeState.FADE_IN );

        } // End of Start.

        /// <summary>
        /// @brief  更新処理
        /// </summary>
        private void Update() {

            // シーンステータスで処理の分岐
            switch( m_scene_state ) {

                case SceneState.FADE_IN:    this.FadeIn();      break;
                case SceneState.PLAY:       this.Play();        break;
                case SceneState.TIME_UP:    this.TimeUp();      break;
                case SceneState.FADE_OUT:   this.FadeOut();     break;

            }

        } // End of Update.
        
        /// <summary>
        /// @brief  フェード・イン
        /// </summary>
        private void FadeIn() {

            // フェード・インの処理が終了したとき
            if( m_fade_manager.IsFade() ) {

                // ToDo : きゃらとかの起動をする

                // タイマー起動
                m_timer.Beginning();

                // シーンステータスを " カウントダウン " へ
                m_scene_state = SceneState.PLAY;

            }

        } // End of FadeIn.
        
        /// <summary>
        /// @brief  実行
        /// </summary>
        private void Play() {

            // タイマーが止まったとき
            if( m_timer.IsFinish() ) {

                // シーンステータスを " タイムアップ " へ
                m_scene_state = SceneState.TIME_UP;

            }

        } // End of Play.

        /// <summary>
        /// @brief  タイムアップ
        /// </summary>
        private void TimeUp() {

            // タイムアップの処理が終了したとき
            if( true ) {

                // ToDo : きゃらを止める処理

                // フェード・アウトの処理を起動
                m_fade_manager.ChangeState( FadeManager.FadeState.FADE_OUT );

                // シーンステータスを " フェード・アウト " へ
                m_scene_state = SceneState.FADE_OUT;

            }

        } // End of TimeUp.

        /// <summary>
        /// @brief  フェード・アウト
        /// </summary>
        private void FadeOut() {

            // フェード・アウトの処理が終了したとき
            if( m_fade_manager.IsFade() ) {
                
                // リザルトシーンの読み込み
                SceneManager.LoadScene( 3 );

            }

        } // End of FadeOut.

    } // End of class.

} // End of namespace.

//**************************************************
//! End of File.
//**************************************************