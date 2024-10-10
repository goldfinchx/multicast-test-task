using System;
using Quantum;
using Quantum.Gameplay.UICommand;
using Quantum.Gameplay.UpgradeCommand;
using UniRx;
using UnityEngine;


public enum ScreenPosition {
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public struct VisualSettings {
    public ScreenPosition ScreenPosition;
    public Vector2 Position;
}

public class UIService : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] private UIStatVisualization statVisualPrefab;
    
    [Header("Layout Settings")]
    [SerializeField] private bool reverseOrder;
    [SerializeField] private int xAlign;
    [SerializeField] private int yAlign;
    
    [Header("Starting Position Settings")]
    [SerializeField] private ScreenPosition screenPosition;
    [SerializeField] private int xScreenOffset;
    [SerializeField] private int yScreenOffset;
    
    [Header("Other")]
    [SerializeField] private bool editMode;
    
    public Subject<EventStatUpdate> UpdateSubject { get; private set; }

    private void Awake() {
        UpdateSubject = new Subject<EventStatUpdate>();
        QuantumEvent.Subscribe<EventStatUpdate>(listener: this, handler: HandleUpdateEvent); 
        Invoke(nameof(CreateStatsVisuals), 0.1f);
    }

    private void Update() {
        if (!editMode) {
            return;
        }

        Vector2 startingPosition = GetScreenPosition(screenPosition) + new Vector2(xScreenOffset, yScreenOffset);
        UIStatVisualization[] childrenVisuals = GetComponentsInChildren<UIStatVisualization>();
        int index = 0;
        foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
            UIStatVisualization statVisual = Array.Find(childrenVisuals, visual => visual.Type == statType);
            if (statVisual == null) {
                continue;
            }

            Vector2 position = startingPosition + new Vector2(index * xAlign, index * yAlign);
            VisualSettings visualSettings = new() {
                ScreenPosition = screenPosition,
                Position = position
            };
            
            
            statVisual.Setup(this, statType, visualSettings);
            index = reverseOrder ? index - 1 : index + 1;
        }
        
    }

    private void CreateStatsVisuals() {
        Canvas canvas = GetComponent<Canvas>();
        Vector2 startingPosition = GetScreenPosition(screenPosition) + new Vector2(xScreenOffset, yScreenOffset);
        
        int index = 0;
        foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
            UIStatVisualization statVisual = Instantiate(statVisualPrefab, canvas.gameObject.transform);
            Vector2 position = startingPosition + new Vector2(index * xAlign, index * yAlign);
            
            VisualSettings visualSettings = new() {
                ScreenPosition = screenPosition,
                Position = position
            };
            statVisual.Setup(this, statType, visualSettings);
            index = reverseOrder ? index - 1 : index + 1;
        }
        
        QuantumRunner.Default.Game.SendCommand(new UISetupCommand());
    }

    private void HandleUpdateEvent(EventStatUpdate updateEvent) {
        UpdateSubject.OnNext(updateEvent);
    }

    public void SendUpgradeCommand() {
        QuantumRunner.Default.Game.SendCommand(new UpgradeCommand());
    }
    
    private Vector2 GetScreenPosition(ScreenPosition position) {
        return position switch {
            ScreenPosition.TopLeft => new Vector2(0, Screen.height),
            ScreenPosition.TopRight => new Vector2(Screen.width, Screen.height),
            ScreenPosition.BottomLeft => new Vector2(0, 0),
            ScreenPosition.BottomRight => new Vector2(Screen.width, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
}