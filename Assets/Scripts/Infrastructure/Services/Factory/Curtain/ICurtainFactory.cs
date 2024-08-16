using Logic.Curtain;

namespace Infrastructure.Services.Factory.Curtain
{
    public interface ICurtainFactory
    {
        LoadingCurtain Curtain { get; }
    }
}