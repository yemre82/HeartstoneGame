using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts.CardSystem
{
    public class CardHoverEffect : MonoBehaviour
    {
        private Vector3 originalScale;
        private float hoverScaleFactor = 1.2f;
        private float animationDuration = 0.2f;

        private Material originalMaterial;
        private Material hoverMaterial;
        private Image cardImage;

        private void Start()
        {
            originalScale = transform.localScale;
            cardImage = GetComponent<Image>();
            originalMaterial = cardImage.material;
            
            hoverMaterial = new Material(Shader.Find("Custom/UIHoverOutline"));
            hoverMaterial.SetColor("_OutlineColor", Color.yellow);
            hoverMaterial.SetFloat("_OutlineWidth", 0.03f);
        }

        public void OnPointerEnter()
        {
            cardImage.material = hoverMaterial;
            transform.DOScale(originalScale * hoverScaleFactor, animationDuration).SetEase(Ease.OutBack);
        }

        public void OnPointerExit()
        {
            cardImage.material = originalMaterial;
            transform.DOScale(originalScale, animationDuration).SetEase(Ease.InBack);
        }
    }
}
