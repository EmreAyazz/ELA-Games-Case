using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int level;

    public static List<GameObject> levels;

    public List<GameObject> _levels;

    private void Awake()
    {
        levels = _levels;

        RetryLevel();
    }

    public static void NextLevel()
    {
        level++;

        GameObject selectedLevel = levels[level % levels.Count];

        GameObject newLevel = Instantiate(selectedLevel, Vector3.zero, Quaternion.identity);
    }

    public static void RetryLevel()
    {
        int value = level % levels.Count;
        Debug.Log(value);

        GameObject selectedLevel = levels[level % levels.Count];

        GameObject newLevel = Instantiate(selectedLevel, Vector3.zero, Quaternion.identity);
    }
}
