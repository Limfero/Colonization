using System;
using UnityEngine;

[RequireComponent (typeof(UnitManager), typeof(BaseCreator))]
public class StateManager : MonoBehaviour
{
    [SerializeField] private FinishZone _finish;

    private int _score;
    private int _costPerUnit = 3;
    private int _costPerBase = 5;

    private UnitManager _unitManger;
    private BaseCreator _creator;
    private State _state = State.CreatingUnits;

    public event Action<int> ScoreChanged;

    private void Awake()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _unitManger = GetComponent<UnitManager>();
        _creator = GetComponent<BaseCreator>();
    }

    private void OnEnable()
    {
        _finish.UnitBack += OnFinish;
        _creator.PositionDefined += SetStateToCreatingBase;
    }

    private void OnDisable()
    {
        _finish.UnitBack -= OnFinish;
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
            _unitManger.Create();
            _score -= _costPerUnit;
            ScoreChanged?.Invoke(_score);
        }
    }

    private void CreateBase()
    {
        if (_score >= _costPerBase && _unitManger.TryDelete())
        {
            _creator.SendBuilder();

            _score -= _costPerBase;
            ScoreChanged?.Invoke(_score);
            _state = State.CreatingUnits;
        }
    }

    private void OnFinish(Unit unit)
    {
        _score++;
        ScoreChanged?.Invoke(_score);
        _unitManger.Relese(unit);
        Create();
    }
}
