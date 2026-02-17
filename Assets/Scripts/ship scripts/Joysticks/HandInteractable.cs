using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandInteractable : MonoBehaviour
{
	public List<Interactor> handInteracotrsAround = new List<Interactor>();

	public bool grapped;

	public delegate void ActionAfterGrapDelegate(Interactor theHand);
	public ActionAfterGrapDelegate actionWhenGrapped;
	public ActionAfterGrapDelegate actionWhenReleased;

	Coroutine CurrentlyChickingIfToBeGripped;
	Coroutine CurrentlyGrippedCoroutine;

	// Update is called once per frame
	private void Start()
	{
		CurrentlyChickingIfToBeGripped = StartCoroutine(CheckingForEachHandIfAroundAndGripped());
	}

	IEnumerator CheckingForEachHandIfAroundAndGripped()
	{
		while (true)
		{
			foreach (Interactor handAround in handInteracotrsAround)
			{
				if (handAround.grip.ReadValue<float>() > 0.4f)
				{
					CurrentlyGrippedCoroutine = StartCoroutine(CurrentlyGrippedByThisHand(handAround));
					if (CurrentlyChickingIfToBeGripped != null)
					{ 
						StopCoroutine(CurrentlyChickingIfToBeGripped);
					}
					
					CurrentlyChickingIfToBeGripped = null;
				}
			}

			yield return null;
		}
	}

	/*void WaitGripFromThisHandOnceAFrame(Interactor handToWaitInputFrom)
	{
		 if(handToWaitInputFrom.grip.ReadValue<float>() > 0.4f)
		 {
			StartCoroutine(CurrentlyGrippedByThisHand(handToWaitInputFrom));

		}
	}*/

	public IEnumerator CurrentlyGrippedByThisHand(Interactor hand)
	{
		actionWhenGrapped(hand);            //call the start delegate which the other functioning class is subscriped to

		this.grapped = true;
		hand.handBusy = true;

		while (true)            //this loop continously checks if grip released while bieng grapped
		{
			if (hand.grip.ReadValue<float>() < 0.3f || Vector3.Distance(this.transform.position, hand.transform.position) > 0.4f)
			{
				actionWhenReleased(hand);                                                                 //call the end delegate which the other functioning class is subscriped to

				this.grapped = false;
				hand.handBusy = false;

				CurrentlyChickingIfToBeGripped = StartCoroutine(CheckingForEachHandIfAroundAndGripped());     //start checking for grip again since it's now released
				if (CurrentlyGrippedCoroutine != null)
				{
					StopCoroutine(CurrentlyGrippedCoroutine);
				}
				CurrentlyGrippedCoroutine = null;
			}
			yield return null;
		}
	}
}
