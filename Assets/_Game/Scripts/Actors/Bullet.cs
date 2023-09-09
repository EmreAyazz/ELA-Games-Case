using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float health;

    public int level;

    private bool go;

    private void Update()
    {
        if (go) transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    public void Go()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = true;

        transform.GetChild(0).gameObject.SetActive(false);

        go = true;
    }

    public void Range(float time)
    {
        float value = time / 10f;
        Invoke("DestroyIt", value);
    }
    private void DestroyIt()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Action action = other.tag switch
        {
            "Wall" => () =>
            {
                Destroy(other.gameObject);

                health -= other.GetComponent<Wall>().health;

                if (health <= 0)
                {
                    BulletManager.BulletControl();

                    Destroy(gameObject);
                }
            },
            "Gun" => () =>
            {
                if (GameManager.controling) return;

                GameActor.Instance.guns.Add(other.gameObject);

                other.GetComponent<Gun>().level = level;

                BulletManager.BulletControl();

                Destroy(gameObject);
            },

            _ => () => { }
        };

        action();
    }
}
