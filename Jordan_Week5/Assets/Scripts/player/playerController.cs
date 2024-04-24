using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpHeight;
    public float maxJumps = 1f;
    public float gravityMultiplier = 1f;
    public float rotationSpeed;

    [Header("Projectiles")]
    public Transform eyes;
    public GameObject[] projectiles;

    [Header("Camera")]
    public Transform cam;
    public Transform focalPoint;
    

    [Header("Spawn Points")]
    public float xRange;
    public float zRange;

    [SerializeField]
    bool hasPowerup = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // set physics gravity multiplier
        Physics.gravity *= gravityMultiplier;

        // set cursor to locked and not visible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // gets forward of camera (z Axis)
        Vector3 cameraForward = Camera.main.transform.forward;
        //keeps Y the same
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // gets right of the camera (x axis)
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        // Get Inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // adds vector3 from camera together
        Vector3 moveDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        // Add force to inputs
        rb.AddForce(moveDirection * moveSpeed);

        focalPoint.position = new Vector3(transform.position.x, focalPoint.position.y, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }

        
       
        if (moveDirection != Vector3.zero) 
        {
            // Gets target look rotation
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            // Rotates the player towards target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //set speed_f float to triggert walk and idle animations
        if (moveDirection.magnitude > 0.1f)
        {
            anim.SetFloat("speed_f", 0.2f);
        }
        if (moveDirection.magnitude < 0.1f)
        {
            anim.SetFloat("speed_f", 0f);
        }

        

            // Resets player if they are out of bounds
            if (transform.position.y < -38f)
        { 
            // Sets random spawn point
            Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), 1, (Random.Range(-zRange, zRange)));

            transform.SetPositionAndRotation(spawnPosition, transform.rotation);
            rb.velocity = Vector3.zero;
            
        }

        // Sets jump input and can only jump == maxJumps
        if (Input.GetKeyDown(KeyCode.Space) && maxJumps > 0)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            maxJumps--;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Resets maxJumps on collision with ground
        if (collision.transform.tag == "Ground")
        {
            maxJumps = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUps"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
        }
    }

    void ShootProjectile()
    {
        
        int projectileIndex = Random.Range(0, projectiles.Length);
        Instantiate(projectiles[projectileIndex], eyes.position + transform.forward, transform.rotation);
        
    }


}
