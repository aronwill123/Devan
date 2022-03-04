using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Transform> enemyList;
    PlayerController playerRef;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playerRef = PlayerController.Instance;
        foreach (Transform t in enemyList)
        {
            t.GetComponent<Enemy>().speed = 2;
        }
        InvokeRepeating("ClosestEnemy", 0, 1f);
    }
    void ClosestEnemy()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in enemyList)
        {
            float dist = Vector3.Distance(t.position, playerRef.transform.position);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        PlayerController.Instance.target = tMin;
    }
}