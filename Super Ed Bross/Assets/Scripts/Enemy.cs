using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    private Rigidbody2D rigidBody;

    public static bool turnAround;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;
        if(turnAround)
        {
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180.0f, 0);
        }
        else
        {
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        switch (GameManager.sharedInstance.currentGameState)
        {
            case GameState.menu:
                break;
            case GameState.inGame:
                //if (this.rigidBody.velocity.x < runningSpeed && this.rigidBody.velocity.x > -runningSpeed)
                //{
                    rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
                //}
                break;
            case GameState.gameOver:
                break;
            default:
                break;
        }

      
    }
}
