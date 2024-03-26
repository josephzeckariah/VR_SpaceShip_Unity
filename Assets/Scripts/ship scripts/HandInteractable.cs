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

    // Update is called once per frame
    void Update()
    {
        if (!grapped)                                                   //if this interactable is free then foreach hand around await input.
        {
            foreach (Interactor handAround in handInteracotrsAround)
            {
                WaitGripFromThisHandOnceAFrame(handAround);
            }
        }

    }

	void WaitGripFromThisHandOnceAFrame(Interactor handToWaitInputFrom)
    {
		 if(handToWaitInputFrom.grip.ReadValue<float>() > 0.4f)
		 {
            StartCoroutine(CurrentlyGrippedByThisHand(handToWaitInputFrom));

		 }
	}

    public IEnumerator CurrentlyGrippedByThisHand(Interactor hand)
    {

		actionWhenGrapped(hand);            //call the start delegate which the other functioning class is subscriped to

		this.grapped = true;
		hand.handBusy = true;

		while (true)            //this loop continously checks if grip released while bieng grapped
        {
            if (hand.grip.ReadValue<float>() < 0.3f || Vector3.Distance(this.transform.position,hand.transform.position)>0.4f)
            {
				actionWhenReleased(hand);                                                                 //call the end delegate which the other functioning class is subscriped to

				this.grapped = false;
				hand.handBusy = false;
				yield break;
            }
			yield return null;
		}
    }
}
