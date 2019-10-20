using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSubTile : MonoBehaviour, ICenter
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _timer;

    public abstract Vector3 Center { get; }


    public virtual void DestroyMe( bool animated )
    {
        transform.SetParent( null );
        if ( animated )
            StartCoroutine( Animation() );
        else
            RemoveObj();
    }


    protected virtual IEnumerator Animation()
    {
        yield return new WaitForSeconds( Random.Range( 0f, 0.2f ) );

        var timer = _timer;

        while ( timer > 0f )
        {
            transform.Translate( Vector3.down * _speed * Time.deltaTime, Space.World );
            timer -= Time.deltaTime;
            yield return null;
        }

        RemoveObj();
    }


    protected virtual void RemoveObj()
    {
        ObjectPool.Instance.PoolObject( gameObject );
    }
}