using TMPro;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private UnitManager _unitManager;

    private void OnEnable()
    {
        _unitManager.CountUnitChanged += Change;
    }

    private void OnDisable()
    {
        _unitManager.CountUnitChanged -= Change;
    }

    private void Change(int count) => _text.text = $"{count}/{_unitManager.Total}";
}
