//using UnityEngine;
//using UnityEngine.EventSystems;

//public class PlayerController : MonoBehaviour
//{
//    public float moveSpeed;
//	public float jumpSpeed;
//	public float gravity;
//	public GameObject mainCamera;
//    private CharacterController controller;
//	private Vector3 moveDirection;

//	void Start()
//	{
//        controller = GetComponent<CharacterController>();
//		mainCamera = Camera.main.gameObject;
//	}

//	void Update()
//    {
//		PlayerMoving();
//	}

//    void PlayerMoving()
//    {
//		// メインカメラの向いている方向にプレイヤーも向く
//		transform.rotation = Quaternion.Euler(0, mainCamera.transform.localEulerAngles.y, 0);

//		// 地面についている時
//		if (controller.isGrounded)
//		{
//			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//			moveDirection = transform.TransformDirection(moveDirection) * moveSpeed;

//			// ジャンプ↓
//			if (Input.GetButton("Jump"))
//			{
//				moveDirection.y = jumpSpeed;
//			}
//		}

//		// 重力分変更する
//		moveDirection.y -= gravity * Time.deltaTime;
//		controller.Move(moveDirection * Time.deltaTime);
//	}
//}
