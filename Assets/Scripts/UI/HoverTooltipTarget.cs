using UnityEngine;

namespace UI
{
    public class HoverTooltipTarget: MonoBehaviour
    {
        [SerializeField] private string title;
        public string Title => title;
    }
}