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

        Debug.Log("Mixtape " + MixtapesManager.MixtapeIndex);
        Debug.Log("Index " + Index);
        transform.DOLocalMoveY(-915f, 0.5f).OnComplete(()=>
        {
            transform.DOLocalMoveY(-1000, MixtapesManager.CurrentWave.MixtapeDuration - 0.5f);
        });
        
    }
}
