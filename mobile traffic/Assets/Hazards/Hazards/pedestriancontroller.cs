using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedestriancontroller:MonoBehaviour {
    public Transform CollisionPoint;
    private static float speed;
    private static float TimeToArrival;// time for menuever
   
    private void  Awake()
    {
        speed = 1.6f;
        TimeToArrival = 2.5f;
       
    }
    public void Wakling()
    {
      this.GetComponent<Animator>().SetBool("istriggered", true);
     transform.Translate(-transform.forward * speed * Time.deltaTime);
    }
    private float CalculateTTA(Transform cartrans,float targetspeed)
    {
        float distance = Mathf.Abs(cartrans.position.x-CollisionPoint.position.x);//distance from pedstrian's projection to the user car's forward
        
        return distance / targetspeed;
    }
    public bool isWithinTTA(Transform cartrans,float targetspeed) //to tell whether it is the time to trigger the pedestrian
    {

        return CalculateTTA(cartrans, targetspeed) < TimeToArrival;
    }
   
}
