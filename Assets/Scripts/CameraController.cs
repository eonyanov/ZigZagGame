using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;


    void OnEnable()
    {
        EventPublisher.Instance.Subscribe<PlayerCreaterEvent>( OnPlayerCreated );
    }


    void OnDisable()
    {
        EventPublisher.Instance.Unsubscribe<PlayerCreaterEvent>( OnPlayerCreated );
    }


    private void OnPlayerCreated( PlayerCreaterEvent e )
    {
        SetTarget( e.Player.transform );
    }


//    void Update()
//    {
//        transform.Rotate( Vector3.up * 10f * Time.deltaTime, Space.World );
//    }


    public void SetTarget( Transform target )
    {
        if ( _camera == null )
            _camera = GetComponent<CinemachineVirtualCamera>();

        _camera.Follow = target;
    }
}