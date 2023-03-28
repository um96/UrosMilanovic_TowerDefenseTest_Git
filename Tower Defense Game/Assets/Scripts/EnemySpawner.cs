using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    ObjectPools pools;




    List<int> enemies_per_lvl;

    public GameObject StartPos;


    GameMasterControls GMC;

    /// <summary>
    /// Every position where a big enemy should spawn. Ie: 10 would replace the 10th enemy with a more powerful ("Big") enemy.
    /// </summary>
    int BigEnemyPos = 10;

    private void Awake()
    {
        GMC = GetComponent<GameMasterControls>();
        pools = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<ObjectPools>();
        GMC.DifficultyLevel = 0;
        enemies_per_lvl = new List<int>();
        GMC.active_GOs = new List<GameObject>();
        int starter_amt = 10;
        for(int y = 0; y < GMC.max_lvl; y++)
        {
            enemies_per_lvl.Add(starter_amt);
            starter_amt += 6;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(!GMC.round_in_progress && !GMC.GetWaitingOnInputFlag())
        {
            GMC.round_in_progress = true;
            GMC.enemies_exist = true;
            BeginEnemySpawn();
        }
    }


    public void EndRound()
    {
        GMC.SetWaitingOnPlayerFlag(true);
        GMC.round_in_progress = false;
        GMC.enemies_exist = false;
        IncreaseDifficultyLevel();
    }

    /// <summary>
    /// Called on every round end
    /// </summary>
    void IncreaseDifficultyLevel()
    {
        GMC.DifficultyLevel++;
        Debug.Log("Increasing Difficulty! level is now: " + GMC.DifficultyLevel);

        BigEnemyPos = BigEnemyPos - 2;

        if (GMC.DifficultyLevel == GMC.max_lvl)
        {
            //Game over, you won!
            Debug.Log("Game Over!");
            GMC.round_in_progress = false;
            enabled = false;
            GMC.OnGameEnd();
        }
        else
        {
            GMC.OnRoundEnd();
        }
    }


    void BeginEnemySpawn()
    {
        StartCoroutine("AllSpawn");
    }

    /// <summary>
    /// Using a Coroutine to spawn enemies in order to easily control both delay until spawning begins, and delay between each enemy spawns
    /// </summary>
    /// <returns></returns>
    IEnumerator AllSpawn()
    {
        GMC.spawning_in_progress = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < enemies_per_lvl[GMC.DifficultyLevel]; i++)
        {
            //Debug line to double check which eenmy is spawning!
            //Debug.Log(i + " " + ((i + 1) % BigEnemyPos == 0) + " " + BigEnemyPos);

            //Make every nth spawn be a big enemy -OR- if BigEnemyPos is 0, only spawn Big Enemies
            if (((i + 1 ) % BigEnemyPos == 0 && (BigEnemyPos != 0 && i != 0)) || BigEnemyPos == 0)
            {
                //Spawn Big enemy!
                Spawn(ObjectPools.InstantiableObjects.Enemy2);
            }
            else
            {
                //Spawn Normal Enemy
                Spawn(ObjectPools.InstantiableObjects.Enemy1);
            }
            yield return new WaitForSeconds(.8f);

        }
        GMC.spawning_in_progress = false;
    }

    /// <summary>
    /// Create enemy and reset directions to end-goal. Using this instead of the UsePool function directly in order to have any enemy specific things
    /// that must be toggled/enabled in one spot, since we can have N amount of enemies
    /// </summary>
    /// <param name="an_obj"></param>
    void Spawn(ObjectPools.InstantiableObjects an_obj)
    {
        GameObject enemy = pools.UsePool(an_obj, StartPos.transform.position,Quaternion.identity);
        GMC.active_GOs.Add(enemy);
        enemy.GetComponent<Enemy>().SetDestination();
    }



}
