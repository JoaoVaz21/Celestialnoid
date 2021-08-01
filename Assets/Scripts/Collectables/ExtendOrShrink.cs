using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendOrShrink : Collectable
{
    public float _newWidth = 2.5f;
    protected override void ApplyEffect()
    {
       if(Paddle.Instance!=null && !Paddle.Instance.PaddleIsTransforming)
        {
            Paddle.Instance.StartWidthAnimation(_newWidth);
        }
    }
}
