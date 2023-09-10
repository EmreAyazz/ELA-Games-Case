using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool controling;

    public static bool tripleShot;
    public static bool bulletSizeUp;
    public static float fireRate;
    public static float range;

    public static bool isGameFinished;

    public float gunSpeed;

    private Transform gun;
    private float pos_MouseX, pos_PlayerX;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        range = 5f;
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
                GameActor.Instance.gun.transform.position = FindPos();
                GameActor.Instance.gun.Translate(gunSpeed * Time.deltaTime * Vector3.forward);
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopAllCoroutines();
            }
        }
    }

    public void Stop()
    {
        controling = false;

        isGameFinished = true;

        StopAllCoroutines();
    }

    public IEnumerator Fire()
    {
        GameObject bullet = GameActor.Instance.bulletPrefab;

        List<GameObject> guns = GameActor.Instance.guns;

        while (true)
        {
            float fireCoolDown = Mathf.Clamp((200 - fireRate) / 200f, 0.1f, 1f);

            yield return new WaitForSeconds(fireCoolDown);

            foreach (GameObject gun in guns)
            {
                if (!tripleShot)
                {
                    GameObject newBullet = Instantiate(bullet, gun.transform.GetChild(0).position, Quaternion.Euler(90f, 0f, 0f), LevelManager.currentLevel.transform);

                    newBullet.GetComponent<Bullet>().Go();
                    newBullet.GetComponent<Bullet>().speed = 10f;

                    newBullet.GetComponent<Bullet>().level = gun.GetComponent<Gun>().level;
                    newBullet.GetComponent<Bullet>().Range(range);

                    newBullet.GetComponent<Renderer>().material = GameActor.Instance.bulletLevels[gun.GetComponent<Gun>().level - 1];

                    if (bulletSizeUp) newBullet.transform.localScale = newBullet.transform.localScale * 2f;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject newBullet = Instantiate(bullet, gun.transform.GetChild(0).position, Quaternion.Euler(90f, 0f, -15f + (i * 15f)), LevelManager.currentLevel.transform);

                        newBullet.GetComponent<Bullet>().Go();
                        newBullet.GetComponent<Bullet>().speed = 10f;

                        newBullet.GetComponent<Bullet>().level = gun.GetComponent<Gun>().level;
                        newBullet.GetComponent<Bullet>().Range(range);

                        newBullet.GetComponent<Renderer>().material = GameActor.Instance.bulletLevels[gun.GetComponent<Gun>().level - 1];

                        if (bulletSizeUp) newBullet.transform.localScale = newBullet.transform.localScale * 2f;
                    }
                }
            }
        }
    }

    public Vector3 FindPos()
    {
        float x = Mathf.Clamp(pos_PlayerX + (Input.mousePosition.x - pos_MouseX) / 100f, -1.33f, 1.33f);
        Vector3 pos = new Vector3(x, GameActor.Instance.gun.transform.position.y, GameActor.Instance.gun.transform.position.z);

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

        GameActor.Instance.inGame.SetActive(false);
    }

    public static void PlayGun()
    {
        for (int i = GameActor.Instance.gun.childCount - 1; i >= 0; i--)
        {
            GameObject gun = GameActor.Instance.gun.GetChild(i).gameObject;
            bool found = false;
            foreach (GameObject obj in GameActor.Instance.guns)
            {
                if (obj == gun) found = true;
            }
            if (!found)
            {
                gun.transform.SetParent(LevelManager.currentLevel.transform);
                gun.tag = "Untagged";
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

        MissileManager.Instance.StartSpawner();
    }

    public static void Clear()
    {
        bulletSizeUp = false;
        tripleShot = false;
        fireRate = 0f;
        range = 5f;
    }
}
