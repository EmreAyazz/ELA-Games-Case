using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    public static GameActor Instance;

    public Transform bullets;

    private void Awake()
    {
        Instance = this;
    }
}
