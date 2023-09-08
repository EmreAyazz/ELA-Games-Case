using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static bool controling;

    public static bool tripleShot;
    public static bool bulletSizeUp;
    public static float fireRate;
    public static float range;

    public static bool isGameFinished;

    public float gunSpeed;

    private Transform gun;
    private float pos_MouseX, pos_PlayerX;

    private void Start()
    {
        gun = GameActor.Instance.gun;
    }

    public void Update()
    {
        if (controling)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pos_PlayerX = transform.position.x;
                pos_MouseX = Input.mousePosition.x;
                StartCoroutine(Fire());
            }
            if (Input.GetMouseButton(0))
            {
                gun.transform.position = FindPos();
                gun.Translate(gunSpeed * Time.deltaTime * Vector3.forward);
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopAllCoroutines();
            }
        }
    }

    public IEnumerator Fire()
    {
        GameObject bullet = GameActor.Instance.bulletPrefab;

        List<GameObject> guns = GameActor.Instance.guns;

        while (true)
        {
            float fireCoolDown = Mathf.Clamp((100 - fireRate) / 100f, 0.1f, 1f);

            yield return new WaitForSeconds(fireCoolDown);

            foreach (GameObject gun in guns)
            {
                if (!tripleShot)
                {
                    GameObject newBullet = Instantiate(bullet, gun.transform.GetChild(0).position, Quaternion.Euler(90f, 0f, 0f));

                    newBullet.GetComponent<Bullet>().Go();
                    newBullet.GetComponent<Bullet>().speed = 10f;

                    newBullet.GetComponent<Bullet>().level = gun.GetComponent<Gun>().level;

                    newBullet.GetComponent<Renderer>().material = GameActor.Instance.bulletLevels[gun.GetComponent<Gun>().level];

                    if (bulletSizeUp) newBullet.transform.localScale = newBullet.transform.localScale * 2f;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject newBullet = Instantiate(bullet, gun.transform.GetChild(0).position, Quaternion.Euler(90f, 0f, -15f + (i * 15f)));

                        newBullet.GetComponent<Bullet>().Go();
                        newBullet.GetComponent<Bullet>().speed = 10f;

                        newBullet.GetComponent<Bullet>().level = gun.GetComponent<Gun>().level;

                        newBullet.GetComponent<Renderer>().material = GameActor.Instance.bulletLevels[gun.GetComponent<Gun>().level];

                        if (bulletSizeUp) newBullet.transform.localScale = newBullet.transform.localScale * 2f;
                    }
                }
            }
        }
    }

    public Vector3 FindPos()
    {
        float x = Mathf.Clamp(pos_PlayerX + (Input.mousePosition.x - pos_MouseX) / 100f, -0.75f, 0.75f);
        Vector3 pos = new Vector3(x, gun.transform.position.y, gun.transform.position.z);

        return pos;
    }

    public void Play()
    {
        BulletManager.SetWalls();

        BulletLoc[] bulletLocs = GameActor.Instance.bullets.GetComponentsInChildren<BulletLoc>();
        foreach(BulletLoc bulletLoc in bulletLocs)
        {
            if (bulletLoc.myBullet)
                bulletLoc.myBullet.GetComponent<Bullet>().Go();
        }

        GameActor.Instance.mainCam.m_DefaultBlend.m_Time = 4f;
        GameActor.Instance.gunCam.SetActive(true);
        GameActor.Instance.mergeCam.SetActive(false);
    }

    public static void PlayGun()
    {
        for (int i = 0; i < GameActor.Instance.gun.childCount; i++)
        {
            GameObject gun = GameActor.Instance.gun.GetChild(i).gameObject;
            if (!GameActor.Instance.guns.Exists(o => o == gun))
            {
                gun.transform.SetParent(null);
            }
        }

        for (int i = 0; i < GameActor.Instance.guns.Count; i++)
        {
            GameActor.Instance.guns[i].transform.DOLocalMove(GameActor.Instance.gunPos[i], 1f);
        }

        GameActor.Instance.mainCam.m_DefaultBlend.m_Time = 1f;
        GameActor.Instance.gameCam.SetActive(true);
        GameActor.Instance.gunCam.SetActive(false);

        controling = true;
    }
}
