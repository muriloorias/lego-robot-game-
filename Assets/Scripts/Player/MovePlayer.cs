using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 5f;
    public bool grounded = true;
    public Rigidbody rb;
    private Animator anim;
    public float speedTurn = 300f;

    private bool jumpRequest;
    public Transform cam; // arrasta a câmera aqui

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void run()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // direção baseada na câmera
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(cam.right, new Vector3(1, 0, 1)).normalized;

        Vector3 direction = (camForward * vertical + camRight * horizontal).normalized;

        if (direction.magnitude > 0.1f)
        {
            // calcula ângulo relativo à câmera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // gira suavemente pra direção
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speedTurn * Time.deltaTime);

            // move
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

            anim.SetBool("runningController", true);
        }
        else
        {
            anim.SetBool("runningController", false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
            jumpRequest = false;
        }

        run();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
