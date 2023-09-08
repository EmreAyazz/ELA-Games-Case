using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorType type;
    public int value;

    private void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = value.ToString();

        if (value >= 0) transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (type == DoorType.TripleShot || type == DoorType.BulletSizeUp) return;

            value += other.gameObject.GetComponent<Bullet>().level;

            if (value >= 0) transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;

            transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = value.ToString();

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Gun"))
        {
            if (type == DoorType.TripleShot) GameManager.tripleShot = true;
            if (type == DoorType.BulletSizeUp) GameManager.bulletSizeUp = true;
            if (type == DoorType.Range) GameManager.range += value;
            if (type == DoorType.FireRate) GameManager.fireRate += value;

            Destroy(gameObject);
        }
    }

    public enum DoorType
    {
        FireRate,
        Range,
        TripleShot,
        BulletSizeUp
    }
}
