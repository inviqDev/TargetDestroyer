using UnityEngine;
using TMPro;


public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;

    private string _defaultText = "Count: ";
    private int _startValue = 0;
    private int _currentValue;



    private void Start()
    {
        _counterText.text = _defaultText + _startValue;
        _currentValue = _startValue;
    }



    public void IncreaseCounterValue(int increment)
    {
        _currentValue += increment;
        _counterText.text = _defaultText + _currentValue;
    }
}