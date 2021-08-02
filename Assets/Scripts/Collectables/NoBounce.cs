using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBounce : Collectable
{
    protected override void ApplyEffect()
    {
        foreach(var ball in BallsManager.Instance.Balls)
        {
            ball.StartLightningBall();
        }
    }
}
