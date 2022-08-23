public class Wallet
{
    public int TotalCoins { get; private set; }

    public void AddCoin()
    {
        TotalCoins++;
    }
}
