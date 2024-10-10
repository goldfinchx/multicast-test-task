using System;
using Quantum;
using TMPro;
using UniRx;
using UnityEngine;

public class UIStatVisualization : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI value;
    [SerializeField] private TextMeshProUGUI chance;

    public StatType Type { get; set; }
    private UIService UIService { get; set; }
    private IDisposable uiServiceSubscription;

    private void Awake() {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        uiServiceSubscription = UIService.UpdateSubject
            .Where(updateEvent => updateEvent.Stat.Type == Type)
            .Subscribe(evt => UpdateStat(evt.Stat));
    }
    
    private void OnDisable() {
        uiServiceSubscription?.Dispose();
    }

    private void UpdateStat(Stat stat) {
        Debug.Log($"Pre-Update stat: name={GetStatTitle(stat.Type)} level={stat.Level} value={stat.Value} chance={stat.Chance}");
        title.text = GetStatTitle(stat.Type);
        level.text = stat.Level.ToString();
        value.text = stat.Value.ToString();
        chance.text = stat.Chance + "%";
        Debug.Log($"Post-Update stat: name={GetStatTitle(stat.Type)} level={stat.Level} value={stat.Value} chance={stat.Chance}");
    }

    private string GetStatTitle(StatType statType) {
        return statType switch {
            StatType.AttackDamage => "Damage",
            StatType.AttackRange => "Range",
            StatType.MovementSpeed => "Speed",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void Setup(UIService uiService, StatType statType, VisualSettings settings) {
        UIService = uiService;
        Type = statType;
        
        RectTransform rectTransform = GetComponent<RectTransform>();
        switch (settings.ScreenPosition) {
            case ScreenPosition.TopLeft:
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(0, 1);
                break;
            case ScreenPosition.TopRight:
                rectTransform.anchorMin = new Vector2(1, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                break;
            case ScreenPosition.BottomLeft:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 0);
                break;
            case ScreenPosition.BottomRight:
                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        transform.position = settings.Position;
        gameObject.SetActive(true);
    }
}
