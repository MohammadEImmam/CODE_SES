namespace Computer
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// represents the desktop background. and handles the click actions on it
    /// </summary>
    public class DesktopBackground : MonoBehaviour, IPointerClickHandler
    {
        public event Action onRightClick;
        public event Action onLeftClick;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onRightClick?.Invoke();
            }
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onLeftClick?.Invoke();
            }
        }
    }
}