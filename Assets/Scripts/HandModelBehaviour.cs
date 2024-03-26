using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandModelBehaviour : MonoBehaviour
{


    public Interactor interactorOfThisHand;
    Animator handAniamtor;

    // Start is called before the first frame update
    void Start()
    {
        handAniamtor = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        handAniamtor.SetFloat("Grip", interactorOfThisHand.grip.ReadValue<float>());
		handAniamtor.SetFloat("Trigger", interactorOfThisHand.trigger.ReadValue<float>());

		//Debug.Log( handAniamtor.GetParameter();
	}
}
