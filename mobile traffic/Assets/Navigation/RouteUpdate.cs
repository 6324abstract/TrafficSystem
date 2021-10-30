using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteUpdate : Navigation
{
    private void Start()
    {
        Curwaypoint = wp.transform.GetChild(WayPointTag).transform;
        transform.position = Curwaypoint.position;
        transform.rotation = Curwaypoint.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(WayPointTag++);
            Curwaypoint = wp.transform.GetChild(WayPointTag).transform;
            transform.position = Curwaypoint.position;
            transform.rotation = Curwaypoint.rotation;
            TogiveInstruction = true;
        }

    }
}
