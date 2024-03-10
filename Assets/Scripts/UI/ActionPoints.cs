using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ActionPoints: AttachableUI
    {
        [SerializeField] private GameObject actionPoint;
        
        private int _maxPoints;
        private int _lastPoints;

        private readonly List<GameObject> _actionPoints = new();
        private static readonly int IsWastedBoolean = Animator.StringToHash("isWasted");

        protected override void Start()
        {
            var owner = IsAttached ? AttachedTo! : FindObjectOfType<PlayerController>();
            _maxPoints = owner.MaxActionPoints;
            for (var points = 0; points < _maxPoints; points++)
            {
                _actionPoints.Add(Instantiate(actionPoint, transform));
            }
            _lastPoints = _maxPoints;
        }

        public void OnPointsChanged(int points)
        {
            for (var i = _lastPoints; i > points; i--)
            {
                AnimatePoint(i - 1, true);
            }

            for (var i = _lastPoints; i < points; i++)
            {
                AnimatePoint(i, false);
            }
            _lastPoints = points;
        }

        private void AnimatePoint(int i, bool isWasted)
        {
            _actionPoints[i].GetComponent<Animator>().SetBool(IsWastedBoolean, isWasted);
        }
    }
}