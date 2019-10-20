using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _gameOver;
    [SerializeField] private Button _startButton;

    private GameState _gameState;


    private void Start()
    {
        MainController.Instance.GameStateChaged += OnGameStateChaged;
        OnGameStateChaged( MainController.Instance.GameState );
    }


    public void OnStartBtnClick( )
    {
        MainController.Instance.StartGame(  );
    }


    private void OnGameStateChaged( GameState gameState )
    {
        _gameState = gameState;
        switch ( gameState )
        {
            case GameState.Menu:
                _startButton.gameObject.SetActive( true );
                _gameOver.gameObject.SetActive( false );
                _score.gameObject.SetActive( false );
                break;
            case GameState.Playing:
                _startButton.gameObject.SetActive( false );
                _gameOver.gameObject.SetActive( false );
                _score.gameObject.SetActive( true );
                break;
            case GameState.GameOver:
                _startButton.gameObject.SetActive( true );
                _gameOver.gameObject.SetActive( true );
                _score.gameObject.SetActive( true );
                break;
        }
    }


    private void Update()
    {
        if ( _gameState == GameState.Playing )
            _score.text = MainController.Instance.ScoreSystem.Score.ToString();
    }


    private void OnDestroy()
    {
        MainController.Instance.GameStateChaged -= OnGameStateChaged;
    }
}