using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameState currentState;
    public float gameTimer;

    public static GameController instance;

    private ArduinoComms _arduinoController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if( instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _arduinoController = ArduinoComms.instance;
    }

    private void Update()
    {
        if (currentState == GameState.Running)
        {
            _arduinoController.playerAlive = true;
            gameTimer = gameTimer + Time.deltaTime;
        }
        else
        {
            _arduinoController.playerAlive = false;
        }

        if ((currentState == GameState.Starting || currentState == GameState.GameOver) && gameTimer != 0) gameTimer = 0;

    }

}

public enum GameState
{
    None,
    Starting,
    Running,
    GameOver
}
