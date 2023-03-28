using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    GameObject destination;
    EnemySpawner spawner;
    GameMasterControls GMC;
    /// <summary>
    /// Game Master GameObject
    /// </summary>
    GameObject GM;

    public int hitpoints;
    private int default_hitpoints;
    public Animator anim;

    private void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameMaster");
        spawner = GM.GetComponent<EnemySpawner>();
        GMC = GM.GetComponent<GameMasterControls>();
        destination = GM.GetComponent<PositionSetter>().EndPos;

        anim = GetComponent<Animator>();
        default_hitpoints = hitpoints;
    }

    public void SetDestination()

    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.SetDestination(destination.transform.position);
        hitpoints = default_hitpoints;
    }
    public void BulletHit()
    {

        hitpoints--;
        if(hitpoints <= 0)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            gameObject.SetActive(false);
            if (GMC.active_GOs.Contains(gameObject))
            {
                GMC.active_GOs.Remove(gameObject);
                if (GMC.active_GOs.Count == 0 && !GMC.spawning_in_progress)
                {
                    //All enemies are dead, call EndRound
                    spawner.EndRound();
                }
            }

            //If enemy is Big enemy, award more points
            if (default_hitpoints > 1)
            {
                GMC.ChangePoints(50);
            }
            else
            {
                GMC.ChangePoints(5);

            }

        }
        else
        {
            anim.Play("EnemyFlashingOnHit");
        }




    }


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision with " + collision.collider.name);
        if(collision.collider.CompareTag("EndGoal"))
        {
            //Kill enemy and remove points!
            GetComponent<NavMeshAgent>().enabled = false;
            gameObject.SetActive(false);
            GMC.ChangePoints(hitpoints * -30);
            GMC.DeductLifePoint();

        }
    }
}
