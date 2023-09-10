using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collectable;
using TMPro;

public class BulletManager : MonoBehaviour
{
    public int bulletLevel;

    public int bulletPrice;

    public static int totalBullet;

    RaycastHit hit;
    GameObject bullet;
    BulletLoc oldLoc;

    private void Update()
    {
        if (GameManager.controling) return;

        RaycastHit hit = TakeHit();

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.transform.GetComponent<BulletLoc>())
            {
                oldLoc = hit.transform.GetComponent<BulletLoc>();
                bullet = oldLoc.myBullet;
                oldLoc.myBullet = null;
            }
        }

        if (Input.GetMouseButton(0) && bullet)
        {
            bullet.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
        }

        if (Input.GetMouseButtonUp(0) && bullet)
        {
            if (hit.transform.GetComponent<BulletLoc>())
            {
                BulletLoc bulletLoc = hit.transform.GetComponent<BulletLoc>();
                if (!bulletLoc.myBullet)
                {
                    bulletLoc.myBullet = bullet;
                    bullet.transform.position = bulletLoc.transform.position + new Vector3(0, 0.1f, 0);
                    bulletLoc.level = oldLoc.level;
                    oldLoc.level = 0;
                    bullet = null;
                    oldLoc = null;

                    Save();
                }
                else
                {
                    if (bulletLoc.level == oldLoc.level)
                    {
                        Destroy(bullet);
                        bullet = null;
                        oldLoc.level = 0;
                        oldLoc = null;
                        bulletLoc.level++;
                        bulletLoc.myBullet.GetComponent<Renderer>().material = GameActor.Instance.bulletLevels[bulletLoc.level];

                        bulletLoc.myBullet.transform.GetChild(0).GetComponent<TextMeshPro>().text = (bulletLoc.level + 1).ToString();

                        bulletLoc.myBullet.GetComponent<Bullet>().health = 100 + (25 * (bulletLoc.level + 1));
                        bulletLoc.myBullet.GetComponent<Bullet>().level = (bulletLoc.level + 1);

                        totalBullet--;

                        Save();
                    }
                    else
                    {
                        bullet.transform.position = oldLoc.transform.position + new Vector3(0, 0.1f, 0);
                        oldLoc.myBullet = bullet;
                        bullet = null;
                        oldLoc = null;
                    }
                }
            }
            else
            {
                bullet.transform.position = oldLoc.transform.position + new Vector3(0, 0.1f, 0);
                oldLoc.myBullet = bullet;
                bullet = null;
                oldLoc = null;
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

        bulletLoc.SetBullet();

        Save();
    }

    public void Save()
    {
        BulletLoc[] bulletlocs = GameActor.Instance.bullets.transform.GetComponentsInChildren<BulletLoc>();

        for (int i = 0; i < bulletlocs.Length; i++)
        {
            if (bulletlocs[i].myBullet)
                PlayerPrefs.SetInt($"BulletLoc{i}", bulletlocs[i].level);
            else
            {
                if (PlayerPrefs.HasKey($"BulletLoc{i}"))
                {
                    PlayerPrefs.DeleteKey($"BulletLoc{i}");
                }
            }
        }
    }

    public static void Load()
    {
        BulletLoc[] bulletlocs = GameActor.Instance.bullets.transform.GetComponentsInChildren<BulletLoc>();

        for (int i = 0; i < bulletlocs.Length; i++)
        {
            if (PlayerPrefs.HasKey($"BulletLoc{i}"))
            {
                bulletlocs[i].level = PlayerPrefs.GetInt($"BulletLoc{i}");
                bulletlocs[i].SetBullet();
            }
        }
    }

    private RaycastHit TakeHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit;
        }
        else
        {
            return hit;
        }
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

    public static void BulletControl()
    {
        totalBullet--;

        if (totalBullet <= 0)
        {
            GameManager.PlayGun();
        }
    }

    public static float[] ListOfAllBullets()
    {
        float[] list = new float[5];

        for (int i = 0; i < GameActor.Instance.bullets.childCount; i++)
        {
            if (!GameActor.Instance.bullets.GetChild(i).GetComponent<BulletLoc>().myBullet) continue;

            float health = GameActor.Instance.bullets.GetChild(i).GetComponent<BulletLoc>().myBullet.GetComponent<Bullet>().health;

            list[i % 5] += health;
        }

        return list;
    }

    public static void SetWalls()
    {
        float[] list = ListOfAllBullets();

        int rnd = 0;

        for (int i = 0; i < 5; i++)
        {
            Transform wall = GameActor.Instance.walls[i];

            if(rnd > 2)
            {
                float totalHealth = Random.Range(0, 2) == 0 ? list[i] + 5f : list[i] - 10f;
                float health = totalHealth / wall.childCount;

                foreach (Transform obj in wall.transform)
                {
                    obj.GetComponent<Wall>().health = health;
                    obj.GetComponent<Wall>().SetColor();
                }
            }
            else
            {
                float totalHealth = list[i] - 10f;
                float health = totalHealth / wall.childCount;

                foreach (Transform obj in wall.transform)
                {
                    obj.GetComponent<Wall>().health = health;
                    obj.GetComponent<Wall>().SetColor();
                }
            }

            rnd++;
        }
    }
}
