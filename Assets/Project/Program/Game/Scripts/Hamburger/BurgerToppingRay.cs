using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerToppingRay : MonoBehaviour
{
	[SerializeField, TooltipAttribute("init: 0.05")] float maxDistance;
	[SerializeField] LayerMask hitLayers;
	RaycastHit hit;
	MakeHamburger makeHamScript;

	void Awake()
	{
		if (maxDistance <= 0)
		{
			maxDistance = 0.05f;
		}

		if (hitLayers <= 0)
		{
			hitLayers = 1 << LayerMask.NameToLayer("Grab");
		}

		makeHamScript = GameObject.Find("GameManager").GetComponent<MakeHamburger>();
	}

	void Update()
	{
		Vector3 rayPos = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
		Physics.Raycast(rayPos, -transform.up, out hit, maxDistance);

		if (hit.collider == null)
		{
			return;		
		}

		GameObject hitObj = hit.collider.gameObject;

		if (1 << hitObj.layer == hitLayers && (hitObj.tag == "Maikingburger" || hitObj.tag == "BunsUnder"))
		{
			BunsRay bunsScript = hitObj.GetComponent<BunsRay>();

			if (bunsScript.GetBurgerState())
			{
				gameObject.GetComponent<BurgerToppingRay>().enabled = false;
				makeHamScript.RemakeTheHamburger(bunsScript.GetBurgerState(), hitObj, gameObject);
			}
		}
	}
}
