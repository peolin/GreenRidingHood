using System;

namespace DataAndLoaders
{
    [Serializable]
    public class GRH_ScriptWrapper
    {
        public ScriptData script;
    }

    [Serializable]
    public class ScriptData
    {
        public string girl_start; //together
        public string[] girl_flowerpicking; //step-by-step
        public string[] girl_wolf_encounter; //step-by-step
        public string[] mom_start; //together
        public string[] mom_path; //step-by-step
        public string[] mom_bridge; //together
        public string[] mom_trailing; //step-by-step
    }
}