using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIStateInGame : MonoBehaviour {
    public abstract void Enter(UIStateInGame from);

    public abstract void Exit(UIStateInGame to);
}
