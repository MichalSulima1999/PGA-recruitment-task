using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    [SerializeField] Renderer rend;

    [SerializeField] private GameObject openChestEffect;
    [SerializeField] private Color hoverColor;
    private Color startColor;

    [SerializeField] private Animator animator;

    [SerializeField] GameObject doorSound;

    bool opened = false;
    private bool playerInRange;

    private UISystem uiSystem;

    // Start is called before the first frame update
    void Start() {
        uiSystem = GameObject.FindGameObjectWithTag("UI").GetComponent<UISystem>();
        startColor = rend.material.color;
    }

    private void Update() {
        // set interaction range
        playerInRange = Physics.CheckSphere(transform.position, Inventory.InteractRange, Inventory.whatIsPlayer);
    }

    void OpenChest() {
        // Play sound
        var sound = Instantiate(doorSound, transform.position, Quaternion.identity);
        Destroy(sound, 2f);

        // Open chest
        opened = true;
        animator.SetBool("Opened", opened);
        GetComponent<Collider>().enabled = false;

        // Particles after opening
        var effect = Instantiate(openChestEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        // Hide message
        uiSystem.ResetMessagePanel();
    }

    public void OnPointerExit(PointerEventData eventData) {
        rend.material.color = startColor; // restart color
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (playerInRange && !opened)
            rend.material.color = hoverColor; // change color on hover

    }

    public void OnPointerDown(PointerEventData eventData) {
        if (Mouse.current.leftButton.wasPressedThisFrame && !opened && playerInRange) {
            uiSystem.ShowConfirmPanel("<b>Open?</b>");
            uiSystem.messageYes.GetComponent<Button>().onClick.AddListener(OpenChest);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Inventory.InteractRange); // show interaction range
    }
}
