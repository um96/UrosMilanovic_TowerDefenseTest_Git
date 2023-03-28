using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    protected ObjectPools pools;
    protected Rigidbody rb;
    public GameObject Bullet;
    protected bool can_fire = true;
    protected float bullet_cd = .8f; //.8f

    protected GameMasterControls GMC;

    // Start is called before the first frame update
    private void Awake()
    {
        pools = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<ObjectPools>();
        GMC = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterControls>();

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && can_fire && !GMC.GetWaitingOnInputFlag())
        {
            Fire();
            StartCoroutine("BulletFireCooldown");
        }
    }


    protected void Fire()
    {
        GameObject a_bullet = pools.UsePool(ObjectPools.InstantiableObjects.Bullet, transform.position, Quaternion.identity);
        rb = a_bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 100f;
        GMC.ChangePoints(-1);
    }

    protected IEnumerator BulletFireCooldown()
    {
        can_fire = false;
        yield return new WaitForSeconds(bullet_cd);
        can_fire = true;
    }

    /// <summary>
    /// Rate of fire. Can buy modifiers to shoot faster.
    /// </summary>
    /// <param name="to_remove">Amount the cooldown will be substracted by. Pass a positive value to reduce ROF. Negative values treated as positive</param>
    public void UpgradeBulletCD(float to_remove)
    {
        bullet_cd -= System.Math.Abs(to_remove);
    }

    public float GetBulletCD()
    {
        return bullet_cd;
    }

}
