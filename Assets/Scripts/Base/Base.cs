using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Finder), typeof(Sender))]
public class Base : MonoBehaviour
{
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _maxUnit = 1;

    private int _countUnit;
    
    private Finder _finder;
    private Sender _sender;

    public event Action<int> CountUnitChanged;

    public int MaxUnit => _maxUnit;

    public int CountUnit => _countUnit;

    private void Awake()
    {
        _finder = GetComponent<Finder>();
        _sender = GetComponent<Sender>();

        _countUnit = _maxUnit;
    }

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    public void ReleseUnit() => CountUnitChanged?.Invoke(++_countUnit);

    public void DeleteUnit()
    {
        _maxUnit--;
        _countUnit--;
        CountUnitChanged?.Invoke(_countUnit);
    }

    public void CreateUnit()
    {
        _maxUnit++;
        _countUnit++;
        CountUnitChanged?.Invoke(_countUnit);
    }

    private IEnumerator Countdown()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            if (_finder.TryFind(out Queue<PickingObject> targets) && _countUnit > 0)
            {
                _sender.SendUnit(targets);
                CountUnitChanged?.Invoke(--_countUnit);
            }

            yield return wait;
        }
    }
}
