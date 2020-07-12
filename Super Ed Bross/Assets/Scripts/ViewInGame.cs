using Assets.Scripts.Enum;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text collectableLable;
    public Text scoreLabel;
    public Text MaxScoreLabel;

    // Update is called once per frame
    void Update()
    {
        int CurrentObjects = 0;
        switch (GameManager.sharedInstance.currentGameState)
        {
            case GameState.menu:
                break;
            case GameState.inGame:
                CurrentObjects = GameManager.sharedInstance.CollectedObjects;
                this.collectableLable.text = CurrentObjects.ToString();
                float maxScore = PlayerPrefs.GetFloat("maxScore", 0);

                float traveledDistance = PlayerController.sharedInstance.GetDistance();
                this.scoreLabel.text = I18n.Fields["PlayGame"] + "\n " + traveledDistance.ToString("f1");
                if(traveledDistance > maxScore)
                {
                    this.MaxScoreLabel.text = I18n.Fields["MaxScore"] + "\n " + traveledDistance.ToString("f1");
                }
                else
                {
                    this.MaxScoreLabel.text = I18n.Fields["MaxScore"] + "\n " + maxScore.ToString("f1");
                }
                
                break;
            case GameState.gameOver:
                CurrentObjects = GameManager.sharedInstance.CollectedObjects;
                this.collectableLable.text = CurrentObjects.ToString();

                
                break;
            default:
                break;
        }

        //if(GameManager.sharedInstance.currentGameState == GameState.inGame
        //    || GameManager.sharedInstance.currentGameState == GameState.gameOver)
        //{
        //    int CurrentObjects = GameManager.sharedInstance.CollectedObjects;
        //    this.collectableLable.text = CurrentObjects.ToString();
        //}

        //if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        //{
        //    float traveledDistance = PlayerController.sharedInstance.GetDistance();
        //    this.scoreLabel.text = I18n.Fields["PlayGame"] + "\n " + traveledDistance.ToString("f1") ;
        //}
    }
}
