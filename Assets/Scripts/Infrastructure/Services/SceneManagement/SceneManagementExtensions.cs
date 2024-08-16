using System;

namespace Infrastructure.Services.SceneManagement
{
    public static class SceneManagementExtensions
    {
        public static SceneName ToSceneName(this string sceneName)
        {
            return sceneName switch
            {
                "Init" => SceneName.Initial,
                "Main" => SceneName.Main,
                _ => throw new ArgumentOutOfRangeException(nameof(sceneName), sceneName, null)
            };
        }

        public static string ToSceneString(this SceneName sceneName)
        {
            return sceneName switch
            {
                SceneName.Initial => "Init",
                SceneName.Main => "Main",
                _ => throw new ArgumentOutOfRangeException(nameof(sceneName), sceneName, null)
            };
        }
    }
}