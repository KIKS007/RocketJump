using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tapes : MonoBehaviour {

    public int Index;
    public MixtapesManager MixtapesManager;

    // Use this for initialization
    void Start ()
    {
        //MakeGame.Do();

        MixtapesManager.OnMixtapeChange += Actived;
    }
		
    void Actived()
    {
        if (MixtapesManager.MixtapeIndex != Index)
            return;

        transform.DOScale(2f, 0.5f).OnComplete(()=>
        {
            transform.DOScale(0.5f, MixtapesManager.CurrentWave.MixtapeDuration - 0.5f);
        });
        
    }
}
