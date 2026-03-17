using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour, IToggleable
{
    // Private field, stores a reference to the camera
    private Camera cam;

    private bool allowedToShoot;

    // Ammo left
    private int ammoLeft;
    [SerializeField] private TMP_Text ammoText;

    // Reloading time
    private float reloadingTimeLeft;
    private bool isReloading;

    public void ToggleBehavior(bool b)
    {
        allowedToShoot = b;
    }

    // Start is called before the first frame update
    void Start()
    {
        allowedToShoot = true;

        // Initialize reloading time and reloading state
        reloadingTimeLeft = 0f;
        isReloading = false;

        // Initialize to 5 units of ammo
        ammoLeft = 5;
        ammoText.text = $"Ammo left: {ammoLeft}";

        // Get a reference to the camera
        cam = GetComponent<Camera>();

        // hide the cursor and lock it at the center of the screen
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // OnGUI method; for drawing a crosshair
    private void OnGUI()
    {
        int size = 24;

        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "+");

        // Draw a buttonremoveeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
        //if (GUI.Button(new Rect(10, 10, 180, 20), "Click here for a free ipod!"))
        //{
         //   Debug.Log("Button has been clicked!");
        //}

    }

    // Coroutine
    // Place down a sphere at a location, which then disappears after one second
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        // Create a new sphere game object
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Place sphere at pos passed in\
        sphere.transform.position = pos;

        // wait one second
        yield return new WaitForSeconds(1);

        // destroy the sphere
        Destroy(sphere);
    }

    // Update is called once per frame
    void Update()
    {
        // If this component is toggled off, then return immediately
        if (!allowedToShoot) return;

        // When the player left-clicks, perform a raycast
        if (Input.GetMouseButtonDown(0) && ammoLeft > 0 && !EventSystem.current.IsPointerOverGameObject())
        {

            // Consume 1 unit of ammo
            ammoLeft--;
            ammoText.text = $"Ammo left: {ammoLeft}";

            // Calculate the center of the screen
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            // Create a ray whose starting point is the middle of the screen
            Ray ray = cam.ScreenPointToRay(point);

            //Create a raycast object to figure out what was hit
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // temporary, print out the coords of where the ray hits
                Debug.Log("Hit: " + hit.point);

                // If the object hit was a reactive target, say that it was hit
                // Otherwise, place down a sphere
                GameObject hitobject = hit.transform.gameObject;
                ReactiveTarget target = hitobject.GetComponentInParent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();

                    Debug.Log("Target hit!");
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }

        }
        else if (Input.GetMouseButtonDown(0) && ammoLeft == 0)
        {
            // Reloading code goes here

            isReloading = true;
            reloadingTimeLeft = 1f;
            ammoText.text = $"Reloading ... ";
        }

        // If reloading, decrement time left
        // If that time reaches zero, exit reloading state and add more ammo
        if (isReloading)
        {
            reloadingTimeLeft -= Time.deltaTime;

            if (reloadingTimeLeft <= 0.0f)
            {
                reloadingTimeLeft = 0f;
                isReloading = false;
                ammoLeft = 5;
                ammoText.text = $"Ammo left: {ammoLeft}";
            }
        }
    }
}