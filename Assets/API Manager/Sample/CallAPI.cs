using Com.ApiManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



//Dummy data 

[Serializable]
public class User {

    public string email;
    public string password;
}

public class CallAPI : MonoBehaviour
{

    // Load this once for all api
    public APIConfiguration properties;

    
    //Dummy Data 
    [SerializeField]
    User user = new User();

    // Start is called before the first frame update
    void Start()
    {

        NewLogin(user);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Checking Intenet Connection before Every API Call & Handle Internet Connection Error 
    #region CheckConnection
    IEnumerator CheckInternetConnection(Action<bool> action)
    {
      //  RayCastBlock();

        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("net error: " + request.error);
           // errorWindow.SetErrorMessage("Internet Connection Failed !", "Please, Check you internet connection and try again.", "TRY AGAIN", ErrorWindow.ResponseData.InternetIssue, true);
           //  RaycastUnblock();
            action(false);
        }
        else
        {
            action(true);
        }
    }
    #endregion

    public void NewLogin(User user)
    {

        Debug.Log("User data: " + user.email + " " + user.password);
        StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                Debug.Log("Call API From Here");
                APIClient.CallWebAPI(Method.POST.ToString(), properties.LoginAPI, JsonUtility.ToJson(user), string.Empty, LoginUser);
            }
        }));
    }


    private void LoginUser(bool success, object data, long statusCode)
    {
        if (success)
        {
            Debug.Log("Login Done ");
        }
        else
        {
            Debug.Log("Login Failed");
            Debug.Log("Login Failed");
        }
    }
}
