public class LinePresenter : IPresenter
{
    private IBallPositionManipulator _ballPresenter;
    private LineView _lineView;

    public LinePresenter(IBallPositionManipulator ballPositionManipulator, LineView lineView)
    {
        _ballPresenter = ballPositionManipulator;
        _lineView = lineView;
    }

    public void Enable()
    {
        _lineView.RequestBallPosition += OnBallPositionRequested;
        _ballPresenter.BallClickedDown += OnBallClickedDown;
        _ballPresenter.BallClickedUp += OnBallClickedUp;
    }

    public void Disable()
    {
        _lineView.RequestBallPosition -= OnBallPositionRequested;
        _ballPresenter.BallClickedDown -= OnBallClickedDown;
        _ballPresenter.BallClickedUp -= OnBallClickedUp;
    }

    private void OnBallPositionRequested()
    {
        _lineView.SetBallPosition(_ballPresenter.GetBallPosition());
    }

    private void OnBallClickedDown()
    {
        _lineView.EnableLine();
    }

    private void OnBallClickedUp()
    {
        _lineView.DisableLine();
    }
}
