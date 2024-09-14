using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainMenuState : MonoBehaviour
{
    public abstract void Enter(MainMenuState from);

    public abstract void Exit(MainMenuState to);
}
