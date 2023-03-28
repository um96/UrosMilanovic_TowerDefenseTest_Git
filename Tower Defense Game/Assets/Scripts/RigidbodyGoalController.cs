using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyGoalController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Prevent sleeping, so collider can always detect impacts
        GetComponent<Rigidbody>().sleepThreshold = 0.0f;
    }


}
