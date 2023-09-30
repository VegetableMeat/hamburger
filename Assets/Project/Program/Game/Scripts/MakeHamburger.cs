using System;
using UnityEngine;

public class MakeHamburger : MonoBehaviour
{
	[SerializeField] GameObject[] hamburger;
	[SerializeField] GameObject[] cheeseBurger;
	[SerializeField] String[] tags;

	public void RemakeTheHamburger(bool isBurgerOntable, GameObject parentObj, GameObject childObj)
	{
		if (!isBurgerOntable || CheckCompleteTag(parentObj))
		{
			return;
		}

		childObj.GetComponent<BoxCollider>().enabled = false;
		Destroy(childObj.GetComponent<GrabItem>());
		Destroy(childObj.GetComponent<Rigidbody>());

		float burgerTopPos = parentObj.GetComponent<BoxCollider>().bounds.center.y + parentObj.GetComponent<BoxCollider>().bounds.size.y / 2;

		if (parentObj.tag == "BunsUnder")
		{
			parentObj.GetComponent<BoxCollider>().enabled = false;
			Destroy(parentObj.GetComponent<BunsRay>());
			Destroy(parentObj.GetComponent<GrabItem>());
			Destroy(parentObj.GetComponent<Rigidbody>());
			CreateBurgerParent(parentObj, childObj, burgerTopPos);
		}
		else
		{
			AddChildElement(parentObj, childObj, new Vector3(parentObj.transform.position.x, burgerTopPos, parentObj.transform.position.z));
		}


		if (childObj.tag == "BunsTop")
		{
			FinishedBurger(parentObj);
		}
	}

	public void AddChildElement(GameObject parentObj, GameObject childObj, Vector3 childPos)
	{
		childObj.layer = LayerMask.NameToLayer("Default");
		childObj.transform.parent = parentObj.transform.root;
		childObj.transform.position = childPos;
		childObj.transform.rotation = parentObj.transform.rotation;

		Vector3 parentColliderSize = parentObj.GetComponent<BoxCollider>().size;
		Vector3 parentColliderCenter = parentObj.GetComponent<BoxCollider>().center;
		parentObj.GetComponent<BoxCollider>().size = new Vector3(parentColliderSize.x, parentColliderSize.y + childObj.GetComponent<BoxCollider>().size.y, parentColliderSize.z);
		parentObj.GetComponent<BoxCollider>().center = new Vector3(parentColliderCenter.x, parentColliderCenter.y + childObj.GetComponent<BoxCollider>().center.y, parentColliderCenter.z);
	}

	void CreateBurgerParent(GameObject parentObj, GameObject childObj, float burgerTopPos)
	{
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
		parentObj.transform.parent = hamburger.transform;

		AddChildElement(hamburger, childObj, new Vector3(hamburger.transform.position.x, burgerTopPos, hamburger.transform.position.z));
	}

	bool CheckCompleteTag(GameObject parentObj)
	{
		int tagCount = tags.Length;

		for (int i = 0; i < tagCount; i++) 
		{
			if (tags[i] != parentObj.tag) 
			{
				continue;
			}
			return true;
		}

		return false;
	}

	void FinishedBurger(GameObject parentObj)
	{
		int childCount = parentObj.transform.childCount;
		Transform allChild = parentObj.transform;

		if (hamburger.Length == childCount) 
		{
			int trueOrfalse = 0;

			foreach (Transform child in allChild)
			{
				for (int i = 0; i < childCount; i++) 
				{
					if (hamburger[i].name != child.gameObject.name) 
					{
						continue;
					}

					trueOrfalse++;
				}
			}

			if (trueOrfalse == childCount) 
			{
				parentObj.tag = "Hamburger";
				return;
			}
		}

		if (cheeseBurger.Length == childCount) 
		{
			int trueOrfalse = 0;

			foreach (Transform child in allChild)
			{
				for (int i = 0; i < childCount; i++)
				{
					if (cheeseBurger[i].name != child.gameObject.name)
					{
						continue;
					}

					trueOrfalse++;
				}
			}

			if (trueOrfalse == childCount)
			{
				parentObj.tag = "Cheeseburger";
				return;
			}
		}

		parentObj.tag = "Unhamburger";
	}
}
