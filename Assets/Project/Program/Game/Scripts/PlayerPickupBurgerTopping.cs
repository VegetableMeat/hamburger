using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickupBurgerTopping : MonoBehaviour
{
	[SerializeField] float maxDistance;
	[SerializeField] LayerMask hitLayers;
	GameObject mainCamera;
	GameObject viewBox;
	RaycastHit hit;

	void Awake()
	{
		mainCamera = Camera.main.gameObject;
		viewBox = null;
	}

	void Update()
	{
		Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxDistance);

		if (hit.collider != null && 1 << hit.collider.gameObject.layer == hitLayers && Input.GetMouseButtonDown(0))
		{
			if (viewBox == null) 
			{
				viewBox = hit.collider.gameObject;
			}

			viewBox.GetComponent<BurgerToppingBox>().SpawnToppingFromBox(viewBox);

			return;
		}

		viewBox = null;
	}
}
