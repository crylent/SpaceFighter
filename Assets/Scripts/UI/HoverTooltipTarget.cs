using UnityEngine;

namespace UI
{
    public abstract class HoverTooltipTarget: MonoBehaviour
    {
        public abstract string GetTitle();
        public abstract string MakeDesc();
    }
}