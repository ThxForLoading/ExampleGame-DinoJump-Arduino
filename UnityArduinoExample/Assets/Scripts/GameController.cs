using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameState currentState;
    public float gameTimer;

    public static GameController instance;

    private ArduinoComms _arduinoController;

    private int _score;
    private int _highScore;

    [SerializeField] TextMeshProUGUI _scoreTMP;
    [SerializeField] TextMeshProUGUI _highScoreTMP;

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
        currentState = GameState.Starting;
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

        if( gameTimer > 0)
        {
            _score = (int) gameTimer;
        }

        if ((currentState == GameState.Starting || currentState == GameState.GameOver) && gameTimer != 0)
        {
            if(_score > _highScore) _highScore = _score;

            _score = 0;
            gameTimer = 0;
        }

        _scoreTMP.text = _score.ToString();
        _highScoreTMP.text = _highScore.ToString();
    }

    public void PlayerInput()
    {
        if(currentState == GameState.Starting)
        {
            currentState = GameState.Running;
        }
        if(currentState == GameState.GameOver)
        {
            currentState = GameState.Starting;
            ResetGame();
        }
    }

    public void ResetGame()
    {
        GameObject gr = GameObject.FindGameObjectWithTag("GroundController");
        if(gr.TryGetComponent<GroundController>(out GroundController gc))
        {
            gc.ClearAllObstacles();
        }
    }

}

public enum GameState
{
    None,
    Starting,
    Running,
    GameOver
}
