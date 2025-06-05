using UnityEngine;
using TMPro;

namespace TradeMarket.NPCSystem
{
    public class NPCView : MonoBehaviour
    {
        [Header("Visual Components")]
        [SerializeField] private SpriteRenderer npcSpriteRenderer;
        [SerializeField] private GameObject interactionPrompt;

        // [Header("Trade Status Visual")]
        // [SerializeField] private GameObject tradedIndicator;

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

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            // if (tradedIndicator != null)
            //     tradedIndicator.SetActive(false);

            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
        }

        private void Update() => HandleInput();

        private void HandleInput()
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

            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
        }

        public void UpdateTradeStatus(bool hasTraded)
        {
            // if (tradedIndicator != null)
            //     tradedIndicator.SetActive(hasTraded);

            if (interactionPrompt != null && hasTraded)
                interactionPrompt.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
                if (interactionPrompt != null && !npcController.NPCModel.HasTraded)
                    interactionPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;

                // Reset the trade offer state when player leaves
                npcController?.ResetTradeOfferState();

                if (interactionPrompt != null)
                    interactionPrompt.SetActive(false);

                if (dialoguePanel != null)
                    dialoguePanel.SetActive(false);

                if (dialogueCoroutine != null)
                {
                    StopCoroutine(dialogueCoroutine);
                    dialogueCoroutine = null;
                }
            }
        }
    }
}