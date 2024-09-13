using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIState : MonoBehaviour {
    public abstract void Enter(UIState from);

    public abstract void Exit(UIState to);
}
