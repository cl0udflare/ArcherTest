using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public class SpawnArrowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnValidate() => 
            _button = GetComponent<Button>();

        public void RegisterCallback(Action callback) => 
            _button.onClick.AddListener(callback.Invoke);

        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();
    }
}