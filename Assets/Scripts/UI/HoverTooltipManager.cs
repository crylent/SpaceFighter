using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HoverTooltipManager: UIBehaviour
    {
        [SerializeField] private CanvasGroup tooltip;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        
        private Camera _camera;
        private RectTransform _rectTransform;
        private bool _isDisplayed;
        
        protected override void Start()
        {
            _camera = Camera.main;
            _rectTransform = tooltip.GetComponent<RectTransform>();
        }

        private void Update()
        {
            var cursorPos = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition);
            _rectTransform.pivot = new Vector2(cursorPos.x > 0 ? 1 : 0, cursorPos.y > 0 ? 1 : 0);
            tooltip.transform.position = cursorPos;
            // ReSharper disable once Unity.PreferNonAllocApi
            var raycast = Physics2D.RaycastAll(cursorPos, Vector2.zero);
            if (raycast.Length == 0)
            {
                HideTooltip();
                return;
            }
            
            HoverTooltipTarget target = null;
            foreach (var hit in raycast)
            {
                if(hit.transform.TryGetComponent(out target)) break;
            }
            if (target && !_isDisplayed) DisplayTooltip(target);
            else if (!target && _isDisplayed) HideTooltip();
        }

        private void DisplayTooltip(HoverTooltipTarget target)
        {
            title.SetText(target.Title);
            description.SetText(target.MakeDesc());
            tooltip.alpha = 1;
        }

        private void HideTooltip()
        {
            tooltip.alpha = 0;
        }
    }
}