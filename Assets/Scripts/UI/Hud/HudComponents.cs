using UnityEngine;

namespace UI.Hud
{
    public class HudComponents : MonoBehaviour
    {
        [SerializeField] private SpawnArrowButton _spawnArrow;
        [SerializeField] private ToggleFireArrow _toggleFire;

        public SpawnArrowButton SpawnArrow => _spawnArrow;
        public ToggleFireArrow ToggleFire => _toggleFire;
        
        private void OnValidate()
        {
            _spawnArrow = GetComponentInChildren<SpawnArrowButton>();
            _toggleFire = GetComponentInChildren<ToggleFireArrow>();
        }
    }
}