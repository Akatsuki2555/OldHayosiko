using System;
using System.Reflection;
using JetBrains.Annotations;
using MSCLoader;
using OldHayosiko.Features;
using UnityEngine;

namespace OldHayosiko
{
    [UsedImplicitly]
    public class OldHayosiko : Mod
    {
        private SettingsCheckBox _removeDashboard;
        private SettingsCheckBox _steeringWheel;
        private SettingsCheckBox _disableHorn;
        private SettingsCheckBox _oldEngine;
        private SettingsCheckBox _oldOutsideTexture;
        private SettingsCheckBox _oldInsideTexture;

        public override string ID => nameof(OldHayosiko);

        public override string Name => "Old Hayosiko";

        public override string Author => "アカツキ";

        public override string Version => "1.0.2";

        public override void OnLoad()
        {
            byte[] numArray;
            using (var manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("OldHayosiko.Resources.oldhayoso.unity3d"))
            {
                if (manifestResourceStream == null)
                    throw new Exception("The mod DLL is corrupted, unable to load oldhayoso.unity3d. Cannot continue");
                numArray = new byte[manifestResourceStream.Length];
                _ = manifestResourceStream.Read(numArray, 0, numArray.Length);
            }

            var assetBundle = numArray.Length != 0
                ? AssetBundle.CreateFromMemoryImmediate(numArray)
                : throw new Exception("The mod DLL is corrupted, unable to load oldhayoso.unity3d. Cannot continue");
            ModConsole.Print("Loading OldHayosiko");
            
            Features.SteeringWheel.ApplySteeringWheel(assetBundle, _steeringWheel.GetValue());
            RemoveDashboard.ApplyRemoveDashboard(_removeDashboard.GetValue());
            DisableHorn.ApplyDisableHorn(_disableHorn.GetValue());
            OldEngine.ApplyOldEngine(assetBundle, _oldEngine.GetValue());
            OldInsideTextures.ApplyOldInsideTextures(assetBundle, _oldInsideTexture.GetValue(), _removeDashboard.GetValue());
            OldOutsideTextures.ApplyOldOutsideTexture(assetBundle, _oldOutsideTexture.GetValue());
        }

        public override void ModSettings()
        {
            _removeDashboard = Settings.AddCheckBox(this, "dashboard", "Remove Dashboard");
            _steeringWheel = Settings.AddCheckBox(this, "wheel", "Old steering wheel");
            _disableHorn = Settings.AddCheckBox(this, "horn", "Disable horn");
            Settings.AddCheckBox(this, "shift", "Instant Shifting");
            _oldEngine = Settings.AddCheckBox(this, "engine", "Old engine sound");
            _oldOutsideTexture = Settings.AddCheckBox(this, "texture", "Old body texture");
            _oldInsideTexture = Settings.AddCheckBox(this, "texture2", "Old inside textures");
        }
    }
}