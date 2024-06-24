using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ResourceFinder), typeof(Sender), typeof(UnitManager))]
public class Base : MonoBehaviour
{
    [SerializeField] private float _repeatRate;

    private ResourceFinder _finder;
    private Sender _sender;
    private UnitManager _unitManager;

    private void Awake()
    {
        _finder = GetComponent<ResourceFinder>();
        _sender = GetComponent<Sender>();
        _unitManager = GetComponent<UnitManager>();
    }

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            TrySendUnitToTarget();

            yield return wait;
        }
    }

    private void TrySendUnitToTarget()
    {
        if (_unitManager.TryGet(out Unit unit))
        {
            if (_finder.TryFind(out Resource target))
            {
                _unitManager.Activate(unit);
                _sender.SendUnit(target, unit);
            }
        }
    }
}
