using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerToppingRay : MonoBehaviour
{
	[SerializeField, Tooltip("init: 0.05")] float _MaxDistance;
	[SerializeField, Tooltip("init: Grab")] LayerMask _HitLayers;
	string[] _Tags;
	string _TagMaikingburger;
	string _TagBunsUnder;
	RaycastHit _Hit;
	MakeHamburger _MakeHamScript;

	void Awake()
	{
		if (_MaxDistance <= 0)
		{
			_MaxDistance = 0.05f;
		}

		if (_HitLayers <= 0)
		{
			_HitLayers = 1 << LayerMask.NameToLayer("Grab");
		}

		_TagMaikingburger = "Maikingburger";
		_TagBunsUnder = "BunsUnder";
		_Tags = new string[] { _TagMaikingburger, _TagBunsUnder };
		_MakeHamScript = GameObject.Find("GameManager").GetComponent<MakeHamburger>();
	}

	void Update()
	{
		Vector3 rayPos = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
		Physics.Raycast(rayPos, -transform.up, out _Hit, _MaxDistance);

		if (_Hit.collider == null)
		{
			return;		
		}

		GameObject hitObj = _Hit.collider.gameObject;

		if (1 << hitObj.layer == _HitLayers && CheckUnCompleteBurgerTag(hitObj))
		{
			BunsRay bunsScript = hitObj.GetComponent<BunsRay>();

			if (bunsScript.GetBurgerState())
			{
				gameObject.GetComponent<BurgerToppingRay>().enabled = false;
				_MakeHamScript.RemakeTheHamburger(bunsScript.GetBurgerState(), hitObj, gameObject);
			}
		}
	}

	bool CheckUnCompleteBurgerTag(GameObject hitObj) 
	{
		foreach(string tag in _Tags)
		{
			if (hitObj.tag == tag)
			{
				return true;
			}
		}

		return false;
	}
}
