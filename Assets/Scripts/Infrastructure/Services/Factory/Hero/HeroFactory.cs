using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using Logic.ArcherLogic;
using StaticData.Level;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Factory.Hero
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssets _asset;
        private readonly IStaticDataService _staticData;

        public HeroFactory(IAssets asset, IStaticDataService staticData)
        {
            _asset = asset;
            _staticData = staticData;
        }

        public Archer CreateHero()
        {
            Archer archer = _asset.Instantiate<Archer>(AssetPath.ArcherPath);
         
            LevelStaticData levelData = LevelStaticData();
            archer.transform.position = levelData.HeroSpawn;
            archer.ArcherForce = _staticData.Archer.Force;
            
            return archer;
        }
        
        private LevelStaticData LevelStaticData() => 
            _staticData.ForLevel(SceneManager.GetActiveScene().name);
    }
}