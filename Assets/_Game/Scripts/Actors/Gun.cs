using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    public Vector3 pos;

    public void Play()
    {
        transform.DOLocalMove(pos, 1f);
    }
}
