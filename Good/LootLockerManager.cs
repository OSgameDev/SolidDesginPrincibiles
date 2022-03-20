///////////////////////////////////////////////////////////////////////
////////////////////////  CODE MADE BY OMAR   ////////////////////////
///////////////////////     OMARSTUDIOS™     ////////////////////////
////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootLockerManager : MonoBehaviour
{
    [Header("Login")]
    public InputField LoginEmail;
    public InputField LoginPassword;

    [Space]

    [Header("Singup")]
    public InputField SingupEmail;
    public InputField SingupPassword;

    [Space]

    [Header("States")]
    public Text CurrentStates;

    //Interfaces
    private ILootLockerLoginAndSingup _lootLockerWhiteLabelSL;
    private ILootLockerStatesUpdater _lootLockerStatesUpdater;


    private void Awake() // Called The first sec the app is launched
    {
        _lootLockerWhiteLabelSL = GetComponent<ILootLockerLoginAndSingup>();
        _lootLockerStatesUpdater = GetComponent<ILootLockerStatesUpdater>();
    }

    IEnumerator SignupDisplayUpdateDelay()
    {
        yield return new WaitForSeconds(1f);
        if (!_lootLockerWhiteLabelSL.IsSuccess())
        {
            _lootLockerStatesUpdater.UpdateStates("Failed Try Again.");
            _lootLockerStatesUpdater.DisplayStates(CurrentStates);
        }

        if (_lootLockerWhiteLabelSL.IsSuccess())
        {
            _lootLockerStatesUpdater.UpdateStates("Signed Up.");
            _lootLockerStatesUpdater.DisplayStates(CurrentStates);
        }
    }

    IEnumerator LoginDisplayDelay()
    {
        yield return new WaitForSeconds(1f);

        if (!_lootLockerWhiteLabelSL.IsSuccess())
        {
            _lootLockerStatesUpdater.UpdateStates("Failed Try Again.");
            _lootLockerStatesUpdater.DisplayStates(CurrentStates);
        }

        if (_lootLockerWhiteLabelSL.IsSuccess())
        {
            _lootLockerStatesUpdater.UpdateStates("Logged In.");
            _lootLockerStatesUpdater.DisplayStates(CurrentStates);
        }
    }

    public void PlayerSignUp()
    {
        //Signup
        _lootLockerWhiteLabelSL.Singup(SingupEmail.text, SingupPassword.text);

        //Update and Display
        StartCoroutine(SignupDisplayUpdateDelay());
    }
    public void Login()
    {
        //Login
        _lootLockerWhiteLabelSL.Login(LoginEmail.text, LoginPassword.text);

        //Update and Display
        StartCoroutine(LoginDisplayDelay());
    }
}
