using System;
using UnityEngine;

namespace DataAndLoaders
{
    public class GRH_ScriptLoader : MonoBehaviour
    {
        private GRH_ScriptWrapper _scriptWrapper;
        private string _fileName = "GRH_Script";
        public event Action<ScriptData> OnScriptLoaded;

        public ScriptData Script
        {
            get => _scriptWrapper.script;
        }

        private void Awake()
        {
            LoadData();
        }
        
        private void LoadData()
        {
            //string resourcePath = _fileName.Replace(".json", "");
            
            TextAsset jsonFile = Resources.Load<TextAsset>(_fileName);

            if (jsonFile != null)
            {
                _scriptWrapper = JsonUtility.FromJson<GRH_ScriptWrapper>(jsonFile.text);
        
                Debug.Log("Narrative script loaded successfully.");
                OnScriptLoaded?.Invoke(Script);
            }
            else
            {
                Debug.LogError($"Narrative file not found in Resources: {_fileName}");
            }
        }
    }
}