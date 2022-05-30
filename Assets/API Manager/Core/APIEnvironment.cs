using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Com.ApiManager
{

    public enum BuildEnvironment
    {
        Testing,
        Staging,
        Production
    }

    public enum Method
    {
        GET,
        POST,
        DELETE
    }

    public class APIEnvironment 
    {
        private string baseUrl;

        public APIEnvironment(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public string BaseUrl()
        {
            return this.baseUrl;
        }


    }
}
