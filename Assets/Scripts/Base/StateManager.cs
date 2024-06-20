using System;
using UnityEngine;

[RequireComponent (typeof(Base), typeof(Creator))]
public class StateManager : MonoBehaviour
{
    [SerializeField] private Finish _finish;

    private int _score;
    private int _costPerUnit = 3;
    private int _costPerBase = 5;

    private Base _base;
    private Creator _creator;
    private State _state = State.CreatingUnits;

    public event Action<int> ScoreChanged;

    private void Awake()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _base = GetComponent<Base>();
        _creator = GetComponent<Creator>();
    }

    private void OnEnable()
    {
        _finish.Finished += OnFinish;
        _creator.PositionDefined += SetStateToCreatingBase;
    }

    private void OnDisable()
    {
        _finish.Finished -= OnFinish;
        _creator.PositionDefined -= SetStateToCreatingBase;
    }

    private void SetStateToCreatingBase() => _state = State.CreatingBase;

    private void Create()
    {
        switch (_state)
        {
            case State.CreatingUnits:
                CreateUnit();
                break;

            case State.CreatingBase:
                CreateBase();
                break;
        }
    }

    private void CreateUnit()
    {
        if (_score >= _costPerUnit)
        {
            _base.CreateUnit();
            _score -= _costPerUnit;
            ScoreChanged?.Invoke(_score);
        }
    }

    private void CreateBase()
    {
        if (_score >= _costPerBase && _base.CountUnit > 0)
        {
            _creator.SendBuilder();
            _base.DeleteUnit();

            _score -= _costPerBase;
            ScoreChanged?.Invoke(_score);
            _state = State.CreatingUnits;
        }
    }

    private void OnFinish()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
        _base.ReleseUnit();
        Create();
    }
}
