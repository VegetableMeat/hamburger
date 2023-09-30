using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class GrabItem : MonoBehaviour
{
    Rigidbody rb;
    GameObject manipulator;
    Vector3 hitPoint;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Touch(GameObject camera, Vector3 hit) 
    {
		manipulator = camera;
		hitPoint = transform.InverseTransformPoint(hit);
    }

	public void Hold()
    {
        rb.velocity = (manipulator.transform.forward + manipulator.transform.position - transform.TransformPoint(hitPoint)) * 10.0f;
		rb.rotation = Quaternion.Euler(0, manipulator.transform.localEulerAngles.y, 0);
	}
}
