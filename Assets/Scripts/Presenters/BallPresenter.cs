using System;
using Vector3 = UnityEngine.Vector3;

public class BallPresenter : IBallPositionManipulator
{
    private BallCompressionView _ballCompressionView;
    private BallColorView _ballColorView;
    private BallRotationView _ballRotationView;
    private BallView _ballView;
    private LineVelocityModel _lineVelocityModel;

    public event Action BallClickedDown;
    public event Action BallClickedUp;

    public BallPresenter(BallCompressionView ballCompressionView, BallColorView ballColorView, LineVelocityModel lineVelocityModel, BallRotationView ballRotationView, BallView ballView)
    {
        _ballCompressionView = ballCompressionView;
        _ballColorView = ballColorView;
        _lineVelocityModel = lineVelocityModel;
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
        _ballView.RequestLineVelocity += OnLineVelocityRequested;
    }

    public void Disable()
    {
        _ballView.RequestNewVelocity -= OnVelocityRequested;
        _ballView.MouseClickedDown -= OnMouseClickedDown;
        _ballView.MouseClickedUp -= OnMouseClickedUp;
        _ballView.WallIsHit -= OnWallHit;
        _ballView.RequestRotation -= OnRotationRequested;
        _ballView.VelocityChanged -= OnVelocityChanged;
        _ballView.RequestLineVelocity -= OnLineVelocityRequested;
    }

    public Vector3 GetBallPosition()
    {
        return _ballView.Position;
    }

    private void OnVelocityRequested(Vector3 currentPosition, Vector3 previousPosition)
    {
        _ballView.SetVelocity(CustomVelocity.CalculateVelocity(currentPosition, previousPosition));
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

    private void OnLineVelocityRequested(Vector3 targetPosition, Vector3 currentPosition)
    {
        _ballView.SetVelocity(_lineVelocityModel.CalculateLineVelocity(targetPosition, currentPosition));
    }

    private void OnWallHit(Vector3 ballVelocity, Vector3 contactPointNormal)
    {
        _ballColorView.FlashColor();
        _ballCompressionView.CompressBall();
        Vector3 reflectedVelocity = CustomVelocity.ReflectVelocity(ballVelocity, contactPointNormal);
        _ballView.SetVelocity(reflectedVelocity);
        _ballRotationView.ChangeDirection(reflectedVelocity);
    }
}
