using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class FPSСounter : MonoBehaviour
    {
        private const string FPSTEXT = "FPS: ";
        
        [SerializeField] private TextMeshProUGUI _FPS_Text;
        [SerializeField] private float _updateInterval = 0.5f;
        
        private float _lastInterval;
        private float _frames;
        private float _fps;

        private void OnValidate()
        {
            if (!_FPS_Text)
                _FPS_Text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _lastInterval = Time.realtimeSinceStartup;
            _frames = 0;
        }

        private void Update() => 
            FPSCount();

        private void FPSCount()
        {
            ++_frames;
            float timeNow = Time.realtimeSinceStartup;

            if (timeNow > _lastInterval + _updateInterval)
            {
                _fps = _frames / (timeNow - _lastInterval);
                _FPS_Text.text = FPSTEXT + _fps;
                
                _frames = 0;
                _lastInterval = timeNow;
            }
        }
    }
}
