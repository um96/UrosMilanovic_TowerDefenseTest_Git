using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Invoke("SelfTerminate", 1.1f);
    }

    /// <summary>
    /// If you miss a shot, destroy bullet once it doesnt find a target after N time
    /// There's also a bullet catching boxcollider around the level, just to be safe
    /// </summary>
    void SelfTerminate()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        gameObject.SetActive(false);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Hit an enemy
            collision.collider.GetComponent<Enemy>().BulletHit();
            SelfTerminate();
        }
        else if (collision.collider.CompareTag("BulletCatcher"))
        {
            //If for whatever reason bullet didn't selfdestruct yet, catcher gets it.
            SelfTerminate();


        }
    }

}
