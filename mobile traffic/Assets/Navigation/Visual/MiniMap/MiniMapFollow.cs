using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    

    private float distanceahead = 30f;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CameraFollowPosition = player.position+player.forward*distanceahead;
        CameraFollowPosition.y = transform.position.y;
        transform.position = CameraFollowPosition;
    }
}
