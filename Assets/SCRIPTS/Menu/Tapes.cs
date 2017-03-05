using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tapes : MonoBehaviour {

    public int Index;
    public MixtapesManager MixtapesManager;

    // Use this for initialization
    void Awake ()
    {
		GameManager.Instance.OnPlaying += Actived;

        MixtapesManager.OnMixtapeChange += Actived;

		Actived ();
    }
		
    void Actived()
    {
        if (MixtapesManager.MixtapeIndex != Index)
            return;

        transform.DOLocalMoveY(-915f, 0.5f).OnComplete(()=>
        {
			transform.DOLocalMoveY(-1000, MixtapesManager.CurrentWave.MixtapeDuration - 0.5f).SetUpdate (true);
		}).SetUpdate (true);
        
    }
}
