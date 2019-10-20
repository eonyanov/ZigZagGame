using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGenerator
{
    protected AbstractSubTile _subTilePrefab;
    protected int _width = 1;
    protected int _startPlatformWidth = 3;
    protected GameObject _container;
    protected AbstractDiamondGenerator _diamondGenerator;


    protected AbstractGenerator( AbstractSubTile subTilePrefab, int startPlatformWidth,
        AbstractDiamondGenerator diamondGenerator )
    {
        _subTilePrefab = subTilePrefab;
        _startPlatformWidth = startPlatformWidth;
        _diamondGenerator = diamondGenerator;
        _container = new GameObject( "Level" );
    }


    protected List<AbstractTile> _tiles = new List<AbstractTile>();
    protected Vector3 _nextTilePos;

    protected const int MaxTilesOnScene = 50;


    /// <summary>
    ///  Генерирует начальные тайлы уровня и возвращает точку старта
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 Generate( int width )
    {
        _width = width;

        if ( _width < 1 )
            _width = 1;

        RemoveTiles();

        _nextTilePos = Vector3.zero;

        //Start Platform
        var startPlatform = CreateStartPlatform( TileOnCollisionExitEvent );

        for ( var i = 0; i < MaxTilesOnScene; i++ )
        {
            GenerateTileAndUpdateNextPos();
        }

        return startPlatform.Center;
    }


    protected virtual void TileOnCollisionExitEvent( AbstractTile tile )
    {
        RemoveTile( tile );
    }


    protected abstract AbstractTile CreateStartPlatform( Action<AbstractTile> onCollisionExitAction );


    protected virtual AbstractTile GenerateTile( int width, AbstractSubTile subTilePrefab,
        Action<AbstractTile> onCollisionExitAction )
    {
        var tile = CreateTile();
        tile.Init( width, subTilePrefab );
        tile.transform.SetParent( _container.transform );
        tile.transform.position = _nextTilePos;
        tile.CollisionExitEvent += TileOnCollisionExitEvent;
        _tiles.Add( tile );
        return tile;
    }


    protected abstract AbstractTile CreateTile();

    protected abstract void UpdateNextPos();


    protected virtual void RemoveTile( AbstractTile tile )
    {
        if ( !_tiles.Contains( tile ) ) return;

        _tiles.Remove( tile );
        tile.DestroyMe( true );
        GenerateTileAndUpdateNextPos();
    }


    protected virtual void GenerateTileAndUpdateNextPos()
    {
        var tile = GenerateTile( _width, _subTilePrefab, TileOnCollisionExitEvent );
        //_diamondGenerator.ProcessTiles( tile.GetSubTilesTransform() );
        _diamondGenerator.ProcessTiles( new[] {tile.transform} );
        UpdateNextPos();
    }


    public virtual void RemoveTiles()
    {
        foreach ( var tile in _tiles )
        {
            tile.DestroyMe( false );
        }

        _tiles.Clear();
    }
}