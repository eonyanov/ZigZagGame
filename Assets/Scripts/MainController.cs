using System;
using System.Linq;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController Instance;

    [SerializeField] private AbstractInput _input;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private AbstractSubTile _subTilePrefab;
    [SerializeField] private GameObject _diamondPrefab;
    [SerializeField] private bool _randomDiamondGeneration = true;
    [SerializeField] private int _startPlatformWidth = 3;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private GameSettings.Difficulty _gameDifficulty = GameSettings.Difficulty.Easy;


    public AbstractScoreSystem ScoreSystem { get; private set; }


    private GameState _gameState;

    private Ball _player;
    private AbstractGenerator _generator;

    private GameSettings.DifficultyParams _difficultyParams;


    private void Awake()
    {
        if ( Instance != null )
        {
            Destroy( gameObject );
            return;
        }

        Instance = this;
        DontDestroyOnLoad( gameObject );

        EventPublisher.Instance.Subscribe<GameStateRequestChangeEvent>( OnRequestChangeGameState );
    }


    void OnDisable()
    {
        EventPublisher.Instance.Unsubscribe<GameStateRequestChangeEvent>( OnRequestChangeGameState );
    }


    private void OnRequestChangeGameState( GameStateRequestChangeEvent e )
    {
        switch ( e.NewGameState )
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                StartGame();
                break;
            case GameState.GameOver:
                break;
        }
    }


    // Start is called before the first frame update
    private void Start()
    {
        SetGameState( GameState.Menu );

        _difficultyParams = _gameSettings.Params.First( param => param.Difficulty == _gameDifficulty );

        var diamondGenerator = _randomDiamondGeneration
            ? (AbstractDiamondGenerator) new RandomDiamondGenerator( _diamondPrefab, 5 )
            : new SeriesDiamondGenerator( _diamondPrefab, 5 );

        _generator = new CubeLevelGenerator( _subTilePrefab, _startPlatformWidth, diamondGenerator );
    }


    public void StartGame()
    {
        if ( _player != null )
            Destroy( _player.gameObject );

        CreateBall( _difficultyParams.PlayerSpeed );

        _player.transform.position = _generator.Generate( _difficultyParams.PathWidth );

        ScoreSystem = new DiamondScoreSystem( _player );

        SetGameState( GameState.Playing );
    }


    private void SetGameState( GameState gameState )
    {
        _gameState = gameState;
        EventPublisher.Instance.Publish( new GameStateChangedEvent( _gameState ) );
    }


    private void BallOnDie()
    {
        Destroy( _player.gameObject );

        SetGameState( GameState.GameOver );
    }


    private void CreateBall( float speed )
    {
        _player = Instantiate( _ballPrefab );
        _player.Speed = speed;
        _player.Die += BallOnDie;

        EventPublisher.Instance.Publish( new PlayerCreaterEvent( _player ) );
    }


    public AbstractInput GetInput => new MouseInput();
}