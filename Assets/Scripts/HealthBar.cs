using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthBar: UIBehaviour
{
    [SerializeField] private Image filled;
    [SerializeField] private Image empty;
    [SerializeField] private Vector3 positionShift;
    
    private SpaceShip _attachedTo;

    private void Update()
    {
        filled.fillAmount = (float) _attachedTo.Durability / _attachedTo.MaxDurability;
        transform.position = _attachedTo.transform.position + positionShift;
    }

    public void AttachTo(SpaceShip spaceShip)
    {
        _attachedTo = spaceShip;
    }
}