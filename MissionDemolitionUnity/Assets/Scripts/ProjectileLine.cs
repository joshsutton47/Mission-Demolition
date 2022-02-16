using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine s;

    [Header("Set in Inspector")]
    public float minDist = 0.1f;
    private LineRenderer line;
    private GameObject POI;
    private List<Vector3> points;

    private void Awake()
    {
        s = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }
    public GameObject poi
    {
        get { return (POI); }
        set
        {
            POI = value;
            if (POI != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
        }

    public void Clear()
    {
        POI = null;
        line.enabled = false;
        points = new List<Vector3>(); ;
    }

    public void AddPoint()
    {
        Vector3 pt = POI.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist){
            return;
        }
        if(points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.Launch_Pos;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint
    {
        get
        {
            if(points == null)
            {
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if(poi == null)
        {
            if(FollowCam.POI != null)
            {
                if(FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        AddPoint();
        if (FollowCam.POI == null)
        {
            poi = null;
        }
    }
}
