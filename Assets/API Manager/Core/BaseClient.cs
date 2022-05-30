using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ApiManager
{
    public abstract class BaseClient
    {
        protected APIConfiguration properties;
        protected APIEnvironment APIEnvironment;


        public BaseClient()
        {
            Init();
        }

        public void Init()
        {

            properties = Resources.Load<APIConfiguration>("APIConfig");

            switch (properties.m_CurrentEnvirnment)
            {
                case BuildEnvironment.Testing:
                    APIEnvironment = new APIEnvironment(properties.DevelopementBaseURL);
                    break;
                
                
                case BuildEnvironment.Staging:
                    APIEnvironment = new APIEnvironment(properties.StagingBaseURL);
                    break;

                case BuildEnvironment.Production:
                    APIEnvironment = new APIEnvironment(properties.ProductionBaseURL);
                    break;

            }
        }
    }
}
