using Collectable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int level;

    public static GameObject currentLevel;

    public static List<GameObject> levels;

    public List<GameObject> _levels;

    private void Awake()
    {
        levels = _levels;
    }

    private void Start()
    {
        RetryLevel();

        CollectableManager.Load();
    }

    public static void NextLevel()
    {
        if (currentLevel) Destroy(currentLevel);

        level++;

        GameObject selectedLevel = levels[level % levels.Count];

        GameObject newLevel = Instantiate(selectedLevel, Vector3.zero, Quaternion.identity);

        currentLevel = newLevel;

        GameActor.Instance.inGame.SetActive(true);
        GameActor.Instance.levelCompleted.SetActive(false);
        GameActor.Instance.levelFailed.SetActive(false);

        GameManager.isGameFinished = false;
        GameManager.Clear();

        GameActor.Instance.guns.Clear();
    }

    public static void RetryLevel()
    {
        if (currentLevel) Destroy(currentLevel);

        level++;

        GameObject selectedLevel = levels[level % levels.Count];

        GameObject newLevel = Instantiate(selectedLevel, Vector3.zero, Quaternion.identity);

        currentLevel = newLevel;

        GameActor.Instance.inGame.SetActive(true);
        GameActor.Instance.levelCompleted.SetActive(false);
        GameActor.Instance.levelFailed.SetActive(false);

        GameManager.isGameFinished = false;
        GameManager.Clear();

        GameActor.Instance.guns.Clear();
    }

    public static void LevelCompleted() => LevelState(true);
    public static void LevelFailed() => LevelState(false);

    public static void LevelState(bool state)
    {
        Action action = state ? 
            () => 
            {
                GameActor.Instance.levelCompleted.SetActive(true);

                GameManager.Instance.Stop();
                MissileManager.Instance.StopSpawner();
            } : 
            () => 
            { 
                GameActor.Instance.levelFailed.SetActive(true);

                GameManager.Instance.Stop();
                MissileManager.Instance.StopSpawner();
            };

        action();
    }
}
