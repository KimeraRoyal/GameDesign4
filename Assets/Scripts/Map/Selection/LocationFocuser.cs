using Map;
using UnityEngine;

public class LocationFocuser : MonoBehaviour
{
    LocationSelector _selector;

    [SerializeField] Vector3 _offset;
    [SerializeField] float _movementDamping = 0.1f;

    Vector3 _targetPosition;
    Vector3 _movementVelocity;

    bool _moving;
    
    void Awake()
    {
        _selector = FindAnyObjectByType<LocationSelector>();
        _selector.OnSelected.AddListener(LocationSelected);
        _selector.OnDeselected.AddListener(LocationDeselected);
    }

    void Update()
    {
        if(!_moving) { return; }

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _movementVelocity, _movementDamping);
    }

    void LocationSelected(LocationNode location)
    {
        _targetPosition = location.transform.position + _offset;
        _moving = true;
    }

    void LocationDeselected()
    {
        _moving = false;
        _movementVelocity = Vector3.zero;
    }
}
