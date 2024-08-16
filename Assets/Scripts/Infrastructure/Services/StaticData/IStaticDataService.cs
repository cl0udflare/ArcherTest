using StaticData;
using StaticData.Level;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        LevelStaticData ForLevel(string sceneKey);
        ArcherStaticData Archer { get; }
        ArrowStaticData Arrow { get; }
    }
}