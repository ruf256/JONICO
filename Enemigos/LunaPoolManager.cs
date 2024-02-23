using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaPoolManager : PoolManager
{
    void Awake()
    {
        // Inicializa el pool de enemigos
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(prefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }
}
