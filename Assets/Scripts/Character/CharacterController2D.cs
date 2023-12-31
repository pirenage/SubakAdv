
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
    private bool isWalkingSoundPlaying = false;
    [SerializeField] AudioClip Jalan;
    [SerializeField] AudioClip dash;
    [SerializeField] AudioClip sneak;

    // Parameter Animation
    private string XInput = "Horizontal";
    private string YInput = "Vertical";
    private string X = "X";
    private string Y = "Y";
    private string IsMove = "moving";
    private string LastPosition = "LastHori";
    private string LastPositionY = "LastVerti";
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
        anima.SetBool(IsMove, moving);
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
                // AudioManager.instance.Play(sneak);
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

            // AudioManager.instance.Play(sneak);
        }
        // if (moving && !isWalkingSoundPlaying)
        // {

        //     // AudioManager.instance.Play(Jalan);
        //     isWalkingSoundPlaying = true;
        // }
        // else if (!moving)
        // {
        //     isWalkingSoundPlaying = false;
        // }
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
