using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PattyRay : MonoBehaviour
{
	[SerializeField, TooltipAttribute("init: 0.1")] float maxDistance;
	[SerializeField] LayerMask hitLayers;
	RaycastHit hit;
	MakeHamburger makeHamScript;

	void Awake()
	{
		if (maxDistance < 0) 
		{
			maxDistance = 0.1f;
		}
		makeHamScript = GameObject.Find("GameManager").GetComponent<MakeHamburger>();
	}

	void Update()
	{
		Vector3 rayPos = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
		if (!Physics.Raycast(rayPos, -transform.up, out hit, maxDistance, hitLayers))
		{
			return;
		}
		// Debug—p
		//Debug.DrawRay(rayPos, -transform.up * maxDistance, Color.blue, 0.1f, false);
		GameObject hitObj = hit.collider.gameObject;

		if (hitObj.tag == "Buns") 
		{
			BunsRay bunsScript = hitObj.GetComponent<BunsRay>();
			// Debug—p
			//Debug.Log(script.GetBurgerState());

			if (bunsScript.GetBurgerState())
			{
				makeHamScript.RemakeTheHamburger(bunsScript.GetBurgerState(), hitObj, gameObject);
			}
		}
	}
}
