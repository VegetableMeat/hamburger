using Unity.VisualScripting;
using UnityEngine;

public class BunsRay : MonoBehaviour
{
	[SerializeField, Tooltip("init: 0.05")] float _MaxDistance;
	[SerializeField, Tooltip("init: Table")] LayerMask _HitLayers;
	bool _IsBurgerOntable;
	RaycastHit _Hit;

	void Awake()
	{
		if (_MaxDistance <= 0)
		{
			_MaxDistance = 0.05f;
		}

		if (_HitLayers <= 0)
		{
			_HitLayers = 1 << LayerMask.NameToLayer("Table");
		}
	}

	void Update()
	{
		Vector3 rayPos = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
		Physics.Raycast(rayPos, -transform.up, out _Hit, _MaxDistance);

		if (_Hit.collider != null && 1 << _Hit.collider.gameObject.layer == _HitLayers)
		{
			_IsBurgerOntable = true;
			return;
		}
		_IsBurgerOntable = false;
	}

	public bool GetBurgerState() 
	{
		return _IsBurgerOntable;
	}
}
