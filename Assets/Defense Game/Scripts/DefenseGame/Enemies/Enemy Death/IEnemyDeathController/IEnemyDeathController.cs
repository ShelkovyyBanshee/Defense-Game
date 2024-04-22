using System;


public interface IEnemyDeathController
{
    public event Action onWasDefeated;

    public bool IsDefeated { get; }
}