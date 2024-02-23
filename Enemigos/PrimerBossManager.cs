using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PrimerBossManager : MonoBehaviour
{
    [SerializeField] PoseidoPoolManager poseidoManager;
    [SerializeField] Transform[] poseidoSP;
    [SerializeField] Volume bossVolume;
    [SerializeField] Transform magoBossSpawnPoint;

    float timer;
    float timerStage1;
    float timerStage2;
    int stageCounter;

    [SerializeField] private int stage1Cd;
    [SerializeField] private float stage1CdSpawn;
    [SerializeField] private float stage2CdSpawn;
    [SerializeField] GameObject magoBoss;
    MagoBoss magoBossScript;

    private void OnEnable()
    {
        timer = 0;
        timerStage1 = -10f;
        stageCounter = 1;
        GameObject mB = Instantiate(magoBoss);
        mB.transform.position = magoBossSpawnPoint.position;
        magoBossScript = mB.GetComponent<MagoBoss>();
        bossVolume.gameObject.SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        

        if (stageCounter == 1)
        {
            timerStage1 += Time.deltaTime;

            if(timerStage1 > stage1CdSpawn)
            {
                SpawnPoseido(5);
                timerStage1 = 0;
            }

            if(timer > stage1Cd)
            {
                stageCounter = 2;
                magoBossScript.Stage2();
            }
        }else if (stageCounter == 2)
        {
            timerStage2 += Time.deltaTime;
            if (timerStage2 > stage2CdSpawn)
            {
                SpawnPoseido(7);
                timerStage2 = 0;
            }
        }
        
    }

    void SpawnPoseido(int q)
    {
        StartCoroutine(SpawnEnemigo(q, poseidoManager, poseidoSP));
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
}
