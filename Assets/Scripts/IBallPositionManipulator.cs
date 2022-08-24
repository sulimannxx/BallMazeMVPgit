using System;
using UnityEngine;

public interface IBallPositionManipulator : IPresenter
{
    public event Action BallClickedDown;
    public event Action BallClickedUp;

    public Vector3 GetBallPosition();
}