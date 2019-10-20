using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesDiamondGenerator : AbstractDiamondGenerator
{
    private readonly List<Transform> _tiles;
    private int _counter;


    public SeriesDiamondGenerator( GameObject diamondPrefab, int blockSize ) : base( diamondPrefab, blockSize )
    {
    }


    protected override int GetIndex()
    {
        var counter = _counter;

        _counter++;
        if ( _counter == _blockSize )
            _counter = 0;

        return counter;
    }
}