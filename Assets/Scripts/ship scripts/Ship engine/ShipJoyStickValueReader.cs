using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipJoyStickValueReader : MonoBehaviour
{
	[Header("joystick connections")]
	public joystick joystickToGetInputFrom;
	public ThrottleChanger throttleChangerToGetInputFrom;

	[Header("joystick output read")]
	public Vector3 processedShipRotationInput;
	public float shipSpeedInput;

	public UnityEvent<Vector3,float> shipRotationInputThisFrame;

	// Update is called once per frame
	void Update()
	{
		ReadInputFromController(); //the only methods the ship make every frame
	}

	void ReadInputFromController()
	{
		if (joystickToGetInputFrom != null)
		{
			Vector3 shipRotationInput = joystickToGetInputFrom.neg1Pos1Output;
			processedShipRotationInput = new(shipRotationInput.x, shipRotationInput.y, shipRotationInput.z / 3);
		}

		if (throttleChangerToGetInputFrom != null)
		{
			shipSpeedInput = throttleChangerToGetInputFrom.speedOutput;
		}

		shipRotationInputThisFrame.Invoke(processedShipRotationInput, shipSpeedInput);
		//	enginePowerApplied = (shipSpeedInput * 3) + Mathf.Abs(shipRotationInput.x) + Mathf.Abs(shipRotationInput.y) + Mathf.Abs(shipRotationInput.z);

	}
}

