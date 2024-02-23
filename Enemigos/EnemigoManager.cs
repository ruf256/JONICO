using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoManager : MonoBehaviour
{
    ChefPoolManager chef;
    DelfinPoolManager delfin;
    SolPool sol;
    [SerializeField] Transform[] chefSpawnPoints;
    [SerializeField] Transform[] delfinSpawnPoints;
    [SerializeField] Transform[] solSpawnPoints;

    ArrayList solesActivos = new ArrayList();

    int solSPcount = -1;

    int stateCounter = -1;
    float timerSpawn;
    [SerializeField] private float cooldownSpawn;

    void Awake()
    {
        delfin = GetComponentInChildren<DelfinPoolManager>();
        chef = GetComponentInChildren<ChefPoolManager>();
        sol = GetComponentInChildren<SolPool>();
        timerSpawn = cooldownSpawn / 2;
    }

    private void Start()
    {
        StateManager.Instance.CambioEstado += CambioEstado;
    }

    void Update()
    {

        switch (stateCounter)
        {
            case 0:
                
                if(timerSpawn > cooldownSpawn)
                {
                    SpawnDelfin();
                }
                else
                {
                    timerSpawn += Time.deltaTime;
                }
                
                break;
            case 1:
                if (timerSpawn > cooldownSpawn)
                {
                    SpawnDelfin();
                    SpawnChef();
                }
                else
                {
                    timerSpawn += Time.deltaTime;
                }
                break;
            case 2:
                if (timerSpawn > cooldownSpawn)
                {
                    SpawnDelfin();
                    SpawnChef();
                    //TO DO: sol counter;
                    StartCoroutine(ContarSolesActivos());

                    if (StateManager.Instance.timer > 330f)
                    {
                        cooldownSpawn *= 0.4f;
                    }
                }
                else
                {
                    timerSpawn += Time.deltaTime;
                }
                break;
        }

    }

    private void SpawnDelfin()
    {
        int d = Random.Range(1, 6);
        StartCoroutine(SpawnEnemigo(d, delfin, delfinSpawnPoints));
        timerSpawn = -d * 2;
    }

    private void SpawnChef()
    {
        int c = Random.Range(1, 6);
        StartCoroutine(SpawnEnemigo(c, chef, chefSpawnPoints));
        timerSpawn = -c * 2;
    }

    private void SpawnSol()
    {
        int c = Random.Range(1, 4);
        if(solesActivos.Count < 2 || solesActivos == null)
        {
            StartCoroutine(SpawnEnemigoNoSuperposicion(c, sol, solSpawnPoints));
            timerSpawn = -c * 2.25f;
        }
        
    }

    IEnumerator SpawnEnemigo(float q, PoolManager pool, Transform[] sp)
    {
        for (int i = 0; i < q; i++)
        {
            GameObject c = pool.GetObjFromPool();
            c.transform.position = sp[Random.Range(0, sp.Length)].transform.position;
            yield return null;
        }
    }

    IEnumerator SpawnEnemigoNoSuperposicion(float q, PoolManager pool, Transform[] sp)
    {
        for (int i = 0; i < q; i++)
        {
            solSPcount++;
            GameObject c = pool.GetObjFromPool();
            c.transform.position = sp[solSPcount].transform.position;
            solesActivos.Add(c);
        }
        solSPcount = 0;
        yield return null;
    }

    IEnumerator ContarSolesActivos()
    {

        for (int i = 0; i < solesActivos.Count; i++)
        {
            GameObject c = (GameObject) solesActivos[i];
            if (!c.activeSelf)
            {
                solesActivos.Remove(c);
            }
        }
        SpawnSol();
        yield return null;
    }

    private void CambioEstado(object sender, System.EventArgs e)
    {
        stateCounter++;
        cooldownSpawn *= 1.75f;
        switch (stateCounter)
        {
            case 1:
                SpawnChef();
                break;
            case 2:
                SpawnSol();
                break;
            case 3:

                break;
        }
        
    }
}
