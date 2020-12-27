using UnityEngine;

public class BirdController : MonoBehaviour
{
    private float flapSpeed = 1000f;
    private float forwardSpeed = 10f;
    private float maxFlapSpeed = 2f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool didFlap;
    private bool isDead;

    public void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
    }

    // read input, change graphics
    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && !this.isDead)
        {
            this.didFlap = true;
        }
    }

    // apply physics
    public void FixedUpdate()
    {
        var velocity = this.rb.velocity;
        velocity.x = this.forwardSpeed;
        this.rb.velocity = velocity;

        if (didFlap)
        {
            didFlap = false;
            this.rb.AddForce(new Vector2(0, flapSpeed));

            var updatedVelocity = this.rb.velocity;
            if (updatedVelocity.y > this.maxFlapSpeed)
            {
                updatedVelocity.y = this.maxFlapSpeed;
                this.rb.velocity = updatedVelocity;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Floor"))
        {
            this.isDead = true;
            this.animator.SetBool("BirdDead", true);
            this.forwardSpeed = 0;
        }
    }

}
