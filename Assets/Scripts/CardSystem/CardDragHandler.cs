using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Audio;

namespace Assets.Scripts.CardSystem
{
    public class CardDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {

        public Vector3 originalPosition;
        public Transform originalParent;
        private bool isDragging = false;
        [SerializeField] private Card card;
        



        public void OnPointerDown(PointerEventData eventData)
        {
            originalPosition = transform.position;
            originalParent = transform.parent;
            isDragging = true;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.cardDrawSound);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDragging)
            {
                transform.position = Input.mousePosition;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;

            if (IsOverPlayArea())
            {
                PlayCard();
            }
            else
            {
                transform.position = originalPosition;
                transform.SetParent(originalParent);
            }
        }

        private bool IsOverPlayArea()
        {
            GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
            if (playArea == null) return false;

            return RectTransformUtility.RectangleContainsScreenPoint(
                playArea.GetComponent<RectTransform>(),
                Input.mousePosition
            );
        }

        private void PlayCard()
        {
            Debug.Log("Card Played: " + card.cardData.cardName);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.cardPlaySound);

            card.PlayCard();
        }
    }

}
