using System.Collections;
using UnityEngine;

namespace Logic.Curtain
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        public void DestroyCurtain() =>
            Destroy(gameObject);

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            
            gameObject.SetActive(false);
        }
    }
}