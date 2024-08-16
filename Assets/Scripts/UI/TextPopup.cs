using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextPopup : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnValidate()
        {
            _canvas = GetComponent<Canvas>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                PlayUpAnimation();
            }
        }

        public void SetCamera(Camera mainCamera) =>
            _canvas.worldCamera = mainCamera;

        public void SetText(string text) =>
            _text.text = text;

        public void SetColor(Color color) =>
            _text.color = color;

        public void SetFontSize(float size) =>
            _text.fontSize = size;

        public void PlayUpAnimation(float moveToUpDistance = 1, float speed = 1)
        {
            Sequence sequence = DOTween.Sequence();
            float upPoint = transform.position.y + moveToUpDistance;

            sequence.Append(transform.DOMoveY(upPoint, speed));
            sequence.Insert(0, _text.DOFade(0, speed));

            sequence.OnComplete(() => Destroy(gameObject));
        }
    }
}