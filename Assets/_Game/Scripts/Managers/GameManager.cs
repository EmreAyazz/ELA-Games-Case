using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public void Play()
    {
        BulletManager.SetWalls();

        BulletLoc[] bulletLocs = GameActor.Instance.bullets.GetComponentsInChildren<BulletLoc>();
        foreach(BulletLoc bulletLoc in bulletLocs)
        {
            if (bulletLoc.myBullet)
                bulletLoc.myBullet.GetComponent<Bullet>().Go();
        }
    }

    public static void PlayGun()
    {
        for (int i = 0; i < GameActor.Instance.guns.Count; i++)
        {
            GameActor.Instance.guns[i].transform.DOLocalMove(GameActor.Instance.gunPos[i], 1f);
        }
    }
}
