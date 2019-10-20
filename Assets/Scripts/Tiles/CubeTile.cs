using System;
using UnityEngine;

public class CubeTile : AbstractTile
{
    private BoxCollider _boxCollider;

    public override Vector3 Center => transform.position + _boxCollider.center + Vector3.up * _boxCollider.size.y / 2f;


    public override void Init( int width, AbstractSubTile subTilePrefab )
    {
        base.Init( width, subTilePrefab );

        if ( _boxCollider == null )
            _boxCollider = gameObject.AddComponent<BoxCollider>();

        _boxCollider.size = new Vector3( width, 1, width );
        var offset = 0.5f * width - 0.5f;
        _boxCollider.center = new Vector3( offset, -0.5f, -offset );

        for ( var i = 0; i < width; i++ )
        {
            for ( var j = 0; j < width; j++ )
            {
                var subTile = CreateSubTile();
                subTile.transform.SetParent( transform );
                subTile.transform.localPosition = new Vector3( i, 0, -j );
                _subTiles.Add( subTile );
            }
        }
    }
}