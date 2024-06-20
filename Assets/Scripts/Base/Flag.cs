using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Flag : MonoBehaviour
{
    [SerializeField] private MeshRenderer _base;
    [SerializeField] private float _offsetY = 0.5f;

    private bool _isActive = false;
    private bool _canLanded = false;

    private MeshRenderer _renderer;

    public event Action<Transform> Landed;
    public event Action Achieved;

    private void Awake()
    {
        gameObject.SetActive(false);

        _renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) == false)
            _canLanded = true;

        if (_isActive == true && _canLanded == true)
        {
            Move();
            Rotate();

            if (Input.GetMouseButton(0))
                Land();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitBuilder builder))
        {
            Achieved?.Invoke();
            Destroy(builder.gameObject);
        }         
    }

    public void Create()
    {
        gameObject.SetActive(true);
        _base.gameObject.SetActive(true);
        _isActive = true;
        _canLanded = false;
    }

    private void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.E))
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Q))
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
    }

    private void Land()
    {
        _isActive = false;
        Landed?.Invoke(transform);
        _base.gameObject.SetActive(false);
    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;

        transform.position = new Vector3(hit.point.x, _renderer.bounds.extents.y + _offsetY, hit.point.z);
    }
}
