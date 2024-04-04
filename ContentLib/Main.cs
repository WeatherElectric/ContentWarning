using MelonLoader;
using WeatherElectric.ContentLib.Melon;

namespace WeatherElectric.ContentLib
{
    public class Main : MelonMod
    {
        internal const string ModName = "ContentLib";
        internal const string ModVersion = "1.0.0";
        internal const string ModAuthor = "Weather Electric, SoulWithMae, BugoBug, SonOfForehead, Bread Soup";

        public override void OnInitializeMelon()
        {
            ModConsole.Setup(LoggerInstance);
            Preferences.Setup();
        }
    }
}