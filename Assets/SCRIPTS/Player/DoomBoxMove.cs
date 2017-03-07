using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoomBoxMove : MonoBehaviour
{
    public Player player;
    public float radius = 3f;

    public Animator an;

    void Start ()
    {
		player.OnLaunch += SetDoomBox;
		player.OnJump += SetDoomBox;
		player.OnHold += ChangeFace;
    }

    void ChangeFace()
    {
        an.SetTrigger("Hold");
    }

    void SetDoomBox()
    {
        an.SetTrigger("Fire");
        Vector3 pos = player._launchPosition - player.transform.position;
        pos.Normalize();
        pos *= radius;

        transform.DOLocalMove(pos, 0.1f);
    }
}

