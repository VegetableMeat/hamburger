using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
	[SerializeField, TooltipAttribute("init: 0.1")] float maxDistance;
	[SerializeField] LayerMask hitLayers;
	RaycastHit hit;

	void Awake()
	{
		if (maxDistance < 0)
		{
			maxDistance = 0.1f;
		}
	}
}
