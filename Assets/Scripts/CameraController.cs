using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;


    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        MainController.Instance.GameStateChaged += OnGameStateChaged;
    }


    private void OnGameStateChaged( GameState gameState )
    {
        switch ( gameState )
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                SetTarget( MainController.Instance.Ball.transform );
                break;
            case GameState.GameOver:
                SetTarget( null );
                break;
        }
    }


//    void Update()
//    {
//        transform.Rotate( Vector3.up * 10f * Time.deltaTime, Space.World );
//    }


    public void SetTarget( Transform target )
    {
        _camera.Follow = target;
    }


    void OnDestroy()
    {
        MainController.Instance.GameStateChaged += OnGameStateChaged;
    }
}