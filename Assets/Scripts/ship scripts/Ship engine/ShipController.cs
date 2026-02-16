using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
public class ShipController : MonoBehaviour
{


}
	/*public class ShipController : MonoBehaviour
{
	public joystick joystickToGetInputFrom;
    Vector3 shipRotationInput;
	Vector3 processedShipRotationInput;
	/////
	public ThrottleChanger throttleChangerToGetInputFrom;
	float shipSpeedInput;


	float baseEnginePower;
	float enginePowerApplied;
	Rigidbody shipRigidBody;
	AudioSource shipAudioSource;
	// Start is called before the first frame update
	void Start()
    {
		shipRigidBody = this.GetComponent<Rigidbody>();
		shipAudioSource = this.GetComponent<AudioSource>();
		baseEnginePower = shipRigidBody.mass ;
		
	}
    void Update()
    {
		ReadInputFromController();                  //the only methods the ship make every frame
		ApplyInputToShip();
		RunEngingSounds();

	}



	void ReadInputFromController()
	{
		if (joystickToGetInputFrom != null)
		{
			shipRotationInput = joystickToGetInputFrom.neg1Pos1Output;
			processedShipRotationInput = new(shipRotationInput.x, shipRotationInput.y, shipRotationInput.z /3);
		}

		if (throttleChangerToGetInputFrom != null)
		{
			shipSpeedInput = throttleChangerToGetInputFrom.speedOutput;
		}
		enginePowerApplied = (shipSpeedInput * 3) + Mathf.Abs(shipRotationInput.x) + Mathf.Abs(shipRotationInput.y) + Mathf.Abs(shipRotationInput.z);

	}
	void ApplyInputToShip()
	{
		shipRigidBody.AddForce((this.transform.forward * baseEnginePower * 10) * shipSpeedInput);
		shipRigidBody.AddRelativeTorque(processedShipRotationInput * baseEnginePower);

		
	
	}
	void RunEngingSounds()
	{
		shipAudioSource.volume = Mathf.InverseLerp(0, 5, enginePowerApplied);
		if (enginePowerApplied == 0)
		{
			shipAudioSource.Stop();
		}
		else if (enginePowerApplied > 0 && !shipAudioSource.isPlaying)
		{
			shipAudioSource.Play();
		}
	}


}*/
