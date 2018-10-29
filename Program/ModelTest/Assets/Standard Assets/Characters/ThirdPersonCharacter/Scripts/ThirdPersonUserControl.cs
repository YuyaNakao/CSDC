using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

		//キャラクター
		public GameObject mainCharacter;
		public GameObject target;

		//キャラクターコントローラー宣言
		private CharacterController characterController;
		//Animator宣言
		private Animator animator;

		//引張ハンティングフラグ関連
		public bool pullFlag = false;	//引っ張り始めたフラグ
		public bool popFlag = false;	//弾け飛ばすフラグ

		public float speed = 1.0f;
		float moveX = 0f;
		float moveZ = 0f;
		Rigidbody rb;


        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
			//Animatorを確保
			characterController = GetComponent <CharacterController> ();
			animator = GetComponent <Animator> ();
			rb = GetComponent<Rigidbody> ();
        }


        private void Update()
        {
            /* ジャンプより削除
             if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            */

			//1フレームだけTrueにしたいため、毎回最初にfalseを入れる。
			popFlag = false;


			//スペースの入力で引張ハンティングスタート
			if (Input.GetKey (KeyCode.Space)) {
				animator.SetBool ("pullFlag", true);
				pullFlag = true;	//ひっぱり開始の合図
			} else {
				animator.SetBool ("pullFlag", false);
				pullFlag = false;	//ひっぱり終了
				popFlag = true;		//発射フラグを立てる。マイフレームリセットしてるから、一フレームだけ通るはず？？？
			}

			//pull中はキャラクターの向きを反転させる　→　☓
			//ターゲットの向きを向いて歩きたい →　○
			//pullFlgを持っていればターゲットの方を向き続ける。
			if (pullFlag) {
				mainCharacter.transform.LookAt (target.transform);
			} 


			//移動中はAnimationを再生させておく。
			//移動指定なくて、かつ、引っ張っているならばAnimationを止める。
			//AnimationSpeed->Speed->MultPlierに影響させている。
			if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f ) {	
				//animator.SetFloat("AnimationSpeed", 1.0f);
			} else if(pullFlag){
				animator.SetFloat("AnimationSpeed", 0.0f);
			}

			//移動スクリプト
			moveX = Input.GetAxis ("Horizontal") * speed;
			moveZ = Input.GetAxis ("Vertical") * speed;
			Vector3 direction = new Vector3(moveX , 0, moveZ);


        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {


            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
			if (pullFlag) {

				//前なら逆再生させる

				if (Input.GetKey (KeyCode.W)) {
					transform.position += transform.forward * speed * Time.deltaTime;
					animator.SetFloat("AnimationSpeed", -1);
				}
				if (Input.GetKey (KeyCode.S)) {
					transform.position -= transform.forward * speed * Time.deltaTime;
					animator.SetFloat("AnimationSpeed", 1);
				}
				if (Input.GetKey(KeyCode.D)) {
					transform.position += transform.right * speed * Time.deltaTime;
					animator.SetFloat("AnimationSpeed", 1);
				}
				if (Input.GetKey (KeyCode.A)) {
					transform.position -= transform.right * speed * Time.deltaTime;
					animator.SetFloat("AnimationSpeed", -1);
				}

			} else {

				m_Character.Move (m_Move, crouch, m_Jump);
			}
			m_Jump = false;
        }
    }
}
