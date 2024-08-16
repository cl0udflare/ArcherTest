using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "arrowData", menuName = "StaticData/Arrow")]
    public class ArrowStaticData : ScriptableObject
    {
        public int Damage = 12;
        public int MultipleFireDamage = 2;
    }
}