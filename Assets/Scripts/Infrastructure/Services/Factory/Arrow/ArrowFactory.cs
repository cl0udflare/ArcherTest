using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Infrastructure.Services.Factory.Arrow
{
    public class ArrowFactory : IArrowFactory
    {
        private readonly IAssets _asset;
        private readonly IStaticDataService _staticData;

        public ArrowFactory(IAssets asset, IStaticDataService staticData)
        {
            _asset = asset;
            _staticData = staticData;
        }

        public Logic.Arrow CreateArrow(Transform parent, bool isFireArrow = false)
        {
            Logic.Arrow arrow = _asset.Instantiate<Logic.Arrow>(AssetPath.ArrowPath, parent);
            
            arrow.Damage = !isFireArrow 
                ? _staticData.Arrow.Damage 
                : _staticData.Arrow.Damage * _staticData.Arrow.MultipleFireDamage;
            
            return arrow;
        }
    }
}