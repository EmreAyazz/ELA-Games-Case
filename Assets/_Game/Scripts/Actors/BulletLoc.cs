using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletLoc : MonoBehaviour
{
    public GameObject myBullet;
    public int level;

    public void SetBullet()
    {
        GameObject bulletPrefab = GameActor.Instance.bulletPrefab;

        Vector3 height = new Vector3(0, 0.1f, 0);
        Vector3 rot = new Vector3(90f, 0, 0);

        GameObject newBullet = Instantiate(bulletPrefab, transform.position + height, Quaternion.Euler(rot));

        newBullet.transform.GetChild(0).GetComponent<TextMeshPro>().text = (level + 1).ToString();

        myBullet = newBullet;

        BulletManager.totalBullet++;

        myBullet.GetComponent<Renderer>().material = GameActor.Instance.bulletLevels[level];

        newBullet.GetComponent<Bullet>().level = level + 1;
    }
}
