using Infrastructure.Services.AssetManagement;
using Logic;
using UnityEngine;

namespace Infrastructure.Services.Factory.Target
{
    public class TargetFactory : ITargetFactory
    {
        private readonly IAssets _asset;

        public TargetFactory(IAssets asset) => 
            _asset = asset;

        public Aim CreateTarget()
        {
            Aim target = _asset.Instantiate<Aim>(AssetPath.TargetPath);
            target.transform.position = Vector3.right * 5.5f;
            
            return target;
        }
    }
}