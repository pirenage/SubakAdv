using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    Rigidbody2D rb2D;

    [SerializeField] float speed = 2f;
    private float originalSpeed;

    private bool isSpeedHalved = false;
    Vector2 motionVector;
    public Vector2 lastmotionVector;

    Animator anima;
    // Parameter Animation
    private string XInput = "Horizontal";
    private string YInput = "Vertical";
    private string X = "X";
    private string Y = "Y";
    private string IsMove = "IsMoving";
    private string LastPosition = "LastHori";
    private string LastPositionY = "LastVerti";
    // End parameter

    public bool moving;


    // Dashing
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashDistance = 5f;
    private bool canDash = true;
    // End Dashing

    private void Awake()
    {
        originalSpeed = speed;
        rb2D = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }

    private void Update()
    {
        float Horizontal = Input.GetAxisRaw(XInput);
        float Vertical = Input.GetAxisRaw(YInput);

        motionVector = new Vector2(Horizontal, Vertical);

        anima.SetFloat(X, Horizontal);
        anima.SetFloat(Y, Vertical);

        moving = Horizontal != 0 || Vertical != 0;
        anima.SetBool(IsMove, moving);

        if (Horizontal != 0 || Vertical != 0)
        {
            lastmotionVector = new Vector2(Horizontal, Vertical).normalized;
            anima.SetFloat(LastPosition, Horizontal);
            anima.SetFloat(LastPositionY, Vertical);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSpeedHalved = true;
            speed /= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isSpeedHalved)
            {
                speed = originalSpeed;
                isSpeedHalved = false;
            }
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

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
