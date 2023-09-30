using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed;
	[SerializeField] float dashSpeed;
	GameObject mainCamera;
	Rigidbody rb;
	Vector3 moveDirection;

	void Awake()
	{
		if (dashSpeed <= 0)
		{
			dashSpeed = 1.5f;
		}
		mainCamera = Camera.main.gameObject;
		rb = GetComponent<Rigidbody>();
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
		// ���C���J�����̌����Ă�������Ƀv���C���[������
		rb.rotation = Quaternion.Euler(0, mainCamera.transform.localEulerAngles.y, 0);
	}

	void MovePlayer()
	{
		float totalSpeed = moveSpeed * Time.deltaTime;
		totalSpeed *= Input.GetButton("Fire3") ? dashSpeed : 1.0f;

		// WASD�Ńv���C���[��i�܂���A�J�����̌����Ă�������ɐi��
		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection.Normalize();
		rb.velocity = transform.TransformDirection(moveDirection) * totalSpeed;
	}
}
