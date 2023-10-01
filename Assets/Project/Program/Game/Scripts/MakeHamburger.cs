using TMPro;
using UnityEngine;

public class MakeHamburger : MonoBehaviour
{
	[SerializeField] GameObject _BurgerNamePlatePrefab;
	[SerializeField] GameObject[] _Bread;
	[SerializeField] GameObject[] _Hamburger;
	[SerializeField] GameObject[] _CheeseBurger;
	string[] _Tags;
	string _TagHamburger;
	string _TagCheeseburger;
	string _TagUnhamburger;

	void Awake()
	{
		_TagHamburger = "Hamburger";
		_TagCheeseburger = "Cheeseburger";
		_TagUnhamburger = "Unhamburger";
		_Tags = new string[] { _TagHamburger, _TagCheeseburger, _TagUnhamburger };
	}

	public void RemakeTheHamburger(bool isBurgerOntable, GameObject parentObj, GameObject childObj)
	{
		if (!isBurgerOntable || CheckCompleteTag(parentObj))
		{
			return;
		}

		childObj.GetComponent<BoxCollider>().enabled = false;
		Destroy(childObj.GetComponent<GrabItem>());
		Destroy(childObj.GetComponent<Rigidbody>());

		float burgerTopPos = GetBurgerTopPos(parentObj);
		GameObject _HamburgerParent = null;

		if (parentObj.tag == "BunsUnder")
		{
			parentObj.GetComponent<BoxCollider>().enabled = false;
			Destroy(parentObj.GetComponent<BunsRay>());
			Destroy(parentObj.GetComponent<GrabItem>());
			Destroy(parentObj.GetComponent<Rigidbody>());
			_HamburgerParent = CreateBurgerParent(parentObj, childObj, burgerTopPos);
		}
		else
		{
			AddChildElement(parentObj, childObj, new Vector3(parentObj.transform.position.x, burgerTopPos, parentObj.transform.position.z));
		}


		if (parentObj.tag == "BunsUnder" && childObj.tag == "BunsTop")
		{
			FinishedBurger(_HamburgerParent);
		}
		else if (childObj.tag == "BunsTop")
		{
			FinishedBurger(parentObj);
		}
	}

	public void AddChildElement(GameObject parentObj, GameObject childObj, Vector3 childPos)
	{
		childObj.layer = LayerMask.NameToLayer("Default");
		childObj.transform.SetParent(parentObj.transform);
		childObj.transform.position = childPos;
		childObj.transform.rotation = parentObj.transform.rotation;

		Vector3 parentColliderSize = parentObj.GetComponent<BoxCollider>().size;
		Vector3 parentColliderCenter = parentObj.GetComponent<BoxCollider>().center;
		parentObj.GetComponent<BoxCollider>().size = new Vector3(parentColliderSize.x, parentColliderSize.y + childObj.GetComponent<BoxCollider>().size.y, parentColliderSize.z);
		parentObj.GetComponent<BoxCollider>().center = new Vector3(parentColliderCenter.x, parentColliderCenter.y + childObj.GetComponent<BoxCollider>().center.y, parentColliderCenter.z);
	}

	GameObject CreateBurgerParent(GameObject parentObj, GameObject childObj, float burgerTopPos)
	{
		// TODO: ƒvƒŒƒnƒu‰»
		GameObject hamburger = new GameObject("Hamburger");
		hamburger.name = "Hamburger";
		hamburger.tag = "Maikingburger";
		hamburger.layer = LayerMask.NameToLayer("Grab");
		hamburger.transform.position = parentObj.transform.position;
		hamburger.transform.rotation = Quaternion.identity;
		hamburger.GetComponent<Transform>().localScale = parentObj.transform.localScale;
		hamburger.AddComponent<BoxCollider>();
		hamburger.GetComponent<BoxCollider>().size = parentObj.GetComponent<BoxCollider>().size;
		hamburger.GetComponent<BoxCollider>().center = parentObj.GetComponent<BoxCollider>().center;
		hamburger.AddComponent<GrabItem>();
		hamburger.AddComponent<BunsRay>();
		hamburger.GetComponent<BunsRay>();
		parentObj.transform.SetParent(hamburger.transform);

		AddChildElement(hamburger, childObj, new Vector3(hamburger.transform.position.x, burgerTopPos, hamburger.transform.position.z));

		return hamburger;
	}

	bool CheckCompleteTag(GameObject parentObj)
	{
		foreach (string tag in _Tags)
		{
			if (tag != parentObj.tag)
			{
				continue;
			}

			return true;
		}

		return false;
	}

	void FinishedBurger(GameObject parentObj)
	{
		int toppingCount = parentObj.transform.childCount;
		Transform toppings = parentObj.transform;

		GameObject _BurgerNamePlate = DisplayBurgerName(parentObj);

		if (_Bread.Length == toppingCount)
		{
			if (CheckBurgerToppings(toppings, toppingCount, _Bread))
			{
				parentObj.tag = _TagUnhamburger;
				_BurgerNamePlate.name = "Bread";
				_BurgerNamePlate.GetComponent<TextMeshPro>().text = "Bread";
				return;
			}
		}

		if (_Hamburger.Length == toppingCount) 
		{
			if (CheckBurgerToppings(toppings, toppingCount, _Hamburger))
			{
				SetBurgerTagAndNamePlate(parentObj, _BurgerNamePlate, _TagHamburger);
				return;
			}
		}

		if (_CheeseBurger.Length == toppingCount) 
		{
			if (CheckBurgerToppings(toppings, toppingCount, _CheeseBurger))
			{
				SetBurgerTagAndNamePlate(parentObj, _BurgerNamePlate, _TagCheeseburger);
				return;
			}
		}

		SetBurgerTagAndNamePlate(parentObj, _BurgerNamePlate, _TagUnhamburger);
	}

	GameObject DisplayBurgerName(GameObject parentObj)
	{
		float _BurgerTopPos = GetBurgerTopPos(parentObj) + 0.25f;

		GameObject _BurgerNamePlate = Instantiate(_BurgerNamePlatePrefab, new Vector3(parentObj.transform.position.x, _BurgerTopPos, parentObj.transform.position.z), parentObj.transform.rotation);
		_BurgerNamePlate.transform.SetParent(parentObj.transform);

		return _BurgerNamePlate;
	}

	float GetBurgerTopPos(GameObject parentObj)
	{
		return parentObj.GetComponent<BoxCollider>().bounds.center.y + parentObj.GetComponent<BoxCollider>().bounds.size.y / 2;
	}

	bool CheckBurgerToppings(Transform toppings, int toppingCount, GameObject[] correctBurger)
	{
		int trueCount = 0;

		foreach (Transform topping in toppings)
		{
			for (int i = 0; i < toppingCount; i++)
			{
				if (correctBurger[i].name != topping.gameObject.name)
				{
					continue;
				}

				trueCount++;
			}
		}

		return trueCount == toppingCount;
	}

	void SetBurgerTagAndNamePlate(GameObject parentObj, GameObject burgerNamePlate, string tag)
	{
		parentObj.tag = tag;
		burgerNamePlate.name = tag;
		burgerNamePlate.GetComponent<TextMeshPro>().text = tag;
	}
}
