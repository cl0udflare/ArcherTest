using System.Collections.Generic;
using System.Linq;
using StaticData;
using StaticData.Level;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> _levels;
        
        public ArcherStaticData Archer { get; private set; }
        public ArrowStaticData Arrow { get; private set; }

        public void LoadStaticData()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataPath.LevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);
            
            Archer = Resources.Load<ArcherStaticData>(StaticDataPath.ArcherPath);
            Arrow = Resources.Load<ArrowStaticData>(StaticDataPath.ArrowPath);
        }

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;
        
    }
}