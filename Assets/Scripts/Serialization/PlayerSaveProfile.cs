using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveProfile
{
    public float currentPositionX = -125f;
    public float currentPositionY = 0.4f;
    public float currentPositionZ = 121f;
    public float currentHealth = 50f;
    public int numOfHammerBeingUsed;

    /*public PlayerSaveProfile(PlayerController playerController)
    {
        //currentPosition = new float[3];
        //currentPosition[0] = playerController.transform.position.x;
        //currentPosition[1] = playerController.transform.position.y;
        //currentPosition[2] = playerController.transform.position.z;
    }*/
}
