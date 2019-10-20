public abstract class AbstractScoreSystem
{
    protected Ball _ball;
    protected int _score;
    public virtual int Score => _score;

    protected AbstractScoreSystem( Ball ball )
    {
        _ball = ball;
    }    
}