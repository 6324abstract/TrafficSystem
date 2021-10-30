using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNavigator : Navigation
{
    public GameObject Arrow;
    public GameObject MiniMap;
    public GameObject NavigationArrow;
    private Transform AT;// arrow for direction
   
   private float DurationOfNavigtaion;//according to the theory of short memory 

    void Start()
    {
        AT = Arrow.transform;
      
        DurationOfNavigtaion = 3;
        MiniMap.SetActive(false);
     
    }

    void Update()
    {
       if (TogiveInstruction)
            StartCoroutine(Show(DurationOfNavigtaion));
    }

    IEnumerator Show(float Duration)
    {
        SwitchtheArrow(getNextDirection(WayPointTag));
        
         MiniMap.SetActive(true);

        
        yield return new WaitForSeconds(Duration);
         TogiveInstruction = false;
         MiniMap.SetActive(false);


    }

    private void SwitchtheArrow(Directions dir)
    {
         NavigationArrow.GetComponent<ArrowController>().ChangeSprite(dir);
       
    }
    
}
