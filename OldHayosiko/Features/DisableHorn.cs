using UnityEngine;

namespace OldHayosiko.Features
{
    internal class DisableHorn
    {
        internal static void ApplyDisableHorn(bool isDisableHornOn)
        {
            if (!isDisableHornOn) return;
            // GameObject.Find("HAYOSIKO(1500kg, 250)/LOD/Dashboard/ButtonHorn").SetActive(false);
            GameObject.Find("HAYOSIKO(1500kg, 250)")
                .transform
                .GetChild(6)
                .GetChild(6)
                .GetChild(0)
                .gameObject
                .SetActive(false);
        }
    }
}