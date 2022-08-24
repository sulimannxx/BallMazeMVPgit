public class WalletPresenter : IPresenter
{
    private WalletModel _wallet;
    private WalletView _walletView;
    private CoinView[] _coinView;

    public WalletPresenter(WalletModel wallet, WalletView walletView, CoinView[] coinView)
    {
        _wallet = wallet;
        _walletView = walletView;
        _coinView = coinView;
        _wallet.AmountCoinsChanged += _walletView.UpdateAmountText;
    }

    public void Enable()
    {
        foreach (var coin in _coinView)
        {
            coin.CoinAdded += OnCoinAdded;
        }
    }

    public void Disable()
    {
        foreach (var coin in _coinView)
        {
            coin.CoinAdded -= OnCoinAdded;
        }
        _wallet.AmountCoinsChanged -= _walletView.UpdateAmountText;
    }

    private void OnCoinAdded()
    {
        _wallet.AddCoin();
    }
}