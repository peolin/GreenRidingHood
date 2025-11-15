using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Collectibles;

public class TextPanelManager : MonoBehaviour
{
    private string _demoText = "Mommy said to bring the bread to the grandmother! \n Better make it quick so the bread is still warm! ";
    [SerializeField] private GameObject _tmproBox;
    [SerializeField] private GameObject _bgImage;
    private TextMeshProUGUI _tmpro;
    private TypewriterEffect _typewriter;

    public static event Action OnCollectibleTextHidden;

    private void OnEnable()
    {
        TypewriterEffect.CompleteTextRevealed += HideTextPanel;
        CollectiblesManager.OnObjectCollection += ShowCollectibleText;
    }

    private void OnDisable()
    {
        TypewriterEffect.CompleteTextRevealed -= HideTextPanel;
        CollectiblesManager.OnObjectCollection -= ShowCollectibleText;
    }
    private void Awake()
    {
        _tmpro = _tmproBox.GetComponent<TextMeshProUGUI>();
        _typewriter = _tmproBox.GetComponent<TypewriterEffect>();
    }

    private void Start()
    {
        ShowEntryText();
    }

    private void ShowEntryText()
    {
        _typewriter.StartTypewriter(_demoText);
    }

    private void ShowCollectibleText(string collectibleText)
    {
        _tmproBox.SetActive(true);
        _bgImage.SetActive(true);

        _typewriter.StartTypewriter(collectibleText);
    }

    private void HideTextPanel()
    {
        StartCoroutine(WaitForPlayerToRead());
    }

    private IEnumerator WaitForPlayerToRead()
    {
        yield return new WaitForSeconds(3);

        OnCollectibleTextHidden?.Invoke();

        _tmproBox.SetActive(false);
        _bgImage.SetActive(false);
    }
}
