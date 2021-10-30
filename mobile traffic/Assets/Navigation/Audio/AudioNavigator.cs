using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioNavigator :Navigation
{
    private string left = "TurnLeft";
    private string right = "TurnRight";
    private string straight = "GoStraight";

    private string NextDir;
    private Dictionary<Directions, string> Instructions;
    // Start is called before the first frame update
    void Start()
    {
        NextDir = straight;
        Instructions = new Dictionary<Directions, string>();
        Instructions.Add(Directions.left, left);
        Instructions.Add(Directions.right, right);
        Instructions.Add(Directions.straight, straight);
    }

    // Update is called once per frame
    void Update()
    {
     if (TogiveInstruction)
        {
            NextDir = Instructions[getNextDirection(WayPointTag)];
            FindObjectOfType<AudioManager>().Play(NextDir);
            TogiveInstruction = false;
        }
    }
}
