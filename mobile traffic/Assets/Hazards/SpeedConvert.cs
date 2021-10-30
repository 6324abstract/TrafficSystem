using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedVonvert
{
    private static float ms2mph = 2.2369f;
    private static float ms2kmh = 3.6f;
   
   // private float scaleofunit =1;
    // Start is called before the first frame update
  /*  public static float vel2speed(Vector3 velocity)
    {
        //return transform.InverseTransformDirection(rb.velocity).x + 0.1f;
    }*/
    public static float Vel2Mph(float magnitude)
    {
        return magnitude * ms2mph;
      
    }
    public static float Vel2Kmh(float magnitude)
    {
        return magnitude * ms2kmh;
    }
    
   
}
