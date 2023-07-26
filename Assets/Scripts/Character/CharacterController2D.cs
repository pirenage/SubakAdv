
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{

    private float originalSpeed;
    private bool isSpeedReduced = false;
    Rigidbody2D rb2D;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    public Vector2 lastmotionVector;
    Animator anima;
    public bool moving;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashDistance = 5f;
    private bool canDash = true;

    // Parameter Animation
    public string XInput = "Horizontal";
    public string YInput = "Vertical";
    public string X = "X";
    public string Y = "Y";
    public string IsMove = "IsMoving";
    public string LastPosition = "LastHori";
    public string LastPositionY = "LastVerti";
    // End parameter

    private void Awake()
    {

        rb2D = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        originalSpeed = speed;

    }
    private void Update()
    {
        float Horizontal = Input.GetAxisRaw(XInput);
        float Vertical = Input.GetAxisRaw(YInput);
        motionVector = new Vector2(Horizontal, Vertical);
        anima.SetFloat(X, Horizontal);
        anima.SetFloat(Y, Vertical);

        moving = Horizontal != 0 || Vertical != 0;
        anima.SetBool("moving", moving);
        if (Horizontal != 0 || Vertical != 0)
        {
            lastmotionVector = new Vector2(Horizontal, Vertical).normalized;
            anima.SetFloat(LastPosition, Horizontal);
            anima.SetFloat(LastPositionY, Vertical);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isSpeedReduced)
            {
                speed /= 4;
                isSpeedReduced = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isSpeedReduced)
            {
                speed = originalSpeed;
                isSpeedReduced = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Dash();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb2D.velocity = motionVector * speed;
    }

    private void Dash()
    {
        rb2D.velocity = Vector2.zero;

        Vector2 dashDirection = motionVector.normalized;

        Vector2 dashPosition = rb2D.position + dashDirection * dashDistance;

        rb2D.MovePosition(dashPosition);

        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;

        // Menunggu sejumlah waktu sesuai dengan cooldown
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }


}
