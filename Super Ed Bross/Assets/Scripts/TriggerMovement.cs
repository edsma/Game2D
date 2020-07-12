using Assets.Scripts.Constans;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public bool movingForward;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {

        if(otherCollider.tag == Constants.tags.collectable)
        {
            return;
        }

        if(movingForward)
        {
            Enemy.turnAround = movingForward;
        }
        else
        {
            Enemy.turnAround = movingForward;
        }
        movingForward = !movingForward;    
    }

}
