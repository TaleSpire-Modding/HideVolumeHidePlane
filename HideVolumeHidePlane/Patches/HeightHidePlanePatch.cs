using Bounce.ManagedCollections;
using HarmonyLib;

namespace HideVolumeHidePlane.Patches
{
    [HarmonyPatch(typeof(HideVolumeManager), "OnShowHideVolumesChange")]
    internal sealed class HVMPatch
    {
        internal static BList<HideVolumeItem> _hideVolumeItems;

        internal static bool IsActive;

        static void Postfix(ref bool visibility, ref BList<HideVolumeItem> ____hideVolumeItems)
        {
            _hideVolumeItems = ____hideVolumeItems;
            IsActive = visibility;

            Hider.HideVolumes();
        }

    }

    [HarmonyPatch(typeof(HeightHidePlane), "Update")]
    internal sealed class HeightHidePlanePatch
    {
        internal static float lastHeight;

        static void Postfix(ref float ____currentHeight)
        {
            if (____currentHeight == lastHeight) return;
            lastHeight = ____currentHeight;
            
            Hider.HideVolumes();
        }
    }
    
    internal static class Hider
    {
        public static void HideVolumes()
        {
            foreach (var hv in HVMPatch._hideVolumeItems ?? new BList<HideVolumeItem>())
            {
                var b = hv.HideVolume.Bounds;
                var cutoff = b.size.y / 2 + b.center.y;

                hv.VisibilityChange(HVMPatch.IsActive && cutoff < HeightHidePlanePatch.lastHeight);
            }
        }
    }
}