using System;
using Vector3 = UnityEngine.Vector3;

public class BallPresenter : IPresenter
{
    private BallCompressionView _ballCompressionView;
    private BallColorView _ballColorView;
    private BallRotationView _ballRotationView;
    private BallMovement _ballMovement;
    private BallReflection _ballReflection;
    private BallView _ballView;

    public event Action BallClickedDown;
    public event Action BallClickedUp;

    public BallPresenter(BallCompressionView ballCompressionView, BallColorView ballColorView, BallMovement ballMovement, BallReflection ballReflection, BallRotationView ballRotationView, BallView ballView)
    {
        _ballCompressionView = ballCompressionView;
        _ballColorView = ballColorView;
        _ballMovement = ballMovement;
        _ballReflection = ballReflection;
        _ballRotationView = ballRotationView;
        _ballView = ballView;
    }

    public void Enable()
    {
        _ballView.RequestNewVelocity += OnVelocityRequested;
        _ballView.MouseClickedDown += OnMouseClickedDown;
        _ballView.MouseClickedUp += OnMouseClickedUp;
        _ballView.WallIsHit += OnWallHit;
        _ballView.RequestRotation += OnRotationRequested;
        _ballView.VelocityChanged += OnVelocityChanged;
    }

    public void Disable()
    {
        _ballView.RequestNewVelocity -= OnVelocityRequested;
        _ballView.MouseClickedDown -= OnMouseClickedDown;
        _ballView.MouseClickedUp -= OnMouseClickedUp;
        _ballView.WallIsHit -= OnWallHit;
        _ballView.RequestRotation -= OnRotationRequested;
        _ballView.VelocityChanged -= OnVelocityChanged;
    }

    public Vector3 GetBallPosition()
    {
        return _ballView.Position;
    }

    private void OnVelocityRequested(Vector3 currentPosition, Vector3 previousPosition)
    {
        Vector3 velocity = _ballMovement.CalculateVelocity(currentPosition, previousPosition);
        _ballView.SetVelocity(velocity);
    }

    private void OnRotationRequested(Vector3 velocity)
    {
        _ballRotationView.Rotate(velocity);
    }

    private void OnMouseClickedDown()
    {
        BallClickedDown?.Invoke();
    }

    private void OnMouseClickedUp()
    {
        BallClickedUp?.Invoke();
    }

    private void OnVelocityChanged(Vector3 velocity)
    {
        _ballRotationView.ChangeDirection(velocity);
    }

    private void OnWallHit(Vector3 ballVelocity, Vector3 contactPointNormal)
    {
        _ballColorView.FlashColor();
        _ballCompressionView.CompressBall();
        Vector3 reflectedVelocity = _ballReflection.ReflectVelocity(ballVelocity, contactPointNormal);
        _ballView.SetVelocity(reflectedVelocity);
        _ballRotationView.ChangeDirection(reflectedVelocity);
    }
}
