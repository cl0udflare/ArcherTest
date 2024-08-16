using Infrastructure.Services.AssetManagement;
using UI.Hud;

namespace Infrastructure.Services.Factory.Hud
{
    public class HudFactory : IHudFactory
    {
        private readonly IAssets _asset;

        public HudFactory(IAssets asset) => 
            _asset = asset;

        public HudComponents CreateHud()
        {
            HudComponents hud = _asset.Instantiate<HudComponents>(AssetPath.HudPath);
            
            return hud;
        }
    }
}