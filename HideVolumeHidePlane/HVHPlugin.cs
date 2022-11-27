using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using ModdingTales;
using UnityEngine;
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

        internal static ConfigEntry<ModdingUtils.LogLevel> _logLevel { get; set; }
        internal static Harmony harmony;

        internal static ModdingUtils.LogLevel LogLevel => _logLevel.Value == ModdingUtils.LogLevel.Inherited ? ModdingUtils.LogLevelConfig.Value : _logLevel.Value;
        internal static ManualLogSource _logger;

        public static void DoPatching()
        {
            harmony = new Harmony(Guid);
            harmony.PatchAll();
            if (LogLevel > ModdingUtils.LogLevel.None) _logger.LogInfo($"{Name}: Patched.");
        }

        public static void UnPatch()
        {
            harmony.UnpatchSelf();
            if (LogLevel> ModdingUtils.LogLevel.None) _logger.LogInfo($"{Name}: UnPatched.");
        }

        public static void DoConfig(ConfigFile Config)
        {
            _logLevel = Config.Bind("Logging", "Level", ModdingUtils.LogLevel.Low);
            if (LogLevel > ModdingUtils.LogLevel.None) _logger.LogInfo($"{Name}: Config Bound.");
        }

        private void Awake()
        {
            _logger = Logger;
            DoConfig(Config);
            DoPatching();
            if (LogLevel > ModdingUtils.LogLevel.None) _logger.LogInfo($"{Name} is Active.");
            ModdingUtils.Initialize(this, Logger, "HolloFoxes'");
        }
    }
}