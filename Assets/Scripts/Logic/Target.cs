using Infrastructure.Services.Factory.Popup;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic
{
    public class Aim : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destroyParticle;
        [SerializeField] private float _targetRadius = 0.5f;

        private Camera _camera;
        private ITextPopupFactory _popupFactory;
        private int _maxAttempts = 10;

        public void Construct(ITextPopupFactory popupFactory) => 
            _popupFactory = popupFactory;

        private void Start() => 
            _camera = Camera.main;

        public void MoveTargetToRandomPosition()
        {
            gameObject.SetActive(true);
            
            Vector3 cameraPosition = _camera.transform.position;
            Vector3 minBounds = _camera.ViewportToWorldPoint(new Vector3(0, 0, cameraPosition.z));
            Vector3 maxBounds = _camera.ViewportToWorldPoint(new Vector3(1, 1, cameraPosition.z));

            int attempts = 0;
            bool isPositionValid = false;
            Vector3 randomPosition = Vector3.zero;

            while (attempts < _maxAttempts)
            {
                float randomX = Random.Range(minBounds.x + _targetRadius, maxBounds.x - _targetRadius);
                float randomY = Random.Range(minBounds.y + _targetRadius, maxBounds.y - _targetRadius);
                randomPosition = new Vector3(randomX, randomY, transform.position.z);
            
                Collider2D hitCollider = Physics2D.OverlapCircle(randomPosition, _targetRadius);

                if (hitCollider == null)
                {
                    isPositionValid = true; 
                    break;
                }
            
                attempts++;
            }

            if (isPositionValid) 
                transform.position = randomPosition;
        }

        public void GetDamage(int damage)
        {
            if (damage < 0) 
                Debug.LogWarning("Damage less than 0!");

            ShowDamagePopup(damage);
            CreateParticle();
            print($"DMG: {damage}");
            
            gameObject.SetActive(false);
        }

        private void CreateParticle() => 
            Instantiate(_destroyParticle, transform.position, Quaternion.identity);

        private void ShowDamagePopup(int damage)
        {
            TextPopup popup = _popupFactory.CreateText();

            Vector3 position = transform.position;
            position.y += 1;
            popup.transform.position = position;
            
            popup.SetCamera(_camera);
            popup.SetText($"{damage}");
            popup.SetFontSize(12);
            popup.SetColor(Color.red);
            popup.PlayUpAnimation(2, 2);
        }
    }
}
