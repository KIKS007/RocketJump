using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixtapesManager : Singleton<MixtapesManager> 
{
	[Header ("Mixtapes")]
	public int MixtapeIndex = 0;
	public Wave CurrentWave;
	public Wave[] SelectedMixtapes = new Wave[3];

	[Header ("Unlocked Mixtapes")]
	public List<Wave> UnlockedMixtapes = new List<Wave>();

	[Header ("Mixtape State")]
	public bool IsPaused = false;

	private Player _playerScript;

	public event EventHandler OnMixtapeChange;

	void Start ()
	{
		_playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		FirstMixtape ();
	}

	void FirstMixtape ()
	{
		_playerScript.SetWave (SelectedMixtapes [MixtapeIndex]);
		CurrentWave = SelectedMixtapes [MixtapeIndex];
		StartCoroutine (MixtapeDuration ());
	}

	public void MixtapeUnlocked (Wave wave)
	{
		if (!UnlockedMixtapes.Contains (wave))
			UnlockedMixtapes.Add (wave);
		else
			Debug.LogWarning ("Mixtape Already Unlocked!");
	}

	IEnumerator MixtapeDuration ()
	{
		if (OnMixtapeChange != null)
			OnMixtapeChange ();

		float duration = 0;

		while(!IsPaused && duration < CurrentWave.MixtapeDuration)
		{
			duration += Time.unscaledDeltaTime;

			yield return new WaitForEndOfFrame ();
		}

		NextMixtape ();
	}

	public void NextMixtape ()
	{
		StopCoroutine (MixtapeDuration ());

		MixtapeIndex++;

		if (MixtapeIndex == 3)
			MixtapeIndex = 0;

		_playerScript.SetWave (SelectedMixtapes [MixtapeIndex]);
		CurrentWave = SelectedMixtapes [MixtapeIndex];

		StartCoroutine (MixtapeDuration ());
	}

	public void PreviousMixtape ()
	{
		StopCoroutine (MixtapeDuration ());

		MixtapeIndex--;

		if (MixtapeIndex == -1)
			MixtapeIndex = 2;

		_playerScript.SetWave (SelectedMixtapes [MixtapeIndex]);
		CurrentWave = SelectedMixtapes [MixtapeIndex];

		StartCoroutine (MixtapeDuration ());
	}

	public void PauseMixtape (float duration)
	{
		StartCoroutine (PauseDuration (duration));
	}

	IEnumerator PauseDuration (float duration)
	{
		IsPaused = true;

		yield return new WaitForSecondsRealtime (duration);

		IsPaused = false;
	}
}
