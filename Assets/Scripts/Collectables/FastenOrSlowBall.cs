using Assets.Scripts;


public class FastenOrSlowBall : Collectable
{
    public float SpeedMultiplier = 2.5f;

    protected override void ApplyEffect()
    {
        if (BallsManager.Instance.Balls.Count > 0 && !BallsManager.Instance.SpeedChanged)
        {
            BallsManager.Instance.UpdateBallsSpeed(SpeedMultiplier);
        }
        
    }
}