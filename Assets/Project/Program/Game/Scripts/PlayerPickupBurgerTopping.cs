using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickupBurgerTopping : MonoBehaviour
{
	[SerializeField, Tooltip("init: 5")] float _MaxDistance;
	[SerializeField, Tooltip("init: Box")] LayerMask _HitLayers;
	GameObject _MainCamera;
	GameObject _ViewBox;
	RaycastHit _Hit;

	void Awake()
	{
		if (_MaxDistance <= 0)
		{
			_MaxDistance = 5.0f;
		}

		if (_HitLayers <= 0)
		{
			_HitLayers = 1 << LayerMask.NameToLayer("Box");
		}

		_MainCamera = Camera.main.gameObject;
		_ViewBox = null;
	}

	void Update()
	{
		Physics.Raycast(_MainCamera.transform.position, _MainCamera.transform.forward, out _Hit, _MaxDistance);

		if (CheckClickBurgerToppingBox())
		{
			if (_ViewBox == null) 
			{
				_ViewBox = _Hit.collider.gameObject;
			}

			_ViewBox.GetComponent<BurgerToppingBox>().SpawnToppingFromBox(_ViewBox);

			return;
		}

		_ViewBox = null;
	}

	bool CheckClickBurgerToppingBox()
	{
		return _Hit.collider != null && Input.GetMouseButtonDown(0) && 1 << _Hit.collider.gameObject.layer == _HitLayers;

	}
}
