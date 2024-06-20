using System;
using UnityEngine;

[RequireComponent (typeof(Sender))]
public class Creator : MonoBehaviour
{
    [SerializeField] private UnitBuilder _builder;
    [SerializeField] private Flag _flag;
    [SerializeField] private Base _basePrefab;

    private Transform _baseTransform;
    private int _baseCount = 1;

    private Sender _sender;

    public event Action PositionDefined;

    private void Awake()
    {
        Physics.queriesHitTriggers = true;
        _sender = GetComponent<Sender>();
    }

    private void OnEnable()
    {
        _flag.Landed += SetBasePosition;
        _flag.Achieved += CreateBase;
    }

    private void OnDisable()
    {
        _flag.Landed -= SetBasePosition;
        _flag.Achieved -= CreateBase;
    }

    private void OnMouseDown()
    {
        if (_baseCount > 0)
            _flag.Create();
    }

    public void SetBasePosition(Transform transform)
    {
        _baseTransform = transform;
        PositionDefined?.Invoke();
    }

    public void SendBuilder() => _sender.SendBuilder(_builder, _baseTransform.position);

    private void CreateBase()
    {
        _flag.gameObject.SetActive(false);
        Instantiate(_basePrefab, _baseTransform.position, _baseTransform.rotation);
        _baseCount--;
    }
}
