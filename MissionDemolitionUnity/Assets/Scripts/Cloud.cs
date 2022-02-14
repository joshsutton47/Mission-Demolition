/*** 
 * Created by: Josh Sutton
 * Date created: February 14, 2022
 * 
 * Last editted by: Josh Sutton
 * Last edited:  February 14, 2022
 * 
 * Description: controls the generation of clouds
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject CloudSphere;
    public int minSpheres = 6;
    public int maxSpheres = 10;
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public float scaleYmin = 2f;

    private List<GameObject> spheres;

    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();

        int num = Random.Range(minSpheres, maxSpheres);

        for(int i=0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(CloudSphere);
            spheres.Add(sp);

            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            //Randomly assign a position
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            //Randomly assign scale
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //Adjust y scale by x distance from core
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYmin);

            spTrans.localScale = scale;


        }

    }//end Start()

    // Update is called once per frame
    void Update()
    {
        //keypress spacebar 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }

    }//end Update()

    void Restart()
    {
        foreach(GameObject sp in spheres)
        {
            Destroy(sp);

        }//end foreach
        Start();
    }//end Restart()
}
