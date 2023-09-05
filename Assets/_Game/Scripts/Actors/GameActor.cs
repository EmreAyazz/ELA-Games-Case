using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    public static GameActor Instance;

    public Transform bullets;

    public Material[] bulletLevels;

    private void Awake()
    {
        Instance = this;
    }
}
