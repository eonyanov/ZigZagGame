using System;
using UnityEngine;

/// <summary>
/// Система подсчета очков на основе собранных кристаллов.
/// </summary>
public class DiamondScoreSystem : AbstractScoreSystem
{
    public DiamondScoreSystem( Ball ball ) : base( ball )
    {
        _ball.OnTriggerEnterEvent += BallOnOnTriggerEnterEvent;
    }


    private void BallOnOnTriggerEnterEvent( Collider collider )
    {
        var pickupable = collider.GetComponentInParent<IPickUpable>();
        if ( pickupable != null )
        {
            pickupable.PickUp();
            _score++;
        }
    }
}