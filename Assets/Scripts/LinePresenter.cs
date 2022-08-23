public class LinePresenter
{
    private BallPresenter _ballPresenter;
    private LineView _lineView;

    public LinePresenter(BallPresenter ballPresenter, LineView lineView)
    {
        _ballPresenter = ballPresenter;
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
