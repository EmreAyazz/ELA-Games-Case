using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public static MissileManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawner()
    {
        StartCoroutine(MissileSpawner());
    }
    public void StopSpawner()
    {
        StopAllCoroutines();
    }

    public IEnumerator MissileSpawner()
    {
        while (true)
        {
            float time = Random.Range(3f, 6f);

            yield return new WaitForSeconds(time);

            Vector3 spawnPos = (GameActor.Instance.gun.position + (GameActor.Instance.gun.forward * 20f));

            GameObject newMissile = Instantiate(GameActor.Instance.missilePrefab, spawnPos, Quaternion.Euler(-90f, 0f, 0f));
        }
    }
}
