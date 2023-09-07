using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health;

    public void SetColor()
    {
        GetComponent<Renderer>().material.color = new Color(health / 30f, 0f, 0f);
    }
}
