using UnityEngine;

namespace StaticData.Level
{
    [CreateAssetMenu(fileName = "levelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        [Space]
        public Vector2 HeroSpawn;
    }
}