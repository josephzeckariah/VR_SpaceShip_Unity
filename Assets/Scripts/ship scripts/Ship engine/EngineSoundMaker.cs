using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSoundMaker : MonoBehaviour
{
	

	// Make enginePowerApplied public so it can be set from other scripts or the Inspector
	//public float enginePowerApplied;

	AudioSource shipAudioSource;

	//------------------------------------------------------------------------------------------------------------------------------------------
	//logic
	//------------------------------------------------------------------------------------------------------------------------------------------
	// Start is called before the first frame update
	void Start()
	{
		shipAudioSource = this.GetComponent<AudioSource>();
	}

	

	public void RunEngineSounds(float PowerOfEngineToCOnvertToSound)
	{
		shipAudioSource.volume = Mathf.InverseLerp(0, 5, PowerOfEngineToCOnvertToSound);
		if (PowerOfEngineToCOnvertToSound == 0)
		{
			shipAudioSource.Stop();
		}
		else if (PowerOfEngineToCOnvertToSound > 0 && !shipAudioSource.isPlaying)
		{
			shipAudioSource.Play();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------------------
}
