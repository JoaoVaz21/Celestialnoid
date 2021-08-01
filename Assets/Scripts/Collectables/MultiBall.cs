using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : Collectable
{
    protected override void ApplyEffect()
    {
        foreach(Ball ball in BallsManager.Instance.Balls)
        {
            BallsManager.Instance.SpawnBalls(ball.gameObject.transform.position, 2);
        }
    }
}
