using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VibrationManager : Singleton<VibrationManager> 
{
	public List<VibrationSettings> vibrationsList = new List<VibrationSettings> ();

	public void Vibrate (FeedbackType whichFeedback = FeedbackType.Default)
	{
		float vibrationDuration = 0;
		bool exactType = true;

		for(int i = 0; i < vibrationsList.Count; i++)
		{
			if(vibrationsList[i].feedBackType == whichFeedback)
			{
				vibrationDuration = vibrationsList [i].vibrationDuration;
				exactType = true;
				break;
			}
		}

		if(!exactType)
			vibrationDuration = vibrationsList [0].vibrationDuration;

		Vibrate (vibrationDuration);
	}

	public void Vibrate (long milliseconds)
	{
		if (Vibration.HasVibrator ())
			Vibration.Vibrate (milliseconds);
	}

	public void Vibrate (float seconds)
	{
		long milliseconds = Convert.ToInt64 (seconds * 1000);

		if (Vibration.HasVibrator ())
			Vibration.Vibrate (milliseconds);
	}

	void OnApplicationQuit ()
	{
		Vibration.Cancel ();
	}

	void OnApplicationPause ()
	{
		Vibration.Cancel ();
	}
}

[Serializable]
public class VibrationSettings
{
	public FeedbackType feedBackType = FeedbackType.Default;
	public float vibrationDuration = 0.05f;
}
