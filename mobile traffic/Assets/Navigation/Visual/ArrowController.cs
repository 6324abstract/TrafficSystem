using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController :Navigation
{
    public Transform MyCar;
    public SpriteRenderer spriteRenderer;
    public Sprite Left;
    public Sprite Right;
    public Sprite Straight;
  //  private float DistanceAhead = 30f;
    // Start is called before the first frame update
  void Update()
    {
      /* Vector3 arrowPosition = MyCar.position + MyCar.forward * DistanceAhead;
         arrowPosition.y = transform.position.y;
         transform.position = arrowPosition;*/
            
    }
    public void ChangeSprite(Navigation.Directions dir) 
    {
        switch (dir)
        {
            case (Navigation.Directions.left):
                spriteRenderer.sprite = Left;
                break;
            case (Navigation.Directions.right):
                spriteRenderer.sprite = Right;
                break;
            case (Navigation.Directions.straight):
                spriteRenderer.sprite = Straight;
                break;

        }
    }
  
}
