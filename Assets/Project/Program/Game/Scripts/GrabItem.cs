using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class GrabItem : MonoBehaviour
{
	Rigidbody _Rb;
	GameObject _Manipulator;
	Vector3 _HitPoint;

	void Awake()
	{
        _Rb = GetComponent<Rigidbody>();
	}

	public void Touch(GameObject camera, Vector3 hit) 
	{
        _Manipulator = camera;
        _HitPoint = transform.InverseTransformPoint(hit);
	}

	public void Hold()
	{
        _Rb.velocity = (_Manipulator.transform.forward + _Manipulator.transform.position - transform.TransformPoint(_HitPoint)) * 10.0f;
        _Rb.rotation = Quaternion.Euler(0, _Manipulator.transform.localEulerAngles.y, 0);
	}
}
