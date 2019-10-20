using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDiamondGenerator : AbstractDiamondGenerator
{
    private readonly List<Transform> _tiles;


    public RandomDiamondGenerator( GameObject diamondPrefab, int blockSize ) : base( diamondPrefab, blockSize )
    {
    }


    protected override int GetIndex()
    {
        return Random.Range( 0, _blockSize );
    }
}