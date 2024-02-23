using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject prefab; // El prefab del enemigo
    public int poolSize = 0; // El tamaño inicial del pool
    public List<GameObject> enemyPool; // La lista que contiene el pool de enemigos

    public GameObject GetObjFromPool()
    {
        // Busca un enemigo inactivo en el pool
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        // Si no hay enemigos inactivos, crea uno nuevo y lo añade al pool
        GameObject newEnemy = Instantiate(prefab);

        enemyPool.Add(newEnemy);

        return newEnemy;
    }
}
