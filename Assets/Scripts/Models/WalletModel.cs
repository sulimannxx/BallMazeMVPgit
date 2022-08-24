using System;

public class WalletModel
{
    private const int InitialCoins = 0;
    private int _totalCoins;
    private int TotalCoins
    {
        get
        {
            return _totalCoins;
        }
        set
        {
            if (_totalCoins != value)
            {
                _totalCoins = value;
                AmountCoinsChanged?.Invoke(_totalCoins);
            }
        }
    }

    public event Action<int> AmountCoinsChanged;

    public void Init()
    {
        TotalCoins = InitialCoins;
        AmountCoinsChanged?.Invoke(TotalCoins);
    }

    public void AddCoin()
    {
        TotalCoins++;
    }
}
