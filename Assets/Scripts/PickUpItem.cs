using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    [SerializeField] private ItemObject item;
    [SerializeField] Renderer rend;

    [SerializeField] GameObject keySound;

    private Inventory inventory;
    private UISystem uiSystem;

    private Color startColor;

    private bool playerInRange;

    void Start() {
        startColor = rend.material.color;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        uiSystem = GameObject.FindGameObjectWithTag("UI").GetComponent<UISystem>();
    }

    private void Update() {
        playerInRange = Physics.CheckSphere(transform.position, Inventory.InteractRange, Inventory.whatIsPlayer); // interaction range
    }

    void PickItem() {

        if (inventory.SetItem(item)) {
            // play sound
            var sound = Instantiate(keySound, gameObject.transform.position, Quaternion.identity);
            Destroy(sound, 2f);

            Destroy(gameObject);
            uiSystem.ResetMessagePanel();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (Mouse.current.leftButton.wasPressedThisFrame && playerInRange) {
            uiSystem.ShowConfirmPanel("<b>Take?</b>");
            uiSystem.messageYes.GetComponent<Button>().onClick.AddListener(PickItem);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (playerInRange)
            rend.material.color = item.hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        rend.material.color = startColor;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Inventory.InteractRange);
    }
}
