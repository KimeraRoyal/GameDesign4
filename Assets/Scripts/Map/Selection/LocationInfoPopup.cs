using Map;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LocationInfoPopup : MonoBehaviour
{
    CanvasGroup _group;

    LocationSelector _selector;
    
    [SerializeField] Vector3 _offset;

    bool _visible;
    
    void Awake()
    {
        _group = GetComponent<CanvasGroup>();
        
        _selector = FindAnyObjectByType<LocationSelector>();
        _selector.OnSelected.AddListener(LocationSelected);
        _selector.OnDeselected.AddListener(LocationDeselected);
    }

    void Start()
    {
        _group.alpha = 0.0f;
        _group.blocksRaycasts = false;
    }

    void LocationSelected(LocationNode location)
    {
        transform.position = location.transform.position + _offset;
        FadeGroup(true);
    }

    void LocationDeselected()
    {
        FadeGroup(false);
    }

    void FadeGroup(bool show)
    {
        if(_visible == show) { return; }

        _group.alpha = show ? 1.0f : 0.0f;
        _group.blocksRaycasts = show;

        _visible = show;
    }
}
