public class WalletPresenter : IPresenter
{
    private Wallet _wallet;
    private WalletView _walletView;
    private CoinView[] _coinView;

    public WalletPresenter(Wallet wallet, WalletView walletView, CoinView[] coinView)
    {
        _wallet = wallet;
        _walletView = walletView;
        _coinView = coinView;
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
    }

    private void OnCoinAdded()
    {
        _wallet.AddCoin();
        _walletView.UpdateAmountText(_wallet.TotalCoins);
    }
}
