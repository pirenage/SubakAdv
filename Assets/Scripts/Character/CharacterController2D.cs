
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField]float speed = 2f;
    Vector2 motionVector;
    public Vector2 lastmotionVector;
    Animator anima;
    public bool moving;

    private void Awake()
    {

        rb2D = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();

    }
    private void Update() 
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        motionVector = new Vector2(Horizontal, Vertical);
        anima.SetFloat("MoveX", Horizontal);
        anima.SetFloat("MoveY", Vertical);

        moving =Horizontal != 0 || Vertical!=0;
        anima.SetBool("moving", moving);
        if(Horizontal != 0 || Vertical!=0)
        {
            lastmotionVector = new Vector2(Horizontal,Vertical).normalized;
            anima.SetFloat("LastHori",Horizontal);
            anima.SetFloat("LastVertical",Vertical);
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

}
