using BepInEx;
using HarmonyLib;
using PluginUtilities;
using HideVolumeHidePlane.Patches;

namespace HideVolumeHidePlane
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public sealed class HVHPlugin : DependencyUnityPlugin
    {
        // constants
        public const string Guid = "org.HF.plugins.HVHP";
        public const string Version = "0.0.0.0";
        private const string Name = "Hide Plane for Hide Volumes";

        internal static Harmony harmony;

        /// <summary>
        /// Awake plugin
        /// </summary>
        protected override void OnAwake()
        {
            harmony = new Harmony(Guid);
            harmony.PatchAll();
            Logger.LogDebug($"{Name} is Active.");
        }

        protected override void OnDestroyed()
        {
            // restore volumes
            Hider.UnHideVolumes();
            HVMPatch._hideVolumeItems.Clear();
            
            // unpatch
            harmony.UnpatchSelf();

            Logger.LogDebug($"{Name} is unpatched.");
        }
    }
}