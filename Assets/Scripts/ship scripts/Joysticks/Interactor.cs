using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class Interactor : MonoBehaviour
{
	public GameObject parenti;

	[Header("InputAction")]
	public InputActionAsset inputAsset;
	public InputAction trigger;
	public InputAction rotation;
	 public InputAction grip;

	public enum handInteractor {right,left};
	public handInteractor handType = handInteractor.right;


	public GameObject handModel;

	public bool handBusy;

	public List<HandInteractable> interactablesThisHandSentto = new List<HandInteractable>();      //list of Interactables this hand sent it's avalabilty to .
	void Start()
    {	
		switch (handType)
		{
			case handInteractor.right:
				trigger = inputAsset.FindActionMap("rightXrController").FindAction("trigger");
				rotation = inputAsset.FindActionMap("rightXrController").FindAction("rotation");
				grip = inputAsset.FindActionMap("rightXrController").FindAction("grip");
				break;
			case handInteractor.left:
				trigger = inputAsset.FindActionMap("leftXrController").FindAction("trigger");
				rotation = inputAsset.FindActionMap("leftXrController").FindAction("rotation");
				grip = inputAsset.FindActionMap("leftXrController").FindAction("grip");
				break;
		}
		



	}
	

    // Update is called once per frame
    void Update()
    {
	//	Debug.Log(this.transform.localPosition + parenti.transform.position);

		if (handBusy)                                                       //if hand becomes busy clear it's avaiability from all interactables currently sent to
		{
			foreach(HandInteractable hand in interactablesThisHandSentto)
			{
				hand.handInteracotrsAround.Remove(this);
			}
			interactablesThisHandSentto.Clear();
		}


    }


	     


	void OnTriggerStay(Collider other)                                 //if you are close to an interactable send to it your avalability only once , and keep track of it to clear later.
	{
		if (!handBusy)
		{
			if (other.gameObject.TryGetComponent<HandInteractable>(out HandInteractable interactableComponent))
			{
				if (!interactableComponent.handInteracotrsAround.Contains(this))
				{
					interactableComponent.handInteracotrsAround.Add(this);
					interactablesThisHandSentto.Add(interactableComponent);
				}
			}
		}
	}
	void OnTriggerExit(Collider other)                                                   //if hand go away from interactable then remove the avalability msg 
	{
		if (other.transform.TryGetComponent<HandInteractable>(out HandInteractable interactableComponent))
		{
			interactableComponent.handInteracotrsAround.Remove(this);
			interactablesThisHandSentto.Remove(interactableComponent);
		}

	}
	private void OnEnable()
	{
		inputAsset.Enable();
	}
}
