using UnityEngine;
using TMPro;
using TradeMarket.Utilities;

namespace TradeMarket.NPCSystem
{
    public class NPCView : MonoBehaviour
    {
        [Header("Visual Components")]
        [SerializeField] private SpriteRenderer npcSpriteRenderer;
        [SerializeField] private GameObject interactionPrompt;

        [Header("Dialogue")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private float dialogueDisplayTime = 3f;

        private NPCController npcController;
        private bool playerInRange = false;
        private Coroutine dialogueCoroutine;

        public void SetController(NPCController npcControllerToSet) => npcController = npcControllerToSet;

        public void Initialize(NPCScriptableObject npcData)
        {
            if (npcSpriteRenderer != null && npcData.npcSprite != null)
                npcSpriteRenderer.sprite = npcData.npcSprite;

            interactionPrompt?.SetActive(false);
            dialoguePanel?.SetActive(false);
        }

        private void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E))
                npcController?.OnPlayerInteract();
        }

        public void ShowDialogue(string dialogue)
        {
            if (dialoguePanel == null || dialogueText == null) return;

            if (dialogueCoroutine != null)
                StopCoroutine(dialogueCoroutine);

            dialogueText.text = dialogue;
            dialoguePanel.SetActive(true);
            dialogueCoroutine = StartCoroutine(HideDialogueAfterDelay());
        }

        private System.Collections.IEnumerator HideDialogueAfterDelay()
        {
            yield return new WaitForSeconds(dialogueDisplayTime);
            dialoguePanel?.SetActive(false);
        }

        public void UpdateTradeStatus(bool hasTraded)
        {
            if (interactionPrompt != null && hasTraded)
                interactionPrompt.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameString.PlayerTag))
            {
                playerInRange = true;
                if (interactionPrompt != null && !npcController.NPCModel.HasTraded)
                    interactionPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameString.PlayerTag))
            {
                playerInRange = false;
                npcController?.ResetTradeOfferState();
                interactionPrompt?.SetActive(false);
                dialoguePanel?.SetActive(false);

                if (dialogueCoroutine != null)
                {
                    StopCoroutine(dialogueCoroutine);
                    dialogueCoroutine = null;
                }
            }
        }
    }
}