using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextPanelManager _textPanelManager;
    [SerializeField] private NarrativeManager _narrativeManager;
    
    public event Action RequestNextNarrativeLine;
    public event Action OnUIInteractionEnded;

    private void OnEnable()
    {
        _textPanelManager.RequestNextLine += HandleRequestNextLine;
        _textPanelManager.OnDisplayTextHidden += HandlePanelClosed;

        _narrativeManager.OnNarrativeSequenceComplete += CloseTextPanel;
    }

    private void OnDisable()
    {
        _textPanelManager.RequestNextLine -= HandleRequestNextLine;
        _textPanelManager.OnDisplayTextHidden -= HandlePanelClosed;

        if (_narrativeManager != null)
        {
            _narrativeManager.OnNarrativeSequenceComplete -= CloseTextPanel;
        }
    }

    private void HandleRequestNextLine()
    {
        RequestNextNarrativeLine?.Invoke();
    }

    private void HandlePanelClosed()
    {
        OnUIInteractionEnded?.Invoke();
    }
    
    public void SetLine(string text)
    { 
        _textPanelManager.ShowPanel();
        _textPanelManager.ShowText(text);
    }

    public void HidePanel()
    {
        _textPanelManager.HidePanel();
    }

    public void CloseTextPanel()
    {
        _textPanelManager.ClosePanel();
    }
}