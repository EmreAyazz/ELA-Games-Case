using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

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

    }
}
