using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace HideVolumeHidePlane
{
    public enum LogLevel
    {
        None,
        Low,
        Medium,
        High,
        All,
    }


    [BepInPlugin(Guid, Name, Version)]
    public class HVHPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.HF.plugins.HVHP";
        public const string Version = "1.0.2.0";
        private const string Name = "HolloFoxes' Hide Plane for Hide Volumes";

        internal static ConfigEntry<LogLevel> LogLevel { get; set; }
        internal static Harmony harmony;

        public static void DoPatching()
        {
            harmony = new Harmony(Guid);
            harmony.PatchAll();
            if (LogLevel.Value > HideVolumeHidePlane.LogLevel.None) Debug.Log($"{Name}: Patched.");
        }

        public static void UnPatch()
        {
            harmony.UnpatchSelf();
            if (LogLevel.Value > HideVolumeHidePlane.LogLevel.None) Debug.Log($"{Name}: UnPatched.");
        }

        public static void DoConfig(ConfigFile Config)
        {
            LogLevel = Config.Bind("Logging", "Level", HideVolumeHidePlane.LogLevel.Low);
            if (LogLevel.Value > HideVolumeHidePlane.LogLevel.None) Debug.Log($"{Name}: Config Bound.");
        }

        private void Awake()
        {
            DoConfig(Config);
            DoPatching();
            if (LogLevel.Value > HideVolumeHidePlane.LogLevel.None) Debug.Log($"{Name} is Active.");
            ModdingTales.ModdingUtils.Initialize(this, Logger);
        }
    }
}