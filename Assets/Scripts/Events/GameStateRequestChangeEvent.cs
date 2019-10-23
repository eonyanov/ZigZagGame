public class GameStateRequestChangeEvent : AbstractEvent
{
    public readonly GameState NewGameState;


    public GameStateRequestChangeEvent( GameState newGameState )
    {
        NewGameState = newGameState;
    }
}