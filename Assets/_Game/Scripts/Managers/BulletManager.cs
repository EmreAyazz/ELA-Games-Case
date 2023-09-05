using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collectable;

public class BulletManager : MonoBehaviour
{
    public int bulletLevel;

    public int bulletPrice;

    RaycastHit hit;
    GameObject bullet;
    BulletLoc oldLoc;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.GetComponent<BulletLoc>())
                {
                    oldLoc = hit.transform.GetComponent<BulletLoc>();
                    bullet = oldLoc.myBullet;
                    oldLoc.myBullet = null;
                }
            }
        }

        if (Input.GetMouseButton(0) && bullet)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                bullet.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z); 
            }
        }

        if (Input.GetMouseButtonUp(0) && bullet)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.GetComponent<BulletLoc>())
                {
                    BulletLoc bulletLoc = hit.transform.GetComponent<BulletLoc>();
                    if (!bulletLoc.myBullet)
                    {
                        bulletLoc.myBullet = bullet;
                        bullet.transform.position = bulletLoc.transform.position + new Vector3(0, 0.1f, 0);
                        bullet = null;
                        oldLoc = null;
                    }
                    else
                    {
                        if (bulletLoc.level == oldLoc.level)
                        {
                            Destroy(bullet);
                            bullet = null;
                            oldLoc = null;
                            bulletLoc.level++;
                            bulletLoc.myBullet.GetComponent<Renderer>().material = GameActor.Instance.bulletLevels[bulletLoc.level];
                        }
                    }
                }
                else
                {
                    bullet.transform.position = oldLoc.transform.position + new Vector3(0, 0.1f, 0);
                    bullet = null;
                    oldLoc = null;
                }
            }
        }
    }

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
