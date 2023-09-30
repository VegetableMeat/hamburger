using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
	[SerializeField, Tooltip("init: 100")] float _MoveSpeed;
	[SerializeField, Tooltip("init: 1.5")] float _DashSpeed;
	GameObject _MainCamera;
	Rigidbody _Rb;
	Vector3 _MoveDirection;

	void Awake()
	{
		if (_MoveSpeed <= 0)
		{
			_MoveSpeed = 100.0f;
		}

		if (_DashSpeed <= 0)
		{
			_DashSpeed = 1.5f;
		}

		_MainCamera = Camera.main.gameObject;
		_Rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		RotatePlayerTowardsCamera();
	}

	void FixedUpdate()
	{
		MovePlayer();
	}

	void RotatePlayerTowardsCamera()
	{
		// メインカメラの向いている方向にプレイヤーも向く
		_Rb.rotation = Quaternion.Euler(0, _MainCamera.transform.localEulerAngles.y, 0);
	}

	void MovePlayer()
	{
		float totalSpeed = _MoveSpeed * Time.deltaTime;
		totalSpeed *= Input.GetButton("Fire3") ? _DashSpeed : 1.0f;

		// WASDでプレイヤーを進ませる、カメラの向いている方向に進む
		_MoveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		_MoveDirection.Normalize();
		_Rb.velocity = transform.TransformDirection(_MoveDirection) * totalSpeed;
	}
}
