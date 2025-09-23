using UnityEngine;

//classe da movimentação do player
public class MovePlayer : MonoBehaviour
{
    //variaveis do projeto
    public float speed = 8f;
    public float jumpForce = 5f;
    public bool grounded = true;
    public Rigidbody rb;
    private Animator anim;
    public float speedTurn = 300f;

    private bool jumpRequest;
    public Transform cam;
    //inicio
    private void Start()
    {
        //pegando componentes
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void run()
    {
        //horizontal e vertical do personagem
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //camera
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(cam.right, new Vector3(1, 0, 1)).normalized;

        Vector3 direction = (camForward * vertical + camRight * horizontal).normalized;

        //if para decidir a pposição do personagem
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speedTurn * Time.deltaTime);

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

    //essa função sera utilizada apenas para a configração do controle
    public void activeJump(bool jumpTrue)
    {
        jumpRequest = jumpTrue;
    }
}
