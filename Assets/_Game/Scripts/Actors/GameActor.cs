using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    public static GameActor Instance;

    [Header("Objects")]
    public Transform bullets;
    public Transform gun;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    [Header("Level UI")]
    public GameObject inGame;
    public GameObject levelCompleted;
    public GameObject levelFailed;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI shieldText;

    [Header("All Of List")]
    public List<Vector3> gunPos;
    public Material[] bulletLevels;
    public List<GameObject> guns;
    public List<Transform> walls;

    [Header("Cameras")]
    public CinemachineBrain mainCam;
    public GameObject mergeCam;
    public GameObject gunCam;
    public GameObject gameCam;

    private void Awake()
    {
        Instance = this;
    }
}
