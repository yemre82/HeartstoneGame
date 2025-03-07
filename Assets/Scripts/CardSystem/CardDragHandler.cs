using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Effects;
using Assets.Scripts.Players;

namespace Assets.Scripts.CardSystem
{
    public class CardDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private Vector3 originalPosition;
        private Transform originalParent;
        private bool isDragging = false;
        private Card card;

        private void Start()
        {
            card = GetComponent<Card>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            originalPosition = transform.position;
            originalParent = transform.parent;
            isDragging = true;
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

            ICardEffect effect = null;

            if (card.cardData.cardType == CardType.Attack)
            {
                Enemy enemy = FindFirstObjectByType<Enemy>();
                effect = new DamageEffect(card.cardData.effectValue, enemy);
            }
            else if (card.cardData.cardType == CardType.Heal)
            {
                Player player = FindFirstObjectByType<Player>();
                effect = new HealEffect(card.cardData.effectValue, player);
            }

            effect?.ApplyEffect();
            Destroy(gameObject);
        }
    }

}
