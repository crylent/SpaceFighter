using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar: AttachableUI
    {
        [SerializeField] private Image filled;

        protected override void Update()
        {
            base.Update();
            filled.fillAmount = (float) AttachedTo.Durability / AttachedTo.MaxDurability;
        }
    }
}