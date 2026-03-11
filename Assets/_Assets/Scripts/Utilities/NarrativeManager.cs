using System;
using System.Collections.Generic;
using UnityEngine;
using DataAndLoaders;

/// <summary>
/// Narrative structure & pacing points
/// </summary>
public enum NarrativePoint
{
    GirlStart,
    GirlFlowers,
    GirlWolf,
    MomStart,
    MomPath,
    MomBridge,
    MomTrail
}

/// <summary>
/// Handles narrative parsing, creating and sharing currently relevant lines to UI
/// </summary>
public class NarrativeManager : MonoBehaviour
{
    [SerializeField] private GRH_ScriptLoader _scriptLoader;
    [SerializeField] private UIManager _uiManager;

    private ScriptData _narrative;
    private string[] _currentNarrativeLines;
    private int _lineInNarrativeIndex = 0;
    private bool _isCurrentNarrativeInBatch;

    private NarrativePoint[] _storySequence =
    {
        NarrativePoint.GirlStart,
        NarrativePoint.GirlFlowers,
        NarrativePoint.GirlWolf,
        NarrativePoint.MomStart,
        NarrativePoint.MomPath,
        NarrativePoint.MomBridge,
        NarrativePoint.MomTrail,
    };
    
    public event Action OnNarrativeSequenceComplete;

    private void OnEnable() => _scriptLoader.OnScriptLoaded += InitializeNarrativeSequence;
    private void OnDisable() => _scriptLoader.OnScriptLoaded -= InitializeNarrativeSequence;
    
    private void InitializeNarrativeSequence(ScriptData script)
    {
        Debug.Log("Got the script");
        _narrative = script;
        SetCurrentNarrative(NarrativePoint.GirlStart);
    }

    public void SetCurrentNarrative(NarrativePoint currentNarrative) // will be used also on player entering trigger points
    {
        _lineInNarrativeIndex = 0; // only when we change narrative is lines index reset
        
        _currentNarrativeLines = GetLinesByPoint(currentNarrative);
        
        _isCurrentNarrativeInBatch = IsInBatch(currentNarrative);
        
        ShowLine();
    }
    
    private void ShowLine()
    {
        _uiManager.SetNarrativePoint(_currentNarrativeLines[_lineInNarrativeIndex]);
        _lineInNarrativeIndex++;
        
        _uiManager.RequestNextNarrativeLine -= HandleLineFinished;
        _uiManager.RequestNextNarrativeLine += HandleLineFinished;
    }

    private void HandleLineFinished()
    {
        if (_isCurrentNarrativeInBatch && _lineInNarrativeIndex < _currentNarrativeLines.Length)
        {
            ShowLine();
        }
        else
        {
            _uiManager.RequestNextNarrativeLine -= HandleLineFinished;
            OnNarrativeSequenceComplete?.Invoke();
        }
    }

    /*private void ShowLines()
    {
        //show line in ui manager
        //increase line in narrative index (for steps & batch)
        // subscribe to request next line
        // - if step by step -> will be showing on every trigger outside from player, closing on every request to show line
        // - if batch -> repeat showing, closing only on lines end
        
        //_uiManager.SetNarrativePoint(_currentNarrativeLines[_lineInNarrativeIndex]);
        //_lineInNarrativeIndex++;
        /*if (!_isCurrentNarrativeInBatch)
        {
            OnNarrativeSequenceComplete?.Invoke();

            _uiManager.RequestNextNarrativeLine -= ShowLine;
        }*
        
        _uiManager.RequestNextNarrativeLine -= ShowLine;
        _uiManager.RequestNextNarrativeLine += ShowLine;

        if (_isCurrentNarrativeInBatch && _lineInNarrativeIndex < _currentNarrativeLines.Length)
        {
            // keep showing ....
            _uiManager.SetNarrativePoint(_currentNarrativeLines[_lineInNarrativeIndex]);
            _lineInNarrativeIndex++;
        }
        else // ... until hit lines end (index reset only on new narrative point set)
        {
            OnNarrativeSequenceComplete?.Invoke();

            _uiManager.RequestNextNarrativeLine -= ShowLine;
        }
    }*/

    private bool IsInBatch(NarrativePoint point) =>
        point == NarrativePoint.GirlStart || point == NarrativePoint.MomStart || point == NarrativePoint.MomBridge;
    
    private string[] GetLinesByPoint(NarrativePoint point)
    {
        return point switch
            {
                NarrativePoint.GirlStart => new string[] { _narrative.girl_start },
                NarrativePoint.GirlFlowers => _narrative.girl_flowerpicking,
                NarrativePoint.GirlWolf => _narrative.girl_wolf_encounter,
                NarrativePoint.MomStart => _narrative.mom_start,
                NarrativePoint.MomPath => _narrative.mom_path,
                NarrativePoint.MomBridge => _narrative.mom_bridge,
                NarrativePoint.MomTrail => _narrative.mom_trailing,
                _ => new string[] { ":P" }
            };
    }
}
