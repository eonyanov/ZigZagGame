using System;
using System.Collections.Generic;
using System.Linq;


public class EventPublisher
{
    private static EventManager<AbstractEvent> _instance = new EventManager<AbstractEvent>();

    public static EventManager<AbstractEvent> Instance =>
        _instance ?? ( _instance = new EventManager<AbstractEvent>() );
}

public class EventManager<TDataEvent> where TDataEvent : AbstractEvent
{
    readonly Dictionary<Type, HashSet<object>> _actionsDict = new Dictionary<Type, HashSet<object>>();


    public void Publish<T>( T e ) where T : TDataEvent
    {
        var type = typeof( T );
        if ( !_actionsDict.TryGetValue( type, out var hashSet ) ) return;
        foreach ( var action in hashSet.OfType<Action<T>>().ToArray() )
            action( e );
    }


    public void Subscribe<T>( Action<T> handler ) where T : TDataEvent
    {
        var type = typeof( T );
        if ( !_actionsDict.ContainsKey( type ) ) _actionsDict[type] = new HashSet<object>();
        _actionsDict[type].Add( handler );
    }


    public void Subscribe( Type type, object handler )
    {
        if ( !_actionsDict.ContainsKey( type ) ) _actionsDict[type] = new HashSet<object>();
        _actionsDict[type].Add( handler );
    }


    public void Unsubscribe<T>( Action<T> handler ) where T : TDataEvent
    {
        var type = typeof( T );
        if ( _actionsDict.TryGetValue( type, out var list ) ) list.Remove( handler );
    }


    public void Unsubscribe( Type type, object handler )
    {
        if ( _actionsDict.TryGetValue( type, out var list ) ) list.Remove( handler );
    }
}