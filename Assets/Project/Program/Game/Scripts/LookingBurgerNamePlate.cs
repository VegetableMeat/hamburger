using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingBurgerNamePlate : MonoBehaviour
{
    void Update()
    {
		gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, Camera.main.gameObject.transform.localEulerAngles.y, gameObject.transform.rotation.z);
    }
}
