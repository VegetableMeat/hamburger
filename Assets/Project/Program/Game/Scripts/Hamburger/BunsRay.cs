using Unity.VisualScripting;
using UnityEngine;

public class BunsRay : MonoBehaviour
{
	[SerializeField, TooltipAttribute("init: 0.05")] float maxDistance;
	[SerializeField] LayerMask hitLayers;
	bool isBurgerOntable;
	RaycastHit hit;

	void Awake()
	{
		if (maxDistance <= 0)
		{
			maxDistance = 0.05f;
		}

		if (hitLayers <= 0)
		{
			hitLayers = 1 << LayerMask.NameToLayer("Table");
		}
	}

	void Update()
	{
		Vector3 rayPos = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
		Physics.Raycast(rayPos, -transform.up, out hit, maxDistance);

		if (hit.collider != null && 1 << hit.collider.gameObject.layer == hitLayers)
		{
			isBurgerOntable = true;
			return;
		}
		isBurgerOntable = false;
	}

	public bool GetBurgerState() 
	{
		return isBurgerOntable;
	}
}
