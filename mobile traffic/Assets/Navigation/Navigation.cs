using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation:MonoBehaviour
{
    public Transform Mycar;
    public GameObject wp;//array of waypoints
    public static Transform Curwaypoint;
    public static Vector3 CurForward;
    public static bool TogiveInstruction;

    protected Directions nextdirection;
    protected static int WayPointTag;

    private Directions[] dirs;
    private GpsModal modal;

    [SerializeField]
    private GameObject visualguide;
    [SerializeField]
    private GameObject audioguide;

    public  enum Directions
    {
        straight,
        left,
        right,
        back
    }
    public enum GpsModal
    {
        visual,
        audio,
        compound,
        None
    }
    private void Awake()
    {
        modal = GpsModal.audio;//dependes on the input 
        WayPointTag= 0;
       
        dirs = new Directions[] { Directions.straight, Directions.straight, Directions.left, Directions.straight };

    }
    private void Start()
    {
        StartNavigation(modal);
        TogiveInstruction = true;



    }

    protected Directions getNextDirection()
    {
        float angle = GetGuideAngle(WayPointTag);

        if (angle < -90)
            return Directions.left;
        else if (angle > 90)
            return Directions.right;

        else
            return Directions.straight;
    }
     protected Directions getNextDirection(int waypointtag)
    {
        return dirs[waypointtag];
    }  


private  float GetGuideAngle(int waypointtag)//to calculate the angle between current waypoint and the next
    {
        Transform prepoint = Curwaypoint;
        Transform nextpoint = wp.transform.GetChild(WayPointTag+1).transform;
     
        Vector3 from= prepoint.forward;
        Vector3 to = nextpoint.position - prepoint.position;
        Vector3 cross = Vector3.Cross(from, to);
        float angle = Vector3.Angle(from, to);
        angle = cross.y > 0 ? -angle : angle;

        Debug.Log(angle);
        return angle;
    }

    //calculate the angle to the next waypoint. Instructions are accroding to the angle.
    
        private void StartNavigation(GpsModal modal)
    {
        switch (modal)
        {
            case (GpsModal.audio):
                audioguide.GetComponent<AudioNavigator>().enabled = true;
                break;
            case (GpsModal.visual):
                visualguide.GetComponent<VisualNavigator>().enabled = true;
                break;
            case (GpsModal.compound):
                audioguide.GetComponent<AudioNavigator>().enabled = true;
                visualguide.GetComponent<VisualNavigator>().enabled = true;
                break;
        }
    }
    
     class Route
    {
        public GameObject waypoint;
        private Vector3 forward;

    }
    //update the according to the position of the car and waypoints
}
