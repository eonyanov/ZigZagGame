public class GameStateChangedEvent : AbstractEvent
{
    public readonly GameState GameState;


    public GameStateChangedEvent( GameState gameState )
    {
        GameState = gameState;
    }
}
