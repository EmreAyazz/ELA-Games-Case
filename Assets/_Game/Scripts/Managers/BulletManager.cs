using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collectable;

public class BulletManager : MonoBehaviour
{
    public int bulletLevel;

    public int bulletPrice;

    public void BuyBullet()
    {
        if (CollectableManager.CoinOkay(bulletPrice))
        {
            CollectableManager.RemoveCoin(bulletPrice);

            TakeBullet();
        }
    }

    public void TakeBullet()
    {
        BulletLoc bulletLoc = FindNullBulletPos();

        GameObject bulletPrefab = Resources.Load<GameObject>("Bullet");

        Vector3 height = new Vector3(0, 0.1f, 0);
        Vector3 rot = new Vector3(90f, 0, 0);

        GameObject newBullet = Instantiate(bulletPrefab, bulletLoc.transform.position + height, Quaternion.Euler(rot));

        bulletLoc.myBullet = newBullet;
    }

    private BulletLoc FindNullBulletPos()
    {
        BulletLoc[] bulletlocs = GameActor.Instance.bullets.transform.GetComponentsInChildren<BulletLoc>();

        foreach (BulletLoc bulletLoc in bulletlocs)
        {
            if (!bulletLoc.myBullet)
            {
                return bulletLoc;
            }
        }

        return null;
    }
}
