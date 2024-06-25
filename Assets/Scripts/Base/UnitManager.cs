using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Unit _prefab;
    [SerializeField] private Transform _spawnPoint;

    private List<Unit> _units;
    private int _countFreeUnit;
    private int _total;

    private readonly int _unitInNewBase = 1;

    public int Total => _total;
    public int CountFreeUnit => _countFreeUnit;

    public event Action<int> CountUnitChanged;

    private void Awake()
    {
        _units = new List<Unit>();

        for (int i = 0; i < _unitInNewBase; i++)
            AddNew();

        _total = _units.Count;
        _countFreeUnit = _unitInNewBase;
        CountUnitChanged?.Invoke(_countFreeUnit);
    }

    public bool TryGet(out Unit unit) => TryGetFreeUnit(out unit);

    public void Activate(Unit unit)
    {
        if (unit.gameObject.activeSelf == false)
        {
            unit.gameObject.SetActive(true);
            unit.transform.position = _spawnPoint.position;
            CountUnitChanged?.Invoke(--_countFreeUnit);
        }
    }

    public bool TryDelete()
    {
        if (TryGetFreeUnit(out Unit unit) == false)
            return false;

        _units.Remove(unit);

        _total--;
        CountUnitChanged?.Invoke(--_countFreeUnit);

        return true;
    }

    public void Create()
    {
        AddNew();

        _total++;
        CountUnitChanged?.Invoke(++_countFreeUnit);
    }

    public void Relese(Unit unit)
    {
        unit.gameObject.SetActive(false);
        CountUnitChanged?.Invoke(++_countFreeUnit);
    }

    private bool TryGetFreeUnit(out Unit unit)
    {
        unit = null;

        if (_countFreeUnit > 0)
            unit = _units.Where(unit => unit.gameObject.activeSelf == false).FirstOrDefault();

        return unit != null;
    }

    private void AddNew()
    {
        Unit unit = Instantiate(_prefab);
        _units.Add(unit);
        unit.gameObject.SetActive(false);
    }

}
