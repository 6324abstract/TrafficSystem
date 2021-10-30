using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private Transform followTarget;
    private Vector3 fixedDistance;
    private Vector3 tempPos;
    void Start()
    {
       // followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        followTarget = GameObject.Find("MyCar").transform;
        fixedDistance = new Vector3(0, 1.3f, 0.72f);
    }
    void FixedUpdate()
    {
        tempPos = followTarget.TransformDirection(fixedDistance) + followTarget.position;
        transform.position = Vector3.Lerp(transform.position, tempPos, Time.fixedDeltaTime * 3);
      
        //Quaternion q = Quaternion.LookRotation(Vector3.forward,followTarget.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * 3);
    }
}

