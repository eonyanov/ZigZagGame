using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractDiamondGenerator
{
    private readonly List<Transform> _tiles;

    protected GameObject _diamondPrefab;
    protected readonly int _blockSize = 1;


    public AbstractDiamondGenerator( GameObject diamondPrefab, int blockSize )
    {
        _diamondPrefab = diamondPrefab;
        _blockSize = blockSize;
        _tiles = new List<Transform>();
    }


    public virtual void ProcessTiles( Transform[] tiles )
    {
        foreach ( var t in tiles )
        {
            _tiles.Add( t );
            Generate();
        }
    }


    protected virtual GameObject CreateDiamond( Transform parenTransform )
    {
        return Object.Instantiate( _diamondPrefab, parenTransform );
    }


    protected virtual void Generate()
    {
        if ( _tiles.Count < _blockSize ) return;

        var index = GetIndex();

        var diamond = CreateDiamond( _tiles[index] );

        var center = _tiles[index].GetComponent<ICenter>();

        if ( center != null )
            diamond.transform.position = center.Center;
        
        _tiles.Clear();
    }


    protected abstract int GetIndex();
}