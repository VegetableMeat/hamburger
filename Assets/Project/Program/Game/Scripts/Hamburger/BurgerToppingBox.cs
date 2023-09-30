using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerToppingBox : MonoBehaviour
{
	[SerializeField] GameObject[] _Toppings;

	public void SpawnToppingFromBox(GameObject box)
	{
		if(gameObject.name != box.name)
		{
			return;
		}

		Vector3 spawnPos = new Vector3(box.transform.position.x, box.transform.position.y + 2.0f, box.transform.position.z);

		for (int i = 0; i < _Toppings.Length; i++)
		{
			GameObject topping = Instantiate(_Toppings[i], spawnPos = new Vector3(spawnPos.x, spawnPos.y + i, spawnPos.z), Quaternion.identity);
			topping.name = _Toppings[i].name;
		}
	}
}
