using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementPlayer : MonoBehaviour
{
    PlayerControls playerControls;
    private Rigidbody2D rb2d;
    private Vector2 moveXY;
    public float walkspeed;
    public float sprintspeed;
    private enum state {idle, walk, sprint}
    private state current_state;
    // Start is called before the first frame update
    private void Awake()
    {

        playerControls = new PlayerControls();
    }
    void Start() 
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerControls.PlayerInput.Move.performed += Movement_Performed;
        playerControls.PlayerInput.Move.canceled += Movement_Canceled;

        playerControls.PlayerInput.Sprint.performed += Sprint_Performed;
        playerControls.PlayerInput.Sprint.canceled += Sprint_Canceled;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Movement_Performed(InputAction.CallbackContext context)
    {
        if (current_state != state.sprint)
            current_state = state.walk;
        moveXY = context.ReadValue<Vector2>();
        Movement_Check();
    }
    private void Movement_Canceled(InputAction.CallbackContext context)
    {
        if (current_state == state.walk)
            current_state = state.idle;
        moveXY = moveXY = Vector2.zero;
        Movement_Check();
    }
    private void Sprint_Performed(InputAction.CallbackContext context)
    {
        current_state = state.sprint;
        Movement_Check();
    }
    private void Sprint_Canceled(InputAction.CallbackContext context)
    {
        current_state = state.walk;
        Movement_Check();
    }
    private void Movement_Check()
    {
        float speed = 0;
            switch (current_state) 
        {
            case state.sprint:
                speed = sprintspeed;
                break;
            case state.walk:
                speed = walkspeed;
                break;
        }

        rb2d.velocity = moveXY * speed;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
