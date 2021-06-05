using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rbody;
    [SerializeField] private Animator animator;

    [SerializeField] private float mSpeed = 2f;
    [SerializeField] private float rSpeed = 2f;

    private Vector3 moveInput;
    private Vector3 rotateInput;

    private bool isMoving;

    private PlayerActionControls playerActionControls;

    private void Awake() {
        playerActionControls = new PlayerActionControls();
    }

    private void OnEnable() {
        playerActionControls.Enable();
    }

    private void OnDisable() {
        playerActionControls.Disable();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (GameManager.gameFinished)
            return;

        // Read movement value
        Vector2 moveActionInput = playerActionControls.Player.Move.ReadValue<Vector2>();
        moveInput = new Vector3(moveActionInput.x, 0, moveActionInput.y);

        // Move player
        // localize input
        moveInput = transform.TransformDirection(moveInput);
        rbody.velocity = moveInput * mSpeed;

        // Read rotation value
        float rotateActionInput = playerActionControls.Player.Rotate.ReadValue<float>();
        rotateInput = new Vector3(0, rotateActionInput, 0);

        // Rotate player
        // check if Q or E pressed
        if(rotateActionInput == 0) {
            rbody.MoveRotation(rbody.rotation);
        } else {
            Quaternion deltaRotation = Quaternion.Euler(rotateInput * Time.deltaTime * rSpeed);
            rbody.MoveRotation(rbody.rotation * deltaRotation);
        }

        // Player moving animation
        if (moveInput != Vector3.zero) {
            isMoving = true;
        } else {
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);
    }
}
