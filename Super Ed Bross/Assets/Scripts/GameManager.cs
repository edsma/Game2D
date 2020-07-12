using Assets.Scripts.Constans;
using Assets.Scripts.Enum;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variable que referencia al propio game manager.
    public static GameManager sharedInstance;
    
    //Variable para saber en que estado se encuentra, al inicio se encontrara en menu principal.
    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas,gameCanvas,gameOverCanvas;
    public int CollectedObjects = 0;


    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start") &&
            this.currentGameState != GameState.inGame)
        {
            StartGame();
        }

        if (Input.GetButtonDown("Pause"))
        {
            BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void StartGame()
    {
        SetGameStart(GameState.inGame);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();
        if(PlayerController.sharedInstance.transform.position.x > 10)
        {
            LevelGenerator.sharedInstance.RemoveAllTheLevelBlock();
            LevelGenerator.sharedInstance.GenerateInitialBlock(2);
        }
        PlayerController.sharedInstance.StartGame();
        this.CollectedObjects = 0;
    }

    //Metodo cuando el jugador muere
    public void GamerOver()
    {
        SetGameStart(GameState.gameOver);
    }

    //Metodo para regresar al menu principal
    public void BackToMenu()
    {
        SetGameStart(GameState.menu);
    }

    public void SetEnglishLanguage()
    {
        Text[] TextMenu = menuCanvas.GetComponentsInChildren<Text>();
        Text[] TextGame = gameCanvas.GetComponentsInChildren<Text>();
        Text[] TextGameOver = gameOverCanvas.GetComponentsInChildren<Text>(); 
        I18n.SelectLoadLanguage(Constants.Language.english);
        TextMenu[0].text = I18n.Fields["Play"];
        TextMenu[1].text = I18n.Fields["Exit"];

        TextGame[0].text = I18n.Fields["PlayGame"];
        TextGame[1].text = I18n.Fields["MaxScore"];

        TextGameOver[0].text = I18n.Fields["GameOver"];
        TextGameOver[1].text = I18n.Fields["Play"];
        TextGameOver[2].text = I18n.Fields["Exit"];
        TextGameOver[3].text = I18n.Fields["PlayGame"];

        PlayerController.sharedInstance.SelectedLanguage = "es";
    }

    //Finalizar el juego
    public void ExitGame()
    {
        //#if UNITY_EDITOR
        //    UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //                Application.Quit();
        //#endif

    }

    public void SetSpanishLanguage()
    {
        Text[] buttons = menuCanvas.GetComponentsInChildren<Text>();
        Text[] TextGame = gameCanvas.GetComponentsInChildren<Text>();
        Text[] TextGameOver = gameOverCanvas.GetComponentsInChildren<Text>();
        I18n.SelectLoadLanguage(Constants.Language.spanish);
        buttons[0].text = I18n.Fields["Play"];
        buttons[1].text = I18n.Fields["Exit"];

        TextGame[0].text = I18n.Fields["PlayGame"];
        TextGame[1].text = I18n.Fields["MaxScore"];

        TextGameOver[0].text = I18n.Fields["GameOver"];
        TextGameOver[1].text = I18n.Fields["Play"];
        TextGameOver[2].text = I18n.Fields["Exit"];
        TextGameOver[3].text = I18n.Fields["PlayGame"];
        PlayerController.sharedInstance.SelectedLanguage = "es";
    }


    //Metodo encargado de cambiar el estado del juego
    void SetGameStart(GameState newGameState)
    {

        switch (newGameState)
        {
            case GameState.menu:
                menuCanvas.enabled = true;
                gameCanvas.enabled = false;
                
                gameOverCanvas.enabled = false;
                break;
            case GameState.inGame:
                menuCanvas.enabled = false;
                gameCanvas.enabled = true;
                gameOverCanvas.enabled = false;
                break;
            case GameState.gameOver:
                menuCanvas.enabled = false;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = true;
                break;
            default:
                menuCanvas.enabled = false;
                break;
        }
        //Se asigna el nuevo estado de juego.
        this.currentGameState = newGameState;
    }

    public void CollectObject(int ObjectValue)
    {
        this.CollectedObjects += ObjectValue;
        //Debug.Log("Se llevan recogidos " + this.CollectedObjects);
    }

}
