using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45f;
    public float maximumVert = 45f;

    private float verticalRot = 0;

    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null) {
            rigidbody.freezeRotation = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX) {
            // Horizontal rotation
            transform.Rotate(0, sensitivityHor * Input.GetAxis("Mouse X"), 0);
        } else if (axes == RotationAxes.MouseY) {
            // Vertical rotation
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);

        } else {
            // Horizontal and vertical rotation
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }


    }
}
