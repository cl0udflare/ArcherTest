using DG.Tweening;
using Infrastructure.Services.Factory.Arrow;
using Infrastructure.Services.Factory.Popup;
using UI;
using UnityEngine;

namespace Logic.ArcherLogic
{
    public class Archer : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _handRoot;
        [SerializeField] private TrajectoryDrawer _trajectoryDrawer;
        [SerializeField] private ArcherAnimator _archerAnimator;
        [SerializeField] private ArcherRotator _archerRotator;

        private Aim _target;
        private Arrow _arrow;
        private IArrowFactory _arrowFactory;
        private ArcherBrain _brain;
        private ITextPopupFactory _popupFactory;
        private Camera _camera;
        private float _archerForce;
        private int _aimCount;
        private bool _isAiming;

        private Vector3 Velocity => _shootPoint.right * _archerForce;
        public bool TakeFireArrow { get; set; }

        public float ArcherForce
        {
            set
            {
                if (value <= 0)
                    _archerForce = 1;

                _archerForce = value;
            }
        }

        private void OnValidate()
        {
            _archerAnimator = GetComponentInChildren<ArcherAnimator>();
            _archerRotator = GetComponentInChildren<ArcherRotator>();
            _trajectoryDrawer = GetComponentInChildren<TrajectoryDrawer>();
        }

        public void Construct(Aim target, IArrowFactory arrowFactory, ITextPopupFactory popupFactory)
        {
            _popupFactory = popupFactory;
            _arrowFactory = arrowFactory;
            _target = target;
        }

        private void Start()
        {
            _camera = Camera.main;
            _brain = new ArcherBrain();
            _archerAnimator.TookArrow += CreateArrow;
            _archerAnimator.ShootToTarget += ShootArrow;
        }
        
        private void Update() => 
            _trajectoryDrawer.DrawTrajectory(_shootPoint.position, Velocity);

        public void AimToTarget()
        {
            _aimCount++;
            
            if (_isAiming) return;

            _aimCount = 0;
            _isAiming = true;

            _archerRotator.RotateToTarget(_target.transform).OnComplete(() =>
            {
                float angle = 
                    _brain.CalculateAngle(_shootPoint.position, _target.transform.position, _archerForce);

                if (!float.IsNaN(angle))
                {
                    _archerRotator.RotateTo(angle, 1);
                    _archerAnimator.ShootAnimation();
                }
                else
                {
                    ShowLargeDistancePopup();
                    _isAiming = false;
                }
            });
        }

        private void ShowLargeDistancePopup()
        {
            TextPopup popup = _popupFactory.CreateText();

            Vector3 position = transform.position;
            position.y += 2;
            popup.transform.position = position;

            popup.SetCamera(_camera);
            popup.SetText("Large distance :(");
            popup.SetColor(Color.black);
            popup.SetFontSize(5);
            popup.PlayUpAnimation(2, 5);
        }

        private void ShootArrow()
        {
            _arrow.Shoot(_shootPoint, Velocity);
            _archerRotator.RotateTo(0, 1)
                .SetDelay(2)
                .OnComplete(() =>
                {
                    if (_aimCount > 0)
                    {
                        _isAiming = false;
                        AimToTarget();
                        return;
                    }
                    
                    _isAiming = false;
                });
        }

        private void CreateArrow()
        {
            _arrow = _arrowFactory.CreateArrow(_handRoot, TakeFireArrow);
            _arrow.TookAnimation();

            if (TakeFireArrow)
                _arrow.PlayFireParticle();
        }

        private void OnDestroy()
        {
            _archerAnimator.TookArrow -= CreateArrow;
            _archerAnimator.ShootToTarget -= ShootArrow;
        }
    }
}