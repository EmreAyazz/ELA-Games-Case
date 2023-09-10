using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActor : MonoBehaviour
{
    public Transform bullets;
    public Transform gun;
    public List<Transform> walls;
    public GameObject mergeCam;
    public GameObject gunCam;
    public GameObject gameCam;

    private void Start()
    {
        GameActor.Instance.bullets = bullets;
        GameActor.Instance.gun = gun;
        GameActor.Instance.walls = walls;
        GameActor.Instance.mergeCam = mergeCam;
        GameActor.Instance.gunCam = gunCam;
        GameActor.Instance.gameCam = gameCam;

        BulletManager.Load();
    }
}
