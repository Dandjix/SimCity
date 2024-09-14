using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(MMS_Main))]
[RequireComponent(typeof(MMS_Saves))]
[RequireComponent(typeof(MMS_Options))]
[RequireComponent(typeof(MMS_New))]
public class MainMenuStateMachine : MonoBehaviour
{
    public static MainMenuStateMachine Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        Main = GetComponent<MMS_Main>();
        Saves = GetComponent<MMS_Saves>();
        Options = GetComponent<MMS_Options>();
        New = GetComponent<MMS_New>();
        Set(Main);
    }

    private MainMenuState current;

    public static MMS_Main Main { get; private set; }
    public static MMS_Saves Saves { get; private set; }
    public static MMS_Options Options { get; private set; }
    public static MMS_New New { get; private set; }

    public static void Set(MainMenuState state)
    {
        if (Instance.current != null)
        {
            Instance.current.Exit(state);
            Instance.current.enabled = false;
            Debug.Log("state disabled : " + state);
        }


        MainMenuState old = Instance.current;
        Instance.current = state;

        state.Enter(old);
        state.enabled = true;
        Debug.Log("state enabled : " + state);
    }

    public static void Set(MMStateName name)
    {
        switch (name)
        {
            case MMStateName.Main:
                Set(Main);
                break;
            case MMStateName.Saves:
                Set(Saves);
                break;
            case MMStateName.Options:
                Set(Options);
                break;
            case MMStateName.New:
                Set(New);
                break;
        }
    }
}
public enum MMStateName
{
    Main,
    New,
    Saves,
    Options
}