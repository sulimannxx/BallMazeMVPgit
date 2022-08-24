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

    private IBallPositionManipulator _ballPresenter;
    private IPresenter _linePresenter;
    private WalletModel _wallet;
    private WalletPresenter _walletPresenter;
    private AbstractInput _inputManager;
    private LineVelocityModel _lineVelocityModel;

    private void Awake()
    {
        InitInputManager();
        _lineView.InitView(_inputManager);
        _wallet = new WalletModel();
        _walletPresenter = new WalletPresenter(_wallet, _walletView, _coinView);
        _wallet.Init();
        _lineVelocityModel = new LineVelocityModel();
        _ballPresenter = new BallPresenter(_ballCompressionView, _ballColorView, _lineVelocityModel, _ballRotationView, _ballView);
        _linePresenter = new LinePresenter(_ballPresenter, _lineView);
        _ballView.Init(_inputManager);
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

    private void InitInputManager()
    {

#if UNITY_ANDROID || UNITY_IOS
        _inputManager = new MobileInput();
#else
        _inputManager = new DesktopInput();
#endif
        _inputManager.Init();
    }
}
