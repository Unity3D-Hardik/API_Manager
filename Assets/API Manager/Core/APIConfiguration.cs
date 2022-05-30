using UnityEngine;

namespace Com.ApiManager {

    [CreateAssetMenu(fileName = "APIConfig", menuName = "APISDK/API Settings", order = 1)]
    public class APIConfiguration : ScriptableObject
    {

        #region API Settings
        public BuildEnvironment m_CurrentEnvirnment;
        #endregion

        #region API Envirnment
        public string DevelopementBaseURL = "";
        public string StagingBaseURL = "";
        public string ProductionBaseURL = "";
        #endregion


        #region API List
        [Space(15)]
        [Header("API List")]
        [Space(15)]

        [Tooltip("1) Login Method")]
        public string LoginAPI = "login";

        [Tooltip("2) Register Method")]
        public string Register = "register";
        #endregion
    }
}