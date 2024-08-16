using System.Collections.Generic;
using UnityEngine;

namespace Logic.ArcherLogic
{
    public class TrajectoryDrawer : MonoBehaviour
    {
        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private Transform _trajectoryRoot;
        [SerializeField] private int _pointCount = 10;
        [SerializeField] private float _timeStep = 0.1f;
        [SerializeField] private float _minPointSize = 0.25f;
        [SerializeField] private float _maxPointSize = 1.0f;

        private readonly List<GameObject> _trajectoryPoints = new();

        private void Start() => 
            CreatePoints();

        public void DrawTrajectory(Vector3 startPosition, Vector3 velocity)
        {
            for (int i = 0; i < _trajectoryPoints.Count; i++)
            {
                float step = i * _timeStep;
                Vector3 position = CalculatePosition(startPosition, velocity, step);
                GameObject point = _trajectoryPoints[i];

                point.transform.position = position;
            }
        }

        private void CreatePoints()
        {
            for (int i = 0; i < _pointCount; i++)
            {
                float step = i * _timeStep;
                float scale = Mathf.Lerp(_minPointSize, _maxPointSize, 1 - (step / (_pointCount * _timeStep)));

                GameObject point = Instantiate(_pointPrefab, _trajectoryRoot);
                point.transform.localScale = new Vector3(scale, scale, 1);
                _trajectoryPoints.Add(point);
            }
        }

        private Vector3 CalculatePosition(Vector3 startPosition, Vector3 velocity, float step)
        {
            Vector3 gravity = Physics2D.gravity * (step * step * 0.5f);
            Vector3 position = startPosition + velocity * step + gravity;
            return position;
        }
    }
}