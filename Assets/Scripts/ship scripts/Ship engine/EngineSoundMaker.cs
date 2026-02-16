using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundMaker : MonoBehaviour
{
	

	// Make enginePowerApplied public so it can be set from other scripts or the Inspector
	public float enginePowerApplied;

	AudioSource shipAudioSource;

	//------------------------------------------------------------------------------------------------------------------------------------------
	//logic
	//------------------------------------------------------------------------------------------------------------------------------------------
	// Start is called before the first frame update
	void Start()
	{
		shipAudioSource = this.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		RunEngineSounds();
	}


	void RunEngineSounds()
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
	//------------------------------------------------------------------------------------------------------------------------------------------
}
