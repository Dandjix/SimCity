namespace UIInGameStateMachine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class SwitchState : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private UIStateName uIStateName;
        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            UIInGameStateMachine.Set(uIStateName);
        }
    }   
}
