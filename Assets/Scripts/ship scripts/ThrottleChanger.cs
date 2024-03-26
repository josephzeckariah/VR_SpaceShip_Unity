using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleChanger : MonoBehaviour
{
	HandInteractable handInteractable;

	public float speedOutput;

	public GameObject grippedGraphics;
	public GameObject ship;


	Coroutine gripped;
	GameObject axel;
	void Start()
    {
		handInteractable = this.GetComponent<HandInteractable>();

		handInteractable.actionWhenGrapped += StartGripAction;         //connecting this to gripped and releases of hand interactable    
		handInteractable.actionWhenReleased += StopGripAction;

		axel = this.transform.GetChild(1).gameObject;
	}




	void StartGripAction(Interactor handInteracting)
	{
		gripped = StartCoroutine(WhileGripped(handInteracting));                    //start detecting input time.

		handInteracting.handModel.SetActive(false);
		grippedGraphics.SetActive(true);
	}
	void StopGripAction(Interactor handInteracting)
	{
		StopCoroutine(gripped);                                                         //stop detecting input and zero the output.

		handInteracting.handModel.SetActive(true);
		grippedGraphics.SetActive(false);
	}








	public IEnumerator WhileGripped(Interactor handThatsCurrentlyGrippingUs)
	{
		while (true)                                                            //All the things to do while grapped:
		{
			Vector3 angleBetweenAxilAndHand = AngleBetweenTwoPointsRotated(this.transform.position,handThatsCurrentlyGrippingUs.transform.position,ship.transform.rotation);
			
			Vector3 angleWithLimitedMotion = new Vector3(ClampValue(angleBetweenAxilAndHand.x, 0, 140), 0, 0);
		
			axel.transform.localEulerAngles = -angleWithLimitedMotion;            //rotate the handle it's negative because unity's circle is inverse to what i made thefunction as.           

			speedOutput = Mathf.InverseLerp(120, 0, angleWithLimitedMotion.x);

			yield return null;
		}
	}






	              //below are the utility methods.



	Vector3 AngleBetweenTwoPoints(Vector3 fromPoint,Vector3 ToPoint)                            //Method to calculate angle between two point .
	{
		Vector3 VectorFromAxisToOtherPoint = (ToPoint - fromPoint);
		Vector3 VectorFromAxisToOtherPointNormalized = VectorFromAxisToOtherPoint.normalized;
		

		
		float angleBetweenOnXAxis = Mathf.Atan2(VectorFromAxisToOtherPointNormalized.y, VectorFromAxisToOtherPointNormalized.z) * Mathf.Rad2Deg ;
		float angleBetweenOnYAxis = Mathf.Atan2(VectorFromAxisToOtherPointNormalized.z, VectorFromAxisToOtherPointNormalized.x) * Mathf.Rad2Deg;
		float angleBetweenOnZAxis = Mathf.Atan2(VectorFromAxisToOtherPointNormalized.x, VectorFromAxisToOtherPointNormalized.y) * Mathf.Rad2Deg;
	

		return new Vector3 (angleBetweenOnXAxis, angleBetweenOnYAxis, angleBetweenOnZAxis);
	}


	Vector3 AngleBetweenTwoPointsRotated(Vector3 fromPoint, Vector3 ToPoint, Quaternion rotateTheWholeAngleBy)                            //Method to calculate angle between two point after rotating them.
	{
		Vector3 VectorFromAxisToOtherPoint = (ToPoint - fromPoint);
		Vector3 rotatedVectorFromAxisToOtherPoint = Quaternion.Inverse(rotateTheWholeAngleBy) * VectorFromAxisToOtherPoint;
		Vector3 VectorFromAxisToOtherPointNormalized = rotatedVectorFromAxisToOtherPoint.normalized;



		float angleBetweenOnXAxis = Mathf.Atan2(VectorFromAxisToOtherPointNormalized.y, VectorFromAxisToOtherPointNormalized.z) * Mathf.Rad2Deg;
		float angleBetweenOnYAxis = Mathf.Atan2(VectorFromAxisToOtherPointNormalized.z, VectorFromAxisToOtherPointNormalized.x) * Mathf.Rad2Deg;
		float angleBetweenOnZAxis = Mathf.Atan2(VectorFromAxisToOtherPointNormalized.x, VectorFromAxisToOtherPointNormalized.y) * Mathf.Rad2Deg;


		return new Vector3(angleBetweenOnXAxis, angleBetweenOnYAxis, angleBetweenOnZAxis);
	}









	float ClampValue(float valueToCheck,float minRange,float maxRange)              //Method to lock values within a range.
	{
		if (valueToCheck < minRange)
		{
			valueToCheck = minRange;
			return valueToCheck;
		}
		else if (valueToCheck > maxRange)
		{
			 valueToCheck = maxRange;
			return valueToCheck;
		}
		else
		{
			return valueToCheck;
		}
	}
}
