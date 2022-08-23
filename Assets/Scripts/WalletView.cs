using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void UpdateAmountText(int amount)
    {
        _text.text = amount.ToString();
    }
}
