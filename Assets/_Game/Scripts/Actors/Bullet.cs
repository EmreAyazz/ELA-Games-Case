using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float health;

    private bool go;

    private void Update()
    {
        if (go) transform.Translate(Vector3.up * Time.deltaTime * speed);

    }

    public void Go()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = true;
        go = true;
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
                    Destroy(gameObject);
                }
            },
            "Gun" => () =>
            {
                GameActor.Instance.guns.Add(other.gameObject);

                Destroy(gameObject);
            },

            _ => () => { }
        };

        action();
    }
}
