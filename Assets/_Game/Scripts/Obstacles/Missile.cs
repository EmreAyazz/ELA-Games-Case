using Collectable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            if (CollectableManager.shield <= 0)
            {
                LevelManager.LevelFailed();
            }
            else
            {
                CollectableManager.RemoveShield(1);

                Destroy(gameObject);
            }
        }
    }
}
