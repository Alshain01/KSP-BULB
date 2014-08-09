using System;
using UnityEngine;
using KSP.IO;

namespace Bulb
{
    class BulbModule : PartModule
    {
        PluginConfiguration config = PluginConfiguration.CreateForType<BulbModule>();

        [KSPField(guiActive = true, guiName = "Light R", isPersistant = true)]
        [UI_FloatRange(maxValue = 1, minValue = 0, scene = UI_Scene.Flight, stepIncrement = 0.05f)]
        protected float red = 1;

        [KSPField(guiActive = true, guiName = "Light G", isPersistant = true)]
        [UI_FloatRange(maxValue = 1, minValue = 0, scene = UI_Scene.Flight, stepIncrement = 0.05f)]
        protected float green = 1;

        [KSPField(guiActive = true, guiName = "Light B", isPersistant = true)]
        [UI_FloatRange(maxValue = 1, minValue = 0, scene = UI_Scene.Flight, stepIncrement = 0.05f)]
        protected float blue = 1;

        [KSPField(guiActive = true, guiName = "B.U.L.B. Preset", isPersistant = false)]
        [UI_FloatRange(maxValue = 10, minValue = 1, scene = UI_Scene.All, stepIncrement = 1)]
        protected float selectedPreset = 1;

        [KSPEvent(guiName = "Load B.U.L.B. Preset", active = true, guiActive = true, guiActiveEditor=true)]
        public void loadSelectedPreset()
        {
            loadPreset((int)selectedPreset);
        }

        [KSPEvent(guiName = "Save B.U.L.B. Preset", active = true, guiActive = true, guiActiveEditor = true)]
        public void saveSelectedPreset()
        {
            savePreset((int)selectedPreset);
        }

        #region Action Groups
        [KSPAction("B.U.L.B. Preset #1")]
        public void loadPreset1(KSPActionParam param)
        {
            loadPreset(1);
        }

        [KSPAction("B.U.L.B. Preset #2")]
        public void loadPreset2(KSPActionParam param)
        {
            loadPreset(2);
        }

        [KSPAction("B.U.L.B. Preset #3")]
        public void loadPreset3(KSPActionParam param)
        {
            loadPreset(3);
        }

        [KSPAction("B.U.L.B. Preset #4")]
        public void loadPreset4(KSPActionParam param)
        {
            loadPreset(4);
        }

        [KSPAction("B.U.L.B. Preset #5")]
        public void loadPreset5(KSPActionParam param)
        {
            loadPreset(5);
        }

        [KSPAction("B.U.L.B. Preset #6")]
        public void loadPreset6(KSPActionParam param)
        {
            loadPreset(6);
        }

        [KSPAction("B.U.L.B. Preset #7")]
        public void loadPreset7(KSPActionParam param)
        {
            loadPreset(7);
        }

        [KSPAction("B.U.L.B. Preset #8")]
        public void loadPreset8(KSPActionParam param)
        {
            loadPreset(8);
        }

        [KSPAction("B.U.L.B. Preset #9")]
        public void loadPreset9(KSPActionParam param)
        {
            loadPreset(9);
        }

        [KSPAction("B.U.L.B. Preset #10")]
        public void loadPreset10(KSPActionParam param)
        {
            loadPreset(10);
        }
        #endregion

        public void Update()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                red = part.FindModelComponents<Light>()[0].color.r;
                green = part.FindModelComponents<Light>()[0].color.g;
                blue = part.FindModelComponents<Light>()[0].color.b;
            }
            else
            {
                setColor();
            }
        }

        public void loadPreset(int preset)
        {
            String presetLabel = "Preset" + preset;
            red = (config.GetValue<Int16>(presetLabel + "Red", 100) < 1) ? 0 : config.GetValue<Int16>(presetLabel + "Red", 100) / 100.0f;
            green = (config.GetValue<Int16>(presetLabel + "Green", 100) < 1) ? 0 : config.GetValue<Int16>(presetLabel + "Green", 100) / 100.0f;
            blue = (config.GetValue<Int16>(presetLabel + "Blue", 100) < 1) ? 0 : config.GetValue<Int16>(presetLabel + "Blue", 100) / 100.0f;
            setColor();
            ScreenMessages.PostScreenMessage("B.U.L.B. Preset #" + preset + " Loaded", 3, ScreenMessageStyle.UPPER_RIGHT);
        }

        public void savePreset(int preset)
        {
            String presetLabel = "Preset" + preset;
            config.SetValue(presetLabel + "Red", (short)(red * 100));
            config.SetValue(presetLabel + "Green", (short)(green * 100));
            config.SetValue(presetLabel + "Blue", (short)(blue * 100));
            config.save();
            ScreenMessages.PostScreenMessage("B.U.L.B. Preset #" + preset + " Saved", 3, ScreenMessageStyle.UPPER_RIGHT);
        }

        public void setColor()
        {
            foreach (Light lgt in part.FindModelComponents<Light>())
                lgt.color = new Color(red, green, blue);

            foreach (Renderer emi in part.FindModelComponents<Renderer>())
                emi.material.SetColor("_EmissiveColor", new Color(red, green, blue, 1));
        }
    }
}
