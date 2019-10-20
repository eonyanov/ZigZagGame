using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : AbstractInput
{
    protected override bool CheckTap()
    {
        return Input.GetMouseButtonDown( 0 );
    }
}