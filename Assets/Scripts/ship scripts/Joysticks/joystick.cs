using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(HandInteractable))]
public class joystick : MonoBehaviour
{
    HandInteractable handInteractable;
	//refrence to each part to animate according to input.
	Animator zAxisAnimator;
	Animator xAxisAnimator;
	Animator yAxisAnimator;

	public Vector3 neg1Pos1Output;

	public GameObject grippedGraphics;

	 Coroutine gripped;

    // Start is called before the first frame update
    void Start()
    {
        handInteractable = this.GetComponent<HandInteractable>();

		handInteractable.actionWhenGrapped += StartGripAction;         //connecting this to gripped and releases of hand interactable    
		handInteractable.actionWhenReleased += StopGripAction;

		                                                                   //animator for each of the 3 axis
		zAxisAnimator = this.transform.GetChild(0).GetComponent<Animator>();	
		xAxisAnimator = this.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		yAxisAnimator = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>();

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

		neg1Pos1Output = Vector3.zero;
		animateJoyStick(neg1Pos1Output);
		handInteracting.handModel.SetActive(true);
		grippedGraphics.SetActive(false);
	}




	public IEnumerator WhileGripped (Interactor handThatsCurrentlyGrippingUs)
	{
		while (true)                                                            //All the things to do while grapped:
		{
			

			Vector3 rawHandInput = handThatsCurrentlyGrippingUs.rotation.ReadValue<Quaternion>().eulerAngles;       //read hand rotation and turn it to neg1 pos1
			Vector3 Neg1pos1Values = Vector3AngleToNeg1Pos1(rawHandInput);

			animateJoyStick(neg1Pos1Output);                                              //animate joystick

			neg1Pos1Output = Neg1pos1Values;
			


			yield return null;
		}
	}

	
	




	//the whole part below is to transform the raw 360degrees roation to a -1,1 in each axis
	Vector3 Vector3AngleToNeg1Pos1(Vector3 InputAngle)
	{
		float yAxisNeg1ToPos1 = EulerInputToNeg1Pos1(InputAngle.y);
		float xAxisNeg1ToPos1 = EulerInputToNeg1Pos1(InputAngle.x);             //  Call the same method for each of the 3 axis.
		float zAxisNeg1ToPos1 = EulerInputToNeg1Pos1(InputAngle.z);


		return new Vector3(xAxisNeg1ToPos1, yAxisNeg1ToPos1, zAxisNeg1ToPos1);

	}
	float EulerInputToNeg1Pos1(float eulerAngle)
	{
		float OutputNeg1ToPos1 = 0;

		if (eulerAngle < 91 || eulerAngle > 269)                                             //This code 
		{
			if (eulerAngle < 91)
			{
				OutputNeg1ToPos1 = Mathf.InverseLerp(0, 90, eulerAngle);                                 //poslerp
			}
			else if (eulerAngle > 269)
			{
				OutputNeg1ToPos1 = -Mathf.InverseLerp(360, 270, eulerAngle);
			}                                                                                  //negLerp
		}
		else if (eulerAngle < 180)
		{
			OutputNeg1ToPos1 = 1;
		}
		else if (eulerAngle > 180)
		{
			OutputNeg1ToPos1 = -1f;
		}

		return OutputNeg1ToPos1;
	}
	void animateJoyStick(Vector3 theNeg1Pos1ToAnimateBy)
	{
		zAxisAnimator.SetFloat("zAxisParameter", Mathf.InverseLerp(-1, 1, theNeg1Pos1ToAnimateBy.z));
		xAxisAnimator.SetFloat("xAxisParameter", Mathf.InverseLerp(-1, 1, theNeg1Pos1ToAnimateBy.x));
		yAxisAnimator.SetFloat("yAxisParameter", Mathf.InverseLerp(-1, 1, theNeg1Pos1ToAnimateBy.y));
	}

}
