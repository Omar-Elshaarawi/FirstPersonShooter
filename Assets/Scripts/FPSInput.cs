using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour, IToggleable
{
    public float speed = 3f;
    public float gravity = -9.8f;

    // FEATURE 2: To make the charater stop moving after they die 
    // Reference to character controller component
    private CharacterController charController;

    private bool allowedToMove;

    public void ToggleBehavior(bool b) {
        allowedToMove = b;
    }


    public const float _baseSpeed = 6f;

    private void OnEnable()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        speed = _baseSpeed * value;
    }

    // Start is called before the first frame update
    void Start()
    {
        allowedToMove = true;
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if statement to check if the player is still alive or dead
        if (allowedToMove) {
            // get the horizontal and vertical movement from the players keyboard
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;

            // create new vector for representing this movement
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);

            // clamp movement so it moves no faster than the speed
            movement = Vector3.ClampMagnitude(movement, speed);

            // apply gravity
            movement.y = gravity;

            // multiply by time.deltatime so movement is agnostic of framework
            movement *= Time.deltaTime;

            //transform from local coords to global coords
            movement = transform.TransformDirection(movement);

            // call the character controllers move method and pass in the movement vector
            charController.Move(movement);
        }
    }
}
