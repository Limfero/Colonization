using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Finish _finish;

    private int _score = 0;

    private void OnEnable()
    {
        _finish.Finished += Add;
    }

    private void OnDisable()
    {
        _finish.Finished -= Add;
    }

    private void Add() => _text.text = (++_score).ToString();
}
