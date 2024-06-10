using BepInEx;
using HarmonyLib;
using ModdingTales;
using PluginUtilities;
using BepInEx.Logging;

namespace HideVolumeHidePlane
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public sealed class HVHPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.HF.plugins.HVHP";
        public const string Version = "0.0.0.0";
        private const string Name = "Hide Plane for Hide Volumes";

        internal static Harmony harmony;

        internal static ManualLogSource _logger;

        public static void DoPatching()
        {
            harmony = new Harmony(Guid);
            harmony.PatchAll();
            _logger.LogInfo($"{Name}: Patched.");
        }

        public static void UnPatch()
        {
            harmony.UnpatchSelf();
            _logger.LogInfo($"{Name}: UnPatched.");
        }

        private void Awake()
        {
            _logger = Logger;
            DoPatching();
            _logger.LogInfo($"{Name} is Active.");
            ModdingUtils.AddPluginToMenuList(this, "HolloFoxes'");
        }
    }
}