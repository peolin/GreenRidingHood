using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPanelManager : MonoBehaviour
{
    [SerializeField] private string _demoText = "Mommy said to bring the bread to the grandmother! /n Better make it quick so the bread is still warm! ";
    [SerializeField] private GameObject _tmproBox;
    private TextMeshProUGUI _tmpro;
    private TypewriterEffect _typewriter;

    private void OnEnable()
    {
        TypewriterEffect.CompleteTextRevealed += HideTextPanel;
    }

    private void OnDisable()
    {
        TypewriterEffect.CompleteTextRevealed -= HideTextPanel;
    }
    private void Awake()
    {
        _tmpro = _tmproBox.GetComponent<TextMeshProUGUI>();
        _typewriter = _tmproBox.GetComponent<TypewriterEffect>();
    }

    private void Start()
    {
        _typewriter.StartTypewriter(_demoText);
    }

    private void HideTextPanel()
    {
        StartCoroutine(WaitForPlayerToRead());
    }

    private IEnumerator WaitForPlayerToRead()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
