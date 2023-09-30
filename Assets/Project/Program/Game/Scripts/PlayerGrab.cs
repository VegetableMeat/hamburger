using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGrab : MonoBehaviour
{
	[SerializeField] float maxDistance;
	[SerializeField] LayerMask hitLayers;
	GameObject mainCamera;
	GameObject grabItem;
	bool isClick;
	RaycastHit hit;

	void Awake()
	{
		mainCamera = Camera.main.gameObject;
		grabItem = null;
		isClick = false;
	}

	void Update()
	{
		Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxDistance);

		if (hit.collider != null && Input.GetMouseButtonDown(0) && 1 << hit.collider.gameObject.layer == hitLayers)
		{
			isClick = true;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			isClick = false;
		}

		if (isClick)
		{
			if (grabItem == null)
			{
				grabItem = hit.collider.gameObject;
				grabItem.GetComponent<GrabItem>().Touch(mainCamera, hit.point);
			}
			else if (grabItem.GetComponent<GrabItem>())
			{
				grabItem.GetComponent<GrabItem>().Hold();
			}

			return;
		}

		grabItem = null;
	}
}
