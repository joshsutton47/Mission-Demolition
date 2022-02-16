/*** 
 * Created by: Josh Sutton
 * Date created: February 9, 2022
 * 
 * Last editted by: Josh Sutton
 * Last edited:  February 16, 2022
 * 
 * Description: Controls the camera movement and projectile tracking
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    #region Variables
    /// Variables ///
    static public GameObject POI;

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header ("Set Dynamically")]
    public float camZ;
    
    #endregion

    private void Awake()
    {
        camZ = this.transform.position.z;
    }//end Awake()

    private void FixedUpdate()
    {
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
            destination.z = camZ;
            transform.position = destination;
        }
        else
        {
            destination = POI.transform.position;
        }

        if(POI.tag == "Projectile")
        {
            if (POI.GetComponent<Rigidbody>().IsSleeping())
            {
                POI = null;
                return;
            }
        }


        //Limit the X and Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        //Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;

    }//end FixedUpdate()

}
