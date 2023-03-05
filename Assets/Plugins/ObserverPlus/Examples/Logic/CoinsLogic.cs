using ObserverPlus.Templates;
using UnityEngine;
using UnityEngine.UI;

public class CoinsLogic : MonoBehaviour
{
    [field: SerializeField] public Button AddCoinsBtn { get; set; }
    [field: SerializeField] public CoinsChangedEvent OnCoinsChangedEvent { get; set; }

    private int _coinsValue = 0;
    public int CoinsValue
    {
        get { return _coinsValue; }
        set
        {
            _coinsValue = value;
            OnCoinsChangedEvent?.Raise(_coinsValue);
        }
    }

    public void AddCoins()
    {
        CoinsValue += 10;
    }
}
