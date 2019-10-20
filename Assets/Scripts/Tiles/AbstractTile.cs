using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTile : MonoBehaviour, ICenter
{
    public event Action<AbstractTile> CollisionExitEvent;
    protected List<AbstractSubTile> _subTiles;

    protected Rigidbody _rigidbody;

    public abstract Vector3 Center { get; }

    protected AbstractSubTile _subTilePrefab;


    public virtual void Init( int width, AbstractSubTile subTilePrefab )
    {
        if ( _rigidbody == null )
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

        if ( _subTiles == null )
            _subTiles = new List<AbstractSubTile>();

        _subTilePrefab = subTilePrefab;
    }


    protected virtual AbstractSubTile CreateSubTile()
    {
        return ObjectPool.Instance.GetPooledObject( _subTilePrefab.gameObject ).GetComponent<AbstractSubTile>();
    }


    protected virtual void OnCollisionExit( Collision collision )
    {
        var ball = collision.gameObject.GetComponent<Ball>();
        if ( ball != null )
        {
            CollisionExitEvent?.Invoke( this );
        }
    }


    public virtual void DestroyMe( bool animated )
    {
        foreach ( var subTile in _subTiles )
        {
            subTile.DestroyMe( animated );
        }
        Destroy( gameObject );
    }


    public virtual Transform[] GetSubTilesTransform()
    {
        var transforms = new Transform[_subTiles.Count];
        for ( var i = 0; i < _subTiles.Count; i++ )
        {
            transforms[i] = _subTiles[i].transform;
        }
        return transforms;
    }
}