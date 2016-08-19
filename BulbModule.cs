using System;
using UnityEngine;
using KSP.IO;

namespace Bulb
{
    class BulbModule : PartModule
    {
        [KSPField(guiActive = true, guiName = "Light R", isPersistant = true)]
        [UI_FloatRange(maxValue = 1, minValue = 0, scene = UI_Scene.Flight, stepIncrement = 0.05f)]
        protected float red = 0;

        [KSPField(guiActive = true, guiName = "Light G", isPersistant = true)]
        [UI_FloatRange(maxValue = 1, minValue = 0, scene = UI_Scene.Flight, stepIncrement = 0.05f)]
        protected float green = 0;

        [KSPField(guiActive = true, guiName = "Light B", isPersistant = true)]
        [UI_FloatRange(maxValue = 1, minValue = 0, scene = UI_Scene.Flight, stepIncrement = 0.05f)]
        protected float blue = 0;

        public override void OnStart(StartState state)
        {
            red = part.FindModelComponent<Light>().color.r;
            green = part.FindModelComponent<Light>().color.g;
            blue = part.FindModelComponent<Light>().color.b;
        }

        public override void OnUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                Light lgt = part.FindModelComponent<Light>();
                if (lgt.color.r != red || lgt.color.g != green || lgt.color.b != blue) 
                {
                    lgt.color = new Color(red, green, blue, 1);
                    part.FindModelComponent<Renderer>().material.SetColor("_EmissiveColor", lgt.color);
                }
            }
        }
    }
}
