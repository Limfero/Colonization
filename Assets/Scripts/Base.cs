using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private Spawner<Unit> _spawner;
    [SerializeField] private float _repeatRate;
    [SerializeField] private float _scanningRadius;
    [SerializeField] private Finish _finish;
    [SerializeField] private int _maxUnit;

    private Queue<PickingObject> _targets = new();
    private int _countUnit = 3;

    public event Action<int> CountUnitChanged;

    public int MaxUnit => _maxUnit;

    private void OnEnable()
    {
        _finish.Finished += ReleseUnit;
    }

    private void OnDisable()
    {
        _finish.Finished -= ReleseUnit;
    }

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        if (_countUnit > 0 && _targets.Count != 0)
        {
            Unit unit = _spawner.GetObject(_spawnPoint.position);
            PickingObject target = _targets.Dequeue();

            target.Hold();
            unit.Init(target.transform.position, _finishPoint.position);

            _countUnit--;
            CountUnitChanged?.Invoke(_countUnit);
        }
    }

    private IEnumerator Countdown()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            FindTargets();

            yield return wait;
        }
    }

    private void FindTargets()
    {
        List<PickingObject> targets = Physics.OverlapSphere(transform.position, _scanningRadius)
            .Where(hit => hit.TryGetComponent(out PickingObject pickingObject))
            .Select(hit => hit.GetComponent<PickingObject>())
            .Where(hit => hit.IsBusy == false)
            .ToList();

        _targets.Clear();

        foreach (var target in targets)
            _targets.Enqueue(target);
    }

    private void ReleseUnit() 
    {
        _countUnit++;
        CountUnitChanged?.Invoke(_countUnit);
    }
}
