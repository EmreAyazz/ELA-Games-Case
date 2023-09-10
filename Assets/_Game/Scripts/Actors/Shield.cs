using Collectable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            CollectableManager.AddShield(1);

            Destroy(gameObject);
        }
    }
}
