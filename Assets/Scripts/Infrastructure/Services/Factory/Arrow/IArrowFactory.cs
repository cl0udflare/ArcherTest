using UnityEngine;

namespace Infrastructure.Services.Factory.Arrow
{
    public interface IArrowFactory 
    {
        Logic.Arrow CreateArrow(Transform parent, bool isFireArrow = false);
    }
}