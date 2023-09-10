using Collectable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int health;

    int coin;

    private void Start()
    {
        transform.GetChild(1).GetComponent<TextMeshPro>().text = health.ToString();

        coin = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= other.transform.GetComponent<Bullet>().level;

            transform.GetChild(1).GetComponent<TextMeshPro>().text = health.ToString();

            Destroy(other.gameObject);

            if (health <= 0)
            {
                Explosion();

                CollectableManager.AddCoin(coin);
            }
        }
        if (other.CompareTag("Gun"))
        {
            if (!GameManager.isGameFinished) LevelManager.LevelFailed();
        }
    }

    public void Explosion()
    {
        transform.GetChild(1).gameObject.SetActive(false);

        GetComponent<Collider>().enabled = false;

        Rigidbody[] rigidbodies = transform.GetChild(0).GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;

            rigidbody.AddExplosionForce(100f, transform.position, 100f);
        }
    }
}
