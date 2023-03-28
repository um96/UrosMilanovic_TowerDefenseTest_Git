using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{

    public GameObject BulletPrefab;
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject TowerPrefab;

    //Using enums to compare object types
    [HideInInspector]
    public enum InstantiableObjects
    {
        Bullet,
        Enemy1,
        Enemy2,
        Tower
    }
      

    class Pool
    {
        //amount of objects to pool. There won't be a need to have more than this amount on screen at any point
        public int max_object_amt;
        //the prefab to pool
        public GameObject object_to_pool;
        //type of object
        public InstantiableObjects obj_type;

        public Pool (int amt, GameObject _object, InstantiableObjects _type)
        {
            max_object_amt = amt;
            object_to_pool = _object;
            obj_type = _type;
        }

    }

    Dictionary<InstantiableObjects, Queue<GameObject>> pool_dict;
    List<Pool> pool_list = new List<Pool>();


    void Start()
    {
        pool_dict = new Dictionary<InstantiableObjects, Queue<GameObject>>();

        //Create a bunch of data containers telling our queue how to react to different prefabs

        Pool pool1 = new Pool(50, BulletPrefab, InstantiableObjects.Bullet);
        Pool pool2 = new Pool(40, Enemy1Prefab, InstantiableObjects.Enemy1);
        Pool pool3 = new Pool(20, Enemy2Prefab, InstantiableObjects.Enemy2);
        Pool pool4 = new Pool(20, TowerPrefab,  InstantiableObjects.Tower);
        pool_list.Add(pool1);
        pool_list.Add(pool2);
        pool_list.Add(pool3);
        pool_list.Add(pool4);
        
        //Instantiate all objects and hide them, then add them to the main pool dictionary, which will control if an object is active or inactive
        foreach (Pool pool in pool_list)
        {
            Queue<GameObject> a_Pool = new Queue<GameObject>();

            for(int i = 0; i < pool.max_object_amt; i++)
            {
                GameObject instnt_obj = Instantiate(pool.object_to_pool);
                instnt_obj.SetActive(false);
                a_Pool.Enqueue(instnt_obj);
            }
            pool_dict.Add(pool.obj_type, a_Pool);
        }


    }

    //Enable and place object in the proper position. All spanwers call this.
     public GameObject UsePool(InstantiableObjects obj_type, Vector3 location, Quaternion rot)
    {
        GameObject to_spawn = pool_dict[obj_type].Dequeue();
        to_spawn.SetActive(true);
        to_spawn.transform.position = location;
        to_spawn.transform.rotation = rot;

        pool_dict[obj_type].Enqueue(to_spawn);
        return to_spawn;
    }
}
