using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipForceApplier : MonoBehaviour
{
	public Rigidbody shipRigidBody;

	/*public Vector3 ShipRotationToApply;
	public float ShipThrottleToApply;*/

	public UnityEvent< float> ForceAppliedThisFrame;


	public void ApplyThisForceThisFrame(Vector3 rotationToApply, float throttleToApply)
	{
		shipRigidBody.AddForce((shipRigidBody.gameObject.transform.forward * shipRigidBody.mass * 10) * throttleToApply);
		shipRigidBody.AddRelativeTorque(rotationToApply * shipRigidBody.mass);

		ForceAppliedThisFrame.Invoke( (throttleToApply * 3) + Mathf.Abs(rotationToApply.x) + Mathf.Abs(rotationToApply.y) + Mathf.Abs(rotationToApply.z));
	}
}
