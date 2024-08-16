using Infrastructure.Services.AssetManagement;
using Logic.Curtain;

namespace Infrastructure.Services.Factory.Curtain
{
    public class CurtainFactory : ICurtainFactory
    {
        private readonly IAssets _asset;

        private LoadingCurtain _curtain;
        
        public LoadingCurtain Curtain => Create();

        public CurtainFactory(IAssets asset) => 
            _asset = asset;

        private LoadingCurtain Create()
        {
            if (_curtain)
                return _curtain;
            
            _curtain = _asset.Instantiate<LoadingCurtain>(AssetPath.CurtainPath);
            return _curtain;
        }
    }
}