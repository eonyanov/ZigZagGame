using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeLevelGenerator : AbstractGenerator
{
    public CubeLevelGenerator( AbstractSubTile subTilePrefab, int startPlatformWidth,
        AbstractDiamondGenerator diamondGenerator ) : base( subTilePrefab, startPlatformWidth, diamondGenerator )
    {
    }


    protected override AbstractTile CreateStartPlatform( Action<AbstractTile> onCollisionExitAction )
    {
        var tile = GenerateTile( _startPlatformWidth, _subTilePrefab, onCollisionExitAction );
        _nextTilePos = Vector3.right * _startPlatformWidth;
        return tile;
    }


    protected override AbstractTile CreateTile()
    {
        var go = new GameObject( "Tile" );
        return go.AddComponent<CubeTile>();
    }


    protected override void UpdateNextPos()
    {
        var direction = Random.Range( 0, 2 ) == 1 ? Vector3.right : Vector3.forward;
        _nextTilePos += direction * _width;
    }
}