using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private ParticleSystem _particleSystem;
        
        private int _damage;

        public int Damage
        {
            set
            {
                if (value <= 0)
                    _damage = 1;

                _damage = value;
            }
        }

        private void OnValidate()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Aim target = other.gameObject.GetComponent<Aim>();
        
            if (target)
            {
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                transform.parent = target.transform;
                
                target.GetDamage(_damage);
                StopFireParticle();
            }
        }

        public void Shoot(Transform moveToRoot, Vector3 velocity)
        {
            transform.parent = null;

            transform.DOMove(moveToRoot.position, _rigidbody2D.totalForce.x / 3)
                .OnComplete(() => { Launch(velocity); });
        }
        
        public void TookAnimation()
        {
            transform.DOLocalRotate(Vector3.zero, .5f)
                .From(Vector3.forward * 180);
        }

        public void PlayFireParticle() => 
            _particleSystem.Play();
        
        public void StopFireParticle() => 
            _particleSystem.Stop();

        private void Launch(Vector3 velocity)
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.velocity = velocity;
            
            InvokeRepeating(nameof(UpdateRotation), 0, 0.02f);
        }

        private void UpdateRotation()
        {
            Vector2 velocity = _rigidbody2D.velocity;
            
            if (velocity.magnitude < 0.1f) 
            {
                CancelInvoke(nameof(UpdateRotation));
                return;
            }
            
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}