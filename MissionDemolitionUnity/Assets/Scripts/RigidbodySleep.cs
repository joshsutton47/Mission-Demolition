/*** 
 * Created by: Josh Sutton
 * Date created: February 16, 2022
 * 
 * Last editted by: Josh Sutton
 * Last edited:  February 16, 2022
 * 
 * Description: sleeps the rigidbody
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) { rb.Sleep(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
