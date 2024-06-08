using TMPro;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.CountUnitChanged += Change;
    }

    private void OnDisable()
    {
        _base.CountUnitChanged -= Change;
    }

    private void Change(int count) => _text.text = $"{count}/{_base.MaxUnit}";
}
