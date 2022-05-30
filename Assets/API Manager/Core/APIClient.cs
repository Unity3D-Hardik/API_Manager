using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Com.ApiManager
{
    public class APIClient : BaseClient
    {
        public delegate void action(bool success, object data, long statusCode);

        private static bool IsValid(long code) => (int)code / 100 == 2;

        private bool HasAccessToken => !string.IsNullOrEmpty(accessToken);
        private static string accessToken;


        #region API_CALL
        /// Make a raw API request to Server.
        /// </summary>
        /// <param name="method">HTTP method Ex. GET OR POST </param>
        /// <param name="endPoint">i.e. login , register </param>
        /// <param name="jsonData">Data to post/patch.</param>
        /// <param name="AccessToken">Data to post/patch.</param>
        /// <param name="callback">Returns server response in JSON format (status code, response string).</param>
        public void Request(string method, string endPoint, string jsonData, string accessToken, UnityAction<long, string> callback)
        {
            UnityWebRequest request = new UnityWebRequest(APIEnvironment.BaseUrl() + endPoint, method);

            if (!string.IsNullOrEmpty(accessToken))
                request.SetRequestHeader("Authorization", accessToken);

            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(jsonData))
            {
                Debug.Log("*******************Set Data**************************");
                Debug.Log(jsonData);
                request.uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(jsonData)) { contentType = "application/json" };
            }

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SendWebRequest().completed += _ =>
            {
                callback(request.responseCode, request.downloadHandler.text);
            };
        }
        #endregion

        #region WebAPI call
        public static void CallWebAPI(string method, string endPoint, string jsonData, string accessToken, action onComplete = null)
        {
            new APIClient().CreateAPIResponse(method, endPoint, jsonData, accessToken, onComplete);
        }

        private void CreateAPIResponse(string method, string endPoint, string jsonData, string accessToken, action onComplete)
        {

            Request(method, endPoint, jsonData, accessToken, (status, res) =>
            {
                Debug.Log("Full Response : " + res);

                // Debug.Log("success :" + _node["success"] + " , code :" + _node["code"]);

                /*   "code" : 200 ":  "Sucess "
                 *   "code": 401 : "Runtime message " when data not available or missmatch data found
                 *   "code": 403 : "message": "unauthenticated"
                 *   "code" : 500 : Internal server Error 
                 */

                if (!IsValid(status))
                {
                    onComplete?.Invoke(false, res, status);
                }
                else
                {
                    onComplete?.Invoke(true, res, status);
                }
            });
        }
        #endregion

    }
}
