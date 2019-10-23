using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _gameOver;
    [SerializeField] private Button _startButton;

    private GameState _gameState;


    void OnEnable()
    {
        EventPublisher.Instance.Subscribe<GameStateChangedEvent>( OnGameStateChaged );
    }


    void OnDisable()
    {
        EventPublisher.Instance.Unsubscribe<GameStateChangedEvent>( OnGameStateChaged );
    }


    public void OnStartBtnClick()
    {
        EventPublisher.Instance.Publish( new GameStateRequestChangeEvent( GameState.Playing ) );
    }


    private void OnGameStateChaged( GameStateChangedEvent e )
    {
        _gameState = e.GameState;
        switch ( _gameState )
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
        if ( _gameState == GameState.Playing && MainController.Instance != null)
            _score.text = MainController.Instance.ScoreSystem.Score.ToString();
    }
}