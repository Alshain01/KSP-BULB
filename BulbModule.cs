using UnityEngine;

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

        Light light;
        Renderer emissive;

        public override void OnStart(StartState state)
        {
            light = part.FindModelComponent<Light>();
            emissive = part.FindModelComponent<Renderer>();
        }

        public void Update()
        {
            if (HighLogic.LoadedSceneIsFlight)
                setLightColor();
            else if (HighLogic.LoadedSceneIsEditor)
                recordLightColor();
        }

        public void recordLightColor()
        {
            red = light.color.r;
            green = light.color.g;
            blue = light.color.b;
        }

        public void setLightColor()
        {
            if (light.color.r != red || light.color.g != green || light.color.b != blue)
            {
                light.color = new Color(red, green, blue, 1);
                emissive.material.SetColor("_EmissiveColor", light.color);
            }
        }
    }
}
