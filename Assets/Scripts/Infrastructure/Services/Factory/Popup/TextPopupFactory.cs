using Infrastructure.Services.AssetManagement;
using UI;

namespace Infrastructure.Services.Factory.Popup
{
    public class TextPopupFactory : ITextPopupFactory
    {
        private readonly IAssets _asset;

        public TextPopupFactory(IAssets asset) => 
            _asset = asset;

        public TextPopup CreateText()
        {
            TextPopup text = _asset.Instantiate<TextPopup>(AssetPath.TextPopupPath);
            
            return text;
        }
    }
}