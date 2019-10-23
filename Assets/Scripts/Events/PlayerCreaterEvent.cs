using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreaterEvent : AbstractEvent
{
    public readonly Ball Player;


    public PlayerCreaterEvent( Ball player )
    {
        Player = player;
    }
}
