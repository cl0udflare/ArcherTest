using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Logic.ArcherLogic
{
    public class ArcherRotator : MonoBehaviour
    {
        public TweenerCore<Quaternion, Vector3, QuaternionOptions> RotateToTarget(Transform target)
        {
            TurnToLeftRight(target);
            
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            if (transform.eulerAngles.y != 0)
                angle = target.position.y > 0 ? -(angle - 180) : -(angle + 180);
            
            return RotateTo(angle, 1);
        }

        public TweenerCore<Quaternion, Vector3, QuaternionOptions> RotateTo(float angle, float time)
        {
            Vector3 rotateTo = transform.eulerAngles;
            rotateTo.z = angle;
            
            return transform.DORotate(rotateTo, time);
        }

        private void TurnToLeftRight(Transform target)
        {
            int xDirection = target.position.x < 0 ? 180 : 0;
            transform.eulerAngles = new Vector3(0, xDirection, transform.eulerAngles.z);
        }
    }
}