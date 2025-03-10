using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CardSystem
{
    public class CardHoverEffect : MonoBehaviour
    {
        private Material originalMaterial;
        private Material hoverMaterial;
        private Image cardImage;

        private void Start()
        {
            cardImage = GetComponent<Image>();
            originalMaterial = cardImage.material;
            
            hoverMaterial = new Material(Shader.Find("Custom/UIHoverOutline"));
            hoverMaterial.SetColor("_OutlineColor", Color.yellow);
            hoverMaterial.SetFloat("_OutlineWidth", 0.03f);
        }

        public void OnPointerEnter()
        {
            cardImage.material = hoverMaterial;
        }

        public void OnPointerExit()
        {
            cardImage.material = originalMaterial;
        }
    }
}
