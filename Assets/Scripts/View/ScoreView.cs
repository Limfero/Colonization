using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private StateManager _baseController;

    private void OnEnable()
    {
        _baseController.ScoreChanged += View;
    }

    private void OnDisable()
    {
        _baseController.ScoreChanged -= View;
    }

    private void View(int score) => _text.text = score.ToString();
}
