using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet_AI : FireBullet
{
    GameObject target;

    //Firing mechanisms for the AI towers
    private void Start()
    {
        bullet_cd = 1.2f;
    }
    void Update()
    {
        if(!GMC.enemies_exist)
        {
            return;
        }
        //enemies exist, do stuff!
        if(target == null || !target.activeSelf)
        {
            GameObject closest_target = null;
            float closest_distance = 9999;
            float temp_distance;
            for(int i = 0; i < GMC.active_GOs.Count; i++)
            {
                temp_distance = Vector3.Distance(transform.position, GMC.active_GOs[i].transform.position); 
                if(temp_distance < closest_distance)
                {
                    closest_distance = temp_distance;
                    closest_target = GMC.active_GOs[i];
                }
            }
            target = closest_target;
        }
        else
        {
            //We already have a target!
        }
        if(target != null && target.activeSelf)
        {
            //Looking should always update even if tower can't fire to avoid "snapping -> firing" from happening.
            transform.parent.LookAt(target.transform.position);
        }

        if (can_fire && !GMC.GetWaitingOnInputFlag() && target != null && target.activeSelf)
        {
            Fire();
            StartCoroutine("BulletFireCooldown");
        }




    }

    new protected void Fire()
    {
        GameObject a_bullet = pools.UsePool(ObjectPools.InstantiableObjects.Bullet, transform.position, Quaternion.identity);
        rb = a_bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 100f;
        //GMC.ChangePoints(-1); -- Don't want AI shots to remove points!
        //Otherwise works the same.

    }
}
