using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    #region Variables
    /// Variables ///
    static public GameObject POI;
    public float camZ;

    #endregion

    private void Awake()
    {
        camZ = this.transform.position.z;
    }//end Awake()

    private void FixedUpdate()
    {
        if (POI == null) return;
        Vector3 destination = POI.transform.position;
        destination.z = camZ;
        transform.position = destination;
    }//end FixedUpdate()

    // Start is called before the first frame update
    void Start()
    {
        
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        
    }//end Update()
}
