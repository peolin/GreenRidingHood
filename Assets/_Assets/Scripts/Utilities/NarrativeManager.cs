using System;
using UnityEngine;
using DataAndLoaders;
using Collectibles;

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
    [Header("Module references")]
    [SerializeField] private GRH_ScriptLoader _scriptLoader;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CollectiblesManager _collectiblesManager;

    private ScriptData _narrative;
    private string[] _currentNarrativeLines;
    private int _currentLineIndex = 0;
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

    private int _currentNarrativePointIndex;
    
    public event Action OnNarrativeSequenceComplete;
    public event Action OnNarrativeFinished;

    private void OnEnable()
    {
        _scriptLoader.OnScriptLoaded += InitializeNarrativeSequence;
        _collectiblesManager.OnObjectCollection += OnPlayerProgress;
    }

    private void OnDisable()
    {
        _scriptLoader.OnScriptLoaded -= InitializeNarrativeSequence;
        _collectiblesManager.OnObjectCollection -= OnPlayerProgress;
    }

    private void InitializeNarrativeSequence(ScriptData script) // we start narrative as the script is loaded
    {
        _narrative = script;
        _currentNarrativePointIndex = 0;
        
        SetCurrentNarrative(_storySequence[_currentNarrativePointIndex]); // start on Girl monologue
        GoToNextLine();
    }

    public void SetCurrentNarrative(NarrativePoint currentNarrative) // can be used to start narrative in game manager
    {
        _currentLineIndex = 0; // only when we change narrative is lines index reset
        
        _currentNarrativeLines = GetLinesByPoint(currentNarrative);
        _isCurrentNarrativeInBatch = IsInBatch(currentNarrative);
    }
    
    private void GoToNextNarrativePoint()
    {
        if (_currentNarrativePointIndex + 1 < _storySequence.Length)
        {
            _currentNarrativePointIndex++;
            SetCurrentNarrative(_storySequence[_currentNarrativePointIndex]); // just set next one as in sequence
            GoToNextLine();
        }
        else
        {
            OnNarrativeFinished?.Invoke();
        }
    }
    
    private void GoToNextLine()
    {
        _uiManager.RequestNextNarrativeLine -= HandleLineFinished;

        if (_currentNarrativeLines != null)
        {
            _uiManager.SetLine(_currentNarrativeLines[_currentLineIndex]);
            _currentLineIndex++;
        }

        _uiManager.RequestNextNarrativeLine += HandleLineFinished;
    }
    
    private void HandleLineFinished()
    {
        _uiManager.RequestNextNarrativeLine -= HandleLineFinished;

        if (_isCurrentNarrativeInBatch && _currentLineIndex < _currentNarrativeLines.Length)
        {
            // If it's a batch and we have lines left, just keep going!
            GoToNextLine();
        }
        else
        {
            // If it's Step-by-Step OR the Batch is finished: Close the panel and wait.
            FinishNarrativePoint();
        }
    }

    private void FinishNarrativePoint()
    {
        _uiManager.RequestNextNarrativeLine -= HandleLineFinished;
        OnNarrativeSequenceComplete?.Invoke();
    }
    
    private void OnPlayerProgress()
    {
        // If there are lines left in the current point, show the next one
        if (_currentLineIndex < _currentNarrativeLines.Length)
        {
            GoToNextLine();
        }
        else
        {
            // Only if the current point is totally exhausted do we move the story forward
            GoToNextNarrativePoint();
        }
    }

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
