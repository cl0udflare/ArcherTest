using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public class ToggleFireArrow : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        private void OnValidate() => 
            _toggle = GetComponent<Toggle>();

        public void ToggleRegister(Action<bool> callback) => 
            _toggle.onValueChanged.AddListener(callback.Invoke);
    }
}