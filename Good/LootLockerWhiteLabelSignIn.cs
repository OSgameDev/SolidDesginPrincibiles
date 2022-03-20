///////////////////////////////////////////////////////////////////////
////////////////////////  CODE MADE BY OMAR   ////////////////////////
///////////////////////     OMARSTUDIOS™     ////////////////////////
////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using System;
public class LootLockerWhiteLabelSignIn : MonoBehaviour,ILootLockerLoginAndSingup
{
    private bool _isLoginSuccess = false;
    public void Login(string email, string password)
    {
        var currentDeviceID = PlayerPrefs.GetString("CurrentDevice");

        LootLockerSDKManager.StartSession(currentDeviceID.ToString(), (sessionResponse) =>
        {
            if (!sessionResponse.success) return;

            LootLockerSDKManager.WhiteLabelLogin(email, password, (onLoginResponse) =>
            {
                if (!onLoginResponse.success)
                {
                    Debug.LogError($"Failed With Error {onLoginResponse.Error}");
                    _isLoginSuccess = false;
                    return;
                }

                else
                {
                    _isLoginSuccess = true;
                    email = String.Empty;
                    password = String.Empty;
                }
            });
        });
    }

    public void Singup(string email, string password)
    {
        var _currentDeviceID = Guid.NewGuid();
        PlayerPrefs.SetString("CurrentDevice", _currentDeviceID.ToString());

        LootLockerSDKManager.StartSession(_currentDeviceID.ToString(), (sessionResponse) =>
        {
            if (!sessionResponse.success)
            {
                return;
            }

            LootLockerSDKManager.WhiteLabelSignUp(email, password, (onSignupResponse) => {

                if (!onSignupResponse.success)
                {
                    Debug.LogError($"Failed With Error: {sessionResponse.Error}");
                    _isLoginSuccess = false;
                    return;
                }

                else
                {
                    _isLoginSuccess = true;
                    email = String.Empty;
                    password = String.Empty;
                }
            });
        });
    }
    public bool IsSuccess()
    {
        return _isLoginSuccess;
    }

}
