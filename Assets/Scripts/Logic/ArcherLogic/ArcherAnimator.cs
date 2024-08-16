using System;
using UnityEngine;

namespace Logic.ArcherLogic
{
    [RequireComponent(typeof(Animator))]
    public sealed class ArcherAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int Shoot = Animator.StringToHash("Shoot");
    
        public event Action TookArrow;
        public event Action ShootToTarget;
    
        private void OnValidate() => 
            _animator = GetComponent<Animator>();

        public void ShootAnimation() => 
            _animator.SetTrigger(Shoot);

        private void OnTookArrow() => 
            TookArrow?.Invoke();
    
        private void OnShootToTarget() => 
            ShootToTarget?.Invoke();
    }
}
