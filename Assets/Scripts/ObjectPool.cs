using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    // список уникальных объектов для клонирования новых
    private List<GameObject> _entries = new List<GameObject>();

    // сам пул
    private List<List<GameObject>> _pool = new List<List<GameObject>>();


    void Awake()
    {
        Instance = this;
    }


    public GameObject GetPooledObject( GameObject obj )
    {
        for ( var i = 0; i < _entries.Count; i++ )
        {
            if ( _entries[i].name != obj.name )
                continue;

            if ( _pool[i].Count > 0 )
            {
                var pooledObject = _pool[i][0];

                _pool[i].RemoveAt( 0 );
                pooledObject.transform.SetParent( null );
                pooledObject.SetActive( true );
                return pooledObject;
            }

            var go = Instantiate( _entries[i] );
            go.name = obj.name;
            return go;
        }

        _entries.Add( obj );
        var go1 = Instantiate( obj );
        go1.name = obj.name;
        _pool.Add( new List<GameObject>() );
        return go1;
    }


    public void PoolObject( GameObject obj )
    {
        if ( obj == null ) return;

        var isFinded = false;
        int i;
        for ( i = 0; i < _entries.Count; i++ )
        {
            if ( _entries[i].name != obj.name ) continue;
            isFinded = true;
            break;
        }

        if ( !isFinded )
        {
            _entries.Add( obj );
            _pool.Add( new List<GameObject>() );
        }


        if ( _pool[i].Contains( obj ) ) return;

        obj.SetActive( false );
        obj.transform.SetParent( gameObject.transform, false );
        _pool[i].Add( obj );
    }


    public void PoolObject( GameObject obj, float time )
    {
        StartCoroutine( DelayCoroutine( obj, time ) );
    }


    IEnumerator DelayCoroutine( GameObject go, float t )
    {
        yield return new WaitForSeconds( t );
        PoolObject( go );
    }
}