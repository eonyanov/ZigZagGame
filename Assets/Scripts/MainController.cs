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


    public event Action<GameState> GameStateChaged = state => { };

    public AbstractScoreSystem ScoreSystem { get; private set; }
    public Ball Ball { get; private set; }

    public GameState GameState { get; private set; }
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
        if ( Ball != null )
            Destroy( Ball.gameObject );

        CreateBall( _difficultyParams.PlayerSpeed );

        Ball.transform.position = _generator.Generate( _difficultyParams.PathWidth );

        ScoreSystem = new DiamondScoreSystem( Ball );

        SetGameState( GameState.Playing );
    }


    private void SetGameState( GameState gameState )
    {
        GameState = gameState;
        GameStateChaged?.Invoke( GameState );
    }


    private void BallOnDie()
    {
        Destroy( Ball.gameObject );

        SetGameState( GameState.GameOver );
    }


    private void CreateBall( float speed )
    {
        Ball = Instantiate( _ballPrefab );
        Ball.Speed = speed;
        Ball.Die += BallOnDie;
    }


    public AbstractInput GetInput => new MouseInput();
}