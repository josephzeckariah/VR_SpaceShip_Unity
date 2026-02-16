using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipJoyStickValueReader : MonoBehaviour
{
	[Header("joystick connections")]
	public joystick joystickToGetInputFrom;
	public ThrottleChanger throttleChangerToGetInputFrom;

	[Header("joystick output read")]
	public Vector3 processedShipRotationInput;
	public float shipSpeedInput;

	// Start is called before the first frame update
	void Start()
	{

	}

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
		//	enginePowerApplied = (shipSpeedInput * 3) + Mathf.Abs(shipRotationInput.x) + Mathf.Abs(shipRotationInput.y) + Mathf.Abs(shipRotationInput.z);

	}
}

