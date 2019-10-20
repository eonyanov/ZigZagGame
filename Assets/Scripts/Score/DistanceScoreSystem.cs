/// <summary>
/// Ведет счет на основе пройденной дистанции.
/// Реализации нет, просто для примера.
/// </summary>
public class DistanceScoreSystem : AbstractScoreSystem
{
    public DistanceScoreSystem( Ball ball ) : base( ball )
    {
    }
}


/// <summary>
/// Ведет счет на основе количества смены направлений игрока
/// Реализации нет, просто для примера.
/// </summary>
public class ChangeDirectionScoreSystem : AbstractScoreSystem
{
    public ChangeDirectionScoreSystem( Ball ball ) : base( ball )
    {
    }
}

