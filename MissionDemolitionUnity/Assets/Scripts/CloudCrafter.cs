/*** 
 * Created by: Josh Sutton
 * Date created: February 14, 2022
 * 
 * Last editted by: Josh Sutton
 * Last edited:  February 14, 2022
 * 
 * Description: controls the generation of the generation of clouds
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMultiplier = 0.5f;

    private GameObject[] cloudInstances;
    void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //Scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            //smaller clouds should be near ground
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //and farther away
            cPos.z = 100 - 90 * scaleU;

            //Apply transforms to the cloud
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;

            cloud.transform.SetParent(anchor.transform);

            cloudInstances[i] = cloud;

        }

    }//end Awake()

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;

            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMultiplier;

            if(cPos.x <= cloudPosMax.x)
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;

        }
    }//end Update()
}
