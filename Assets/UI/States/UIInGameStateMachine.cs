using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(UIState_Civilian))]
[RequireComponent(typeof(UIState_CivilianTerrain))]
[RequireComponent(typeof(UIState_CivilianDistricts))]
[RequireComponent(typeof(UIState_CivilianRoads))]
[RequireComponent(typeof(UIState_Military))]
[RequireComponent(typeof (UIState_Escape))]
[RequireComponent(typeof(UIState_Quit))]
public class UIInGameStateMachine : MonoBehaviour
{
    public static UIInGameStateMachine Instance {  get; private set; }

    private void Start()
    {
        Instance = this;

        UIState_Civilian = GetComponent<UIState_Civilian>();
        UIState_CivilianTerrain = GetComponent<UIState_CivilianTerrain>();
        UIState_CivilianDistricts = GetComponent<UIState_CivilianDistricts>();
        UIState_CivilianRoads = GetComponent<UIState_CivilianRoads>();

        UIState_Military = GetComponent<UIState_Military>();

        UIState_Escape = GetComponent<UIState_Escape>();
        UIState_Quit = GetComponent<UIState_Quit>();

        Set(UIState_Civilian);
    }

    private UIStateInGame current;

    public static UIState_Civilian UIState_Civilian { get; private set; }
    public static UIState_CivilianTerrain UIState_CivilianTerrain { get; private set; }
    public static UIState_CivilianDistricts UIState_CivilianDistricts {get; private set;}
    public static UIState_CivilianRoads UIState_CivilianRoads {get; private set;}

    public static UIState_Military UIState_Military {get; private set;}

    public static UIState_Escape UIState_Escape { get; private set; }
    public static UIState_Quit UIState_Quit { get; private set; }

    public static void Set(UIStateInGame state)
    {
        if(Instance.current != null)
        {
            Instance.current.Exit(state);
            Instance.current.enabled = false;
        }


        UIStateInGame old = Instance.current;
        Instance.current = state;

        state.Enter(old);
        state.enabled = true;
    }

    public static void Set(UIStateName name)
    {
        switch (name)
        {
            case UIStateName.UIState_Civilian:
                Set(UIState_Civilian);
                return;
            case UIStateName.UIState_CivilianTerrain:
                Set(UIState_CivilianTerrain);
                return;
            case UIStateName.UIState_CivilianRoads:
                Set(UIState_CivilianRoads);
                return;
            case UIStateName.UIState_CivilianDistricts:
                Set(UIState_CivilianDistricts);
                return;
            case UIStateName.UIState_Military:
                Set(UIState_Military);
                return;
            case UIStateName.UIState_Escape:
                Set(UIState_Escape);
                return;
            case UIStateName.UIState_Quit:
                Set(UIState_Quit);
                return;


                //Debug.LogError("INVALID STATE NAME : " + name);
        }
    }
}
public enum UIStateName
{
    UIState_Civilian,
    UIState_CivilianTerrain,
    UIState_CivilianRoads,
    UIState_CivilianDistricts,
    UIState_Military,
    UIState_Escape,
    UIState_Quit
}