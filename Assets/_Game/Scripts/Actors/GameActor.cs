using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    public static GameActor Instance;

    public Transform bullets;

    public List<Vector3> gunPos;

    public Material[] bulletLevels;

    public List<GameObject> guns;

    public List<Transform> walls;

    private void Awake()
    {
        Instance = this;
    }
}
