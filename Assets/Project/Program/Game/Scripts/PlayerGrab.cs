using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGrab : MonoBehaviour
{
	[SerializeField, Tooltip("init: 5")] float _MaxDistance;
	[SerializeField, Tooltip("init: Grab")] LayerMask _HitLayers;
	GameObject _MainCamera;
	GameObject _GrabItem;
	RaycastHit _Hit;

	void Awake()
	{
		if (_MaxDistance <= 0)
		{
			_MaxDistance = 5.0f;
		}

		if (_HitLayers <= 0)
		{
			_HitLayers = 1 << LayerMask.NameToLayer("Grab");
		}

		_MainCamera = Camera.main.gameObject;
		_GrabItem = null;
	}

	void Update()
	{
		Physics.Raycast(_MainCamera.transform.position, _MainCamera.transform.forward, out _Hit, _MaxDistance);

		if (CheckClickGrabItem())
		{
			if (_GrabItem == null)
			{
				_GrabItem = _Hit.collider.gameObject;
				_GrabItem.GetComponent<GrabItem>().Touch(_MainCamera, _Hit.point);
			}
			else if (_GrabItem.GetComponent<GrabItem>())
			{
				_GrabItem.GetComponent<GrabItem>().Hold();
			}

			return;
		}

		_GrabItem = null;
	}

	bool CheckClickGrabItem()
	{
		if (_Hit.collider != null && Input.GetMouseButtonDown(0) && 1 << _Hit.collider.gameObject.layer == _HitLayers || _GrabItem)
		{
			if (Input.GetMouseButton(0))
			{
				return true;
			}
		}

		return false;
	}
}
