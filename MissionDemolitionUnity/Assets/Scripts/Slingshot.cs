/*** 
 * Created by: Josh Sutton
 * Date created: February 9, 2022
 * 
 * Last editted by: Josh Sutton
 * Last edited:  February 9, 2022
 * 
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    #region Variables
    /// Variables ///
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public GameObject launchPoint;
    public float velocityMult = 8f;

    [Header("Set in Dynamically")]
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    public Rigidbody projectileRB;


    #endregion
    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);

        launchPos = launchPoint.transform.position;
    }//end Awake()

    private void OnMouseEnter()
    {
        print("Slingshot: OnMouseEnter()");
        launchPoint.SetActive(true);
    }//end OnMouseEnter()

    private void OnMouseExit()
    {
        print("Slingshot: OnMouseExit()");
        launchPoint.SetActive(false);
    }//end OnMouseExit()

    private void OnMouseDown()
    {
        aimingMode = true;

        projectile = Instantiate(prefabProjectile);
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        projectileRB = projectile.GetComponent<Rigidbody>();
    }//end OnMouseDown()



    // Start is called before the first frame update
    void Start()
    {
        
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) { return; }

        //Gets the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToViewportPoint(mousePos2D);
        //Finds the delta from the launchPos to mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;
        //Limit mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //move the projectile to this new position
        Vector3 projectilePos = launchPos + mouseDelta;
        projectile.transform.position = projectilePos;

        if (Input.GetMouseButtonUp(0))
        {
            //mouse button has been released
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null; //emptys reference to instance projectile
        }

    }//end Update()
}
