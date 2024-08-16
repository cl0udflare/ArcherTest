using UnityEngine;

namespace Logic.ArcherLogic
{
    public class ArcherBrain
    {
        public float CalculateAngle(Vector3 startPosition, Vector3 targetPosition, float archerForce)
        {
            Vector3 directionToTarget = targetPosition - startPosition;
            float dx = directionToTarget.x;
            float dy = directionToTarget.y;
            
            float gravity = Mathf.Abs(Physics2D.gravity.y);
            
            float velocitySquared = archerForce * archerForce;
            float velocityQuad = velocitySquared * velocitySquared;
            float discriminant = velocityQuad - gravity * (gravity * dx * dx + 2 * dy * velocitySquared);
            
            if (discriminant < 0)
                return float.NaN;

            float sqrtDiscriminant = Mathf.Sqrt(discriminant);
            
            float lowAngle = Mathf.Atan((velocitySquared - sqrtDiscriminant) / (gravity * dx)) * Mathf.Rad2Deg;

            return targetPosition.x < 0 ? -lowAngle : lowAngle;
        }
    }
}