using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Door : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    [SerializeField] Animator animator;
    [SerializeField] Renderer rend;
    [SerializeField] Color hoverColor;

    [SerializeField] GameObject doorSound;

    private Inventory inventory;
    private GameManager gameManager;
    private UISystem uiSystem;

    private bool opened = false;
    private bool playerInRange;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uiSystem = GameObject.FindGameObjectWithTag("UI").GetComponent<UISystem>();
        startColor = rend.material.color;
    }

    private void Update() {
        playerInRange = Physics.CheckSphere(transform.position, Inventory.InteractRange, Inventory.whatIsPlayer); // interaction range
    }

    void CheckKey() {
        if (opened)
            return;

        // check if in inventory is key ( id == 0 - our key )
        foreach (InventorySlot invSlot in inventory.invSlots) {
            if (invSlot.id == 0) {
                HasKey();
                break;
            } else {
                NoKey();
            }
        }
    }

    void NoKey() {
        uiSystem.ShowInfoPanel("<b>You need a key!</b>"); // display message
    }

    void HasKey() {
        uiSystem.ShowConfirmPanel("<b>Open?</b>"); // display confirm panel
        uiSystem.messageYes.GetComponent<Button>().onClick.AddListener(OpenDoor);
    }

    void OpenDoor() {
        // play sound effect
        var sound = Instantiate(doorSound, transform.position, Quaternion.identity);
        Destroy(sound, 2f);

        // open door
        opened = true;
        animator.SetBool("Opened", opened);
        gameManager.GameOver();

        uiSystem.ResetMessagePanel();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (Mouse.current.leftButton.wasPressedThisFrame && playerInRange) {
            CheckKey();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (playerInRange)
            rend.material.color = hoverColor; // change color on hover
    }

    public void OnPointerExit(PointerEventData eventData) {
        rend.material.color = startColor;
    }
}
