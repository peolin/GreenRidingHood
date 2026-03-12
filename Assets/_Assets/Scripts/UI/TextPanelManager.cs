using System;
using System.Collections;
using UnityEngine;
using Utilities;

/// <summary>
/// Handles text display and its typewriter effect
/// </summary>
public class TextPanelManager : MonoBehaviour
{
    private string _demoText = "Mommy said to bring the bread to the grandmother! \n Better make it quick so the bread is still warm! ";
    
    [Header("Component References")]
    [SerializeField] private GameObject _tmproBox;
    [SerializeField] private TypewriterEffect _typewriter;
    [SerializeField] private GameObject _bgImage;

    [Header("Text Panel Settings")] 
    [SerializeField][Min(3f)] private float _readingDelay = 5f;
    
    public bool IsDisplaying { get; set; }

    public event Action RequestNextLine;
    public event Action OnDisplayTextHidden;

    private void OnEnable()
    {
        _typewriter.CompleteTextRevealed += HandleLineEnd;
    }

    private void OnDisable()
    {
        _typewriter.CompleteTextRevealed -= HandleLineEnd;
    }
    
    public void ShowPanel()
    {
        StopAllCoroutines();
        
        _bgImage.SetActive(true);
        _tmproBox.SetActive(true);
    }

    public void ShowText(string newLine)
    {
        StopAllCoroutines();
        
        _tmproBox.SetActive(true);
        _bgImage.SetActive(true);

        if (_typewriter != null)
        {
            _typewriter.StartTypewriter(newLine);
        }
        else Debug.LogError($"TextPanelManager: typewriter effect not initialized!");
    }

    public void HidePanel()
    {
        StopAllCoroutines();
        
        _bgImage.SetActive(false);
        _tmproBox.SetActive(false);
    }
    
    public void ClosePanel()
    {
        StartCoroutine(WaitForPlayerToRead());
    }
    
    private void HandleLineEnd()
    {
        RequestNextLine?.Invoke();
    }
    
    private IEnumerator WaitForPlayerToRead()
    {
        yield return new WaitForSeconds(_readingDelay);

        OnDisplayTextHidden?.Invoke();
        HidePanel();
    }
}
