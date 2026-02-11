using Map;
using UnityEngine;
using UnityEngine.UI;

public class LocationButton : MonoBehaviour
{
    Button _button;
    CanvasGroup _group;

    [SerializeField] Image _icon;

    LocationNode _node;
    LocationAction _action;

    void Awake()
    {
        _button = GetComponent<Button>();
        _group = GetComponent<CanvasGroup>();
        
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
        => _action?.ApplyAction(_node);

    public void Show(LocationNode node, Sprite icon, LocationAction action)
    {
        _node = node;
        _action = action;
        
        _icon.sprite = icon;
        
        _group.alpha = 1.0f;
        _group.blocksRaycasts = true;
    }

    public void Hide()
    {
        _node = null;
        _action = null;
        
        _group.alpha = 0.0f;
        _group.blocksRaycasts = false;
    }
}

public interface LocationAction
{
    void ApplyAction(LocationNode node);
}

public class LocationBuildAction : LocationAction
{
    readonly int _buildingID;

    public LocationBuildAction(int buildingID)
        => _buildingID = buildingID;

    public void ApplyAction(LocationNode node)
        => node.SpawnBuilding(node.Type.SupportedBuildings[_buildingID]);
}

public class LocationDemolishAction : LocationAction
{
    public void ApplyAction(LocationNode node)
        => node.DemolishBuilding();
}