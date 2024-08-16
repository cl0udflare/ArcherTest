using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "archerData", menuName = "StaticData/Archer")]
    public class ArcherStaticData : ScriptableObject
    {
        public int Force = 8;
    }
}