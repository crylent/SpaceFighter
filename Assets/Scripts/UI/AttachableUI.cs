using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AttachableUI: UIBehaviour
    {
        [SerializeField] private Vector3 positionShift;
    
        [CanBeNull] protected SpaceShip AttachedTo;
        protected bool IsAttached => !AttachedTo.IsUnityNull();

        protected virtual void Update()
        {
            if (AttachedTo.IsDestroyed())
            {
                Destroy(gameObject);
                return;
            }
            if (!IsAttached) return;
            transform.position = AttachedTo!.transform.position + positionShift;
        }

        public void AttachTo(SpaceShip spaceShip)
        {
            AttachedTo = spaceShip;
        }
    }
}