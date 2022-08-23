using UnityEngine;

public class ApplicationLoader : MonoBehaviour
{
    [SerializeField] private BallView _ballView;
    [SerializeField] private LineView _lineView;
    [SerializeField] private BallRotationView _ballRotationView;
    [SerializeField] private BallColorView _ballColorView;
    [SerializeField] private BallCompressionView _ballCompressionView;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private CoinView[] _coinView;

    private BallMovement _ballMovement;
    private BallReflection _ballReflection;
    private BallPresenter _ballPresenter;
    private LinePresenter _linePresenter;
    private Wallet _wallet;
    private WalletPresenter _walletPresenter;

    private void Awake()
    {
        _wallet = new Wallet();
        _walletPresenter = new WalletPresenter(_wallet, _walletView, _coinView);
        _ballMovement = new BallMovement();
        _ballReflection = new BallReflection();
        _ballPresenter = new BallPresenter(_ballCompressionView, _ballColorView, _ballMovement, _ballReflection, _ballRotationView, _ballView);
        _linePresenter = new LinePresenter(_ballPresenter, _lineView);
    }

    private void OnEnable()
    {
        _ballPresenter.Enable();
        _linePresenter.Enable();
        _walletPresenter.Enable();
    }

    private void OnDisable()
    {
        _ballPresenter.Disable();
        _linePresenter.Disable();
        _walletPresenter.Disable();
    }
}
