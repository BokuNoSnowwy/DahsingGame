using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 0.1f;
    public float gravity = 2;
    public float dashDistanceMax = 6;
    private float dashDistance;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public float wallSlideTimerBeforeFall;
    public float resetWallSlideTimerBeforeFall = 10f;
    public bool isDashing;
    public bool noGravity;

    [Space]
    public bool groundTouch;
    public bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    
    [Header("Unity Listener")]
    public UnityEvent dashEvent;

    [Header("Axis")] 
    protected float x;
    protected float y;
    protected float xRaw;
    protected float yRaw;

    [Header("Raycast")]
    int layerMaskDash;
    [SerializeField] private GameObject rightRay, leftRay;

    // Start is called before the first frame update
    protected void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        dashDistance = dashDistanceMax;
        layerMaskDash = LayerMask.GetMask("Ground"); 
    }

    // Update is called once per frame
    protected void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, rb.velocity.y);
        
        

        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if(side != coll.wallSide)
                anim.Flip(side*-1);
            wallGrab = true;
            wallSlideTimerBeforeFall -= Time.deltaTime;
            if (wallSlideTimerBeforeFall <= 0)
            {
                wallGrab = false;
            }
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        RaycastHit2D leftHit = Physics2D.Raycast(leftRay.transform.position, Vector2.down, 1f, layerMaskDash);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRay.transform.position, Vector2.down, 1f, layerMaskDash);

        Debug.DrawRay(leftRay.transform.position, Vector2.down, Color.red, 2f);
        Debug.DrawRay(rightRay.transform.position, Vector2.down, Color.red, 2f);

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }
        
        if (wallGrab && !isDashing)
        {
            if(x > .2f || x < -.2f)
             rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }

        DashMovement();
        
        
        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }


        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if ((leftHit.collider != null || rightHit.collider != null) && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(leftHit.collider == null && rightHit.collider == null && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if(x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }
    }

    public virtual void DashMovement()
    {
        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
            
            dashEvent.Invoke();
        }
    }

    protected void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        
        //Reset le timer
        wallSlideTimerBeforeFall = resetWallSlideTimerBeforeFall;
        
        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
        EndDash();
    }

    Vector3 start = Vector3.zero;
    float startTime = 0;
    protected void Dash(float x, float y)
    {
        rb.gravityScale = 0;

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;

        anim.SetTrigger("dash");

        Vector2 dir = new Vector2(x, y);

        rb.velocity = dir * dashSpeed;
        hasDashed = true;
        noGravity = false;

        start = transform.position;
        startTime = Time.realtimeSinceStartup;

        //StartCoroutine(DashWait());
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        //Raycast
        RaycastHit2D hit = Physics2D.Raycast(start, dir, dashDistanceMax, layerMaskDash);
        if (hit.collider != null)
        {
            Debug.Log(hit.distance);
            dashDistance = hit.distance - 0.5f;

            if (hit.distance < 0.6f)
            {
                Debug.Log("salut");
                EndDash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            if (Vector3.Distance(start, transform.position) > dashDistance )
            {
                EndDash();
            }
        }
    }

    private void EndDash()
    {
        rb.velocity = Vector2.zero;
        dashParticle.Stop();
        if (noGravity)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
        }
        dashDistance = dashDistanceMax;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    protected IEnumerator DashWait()
    {

        float t = Time.realtimeSinceStartup;

        yield return new WaitForSeconds(.3f);

        Debug.Log(Time.realtimeSinceStartup - t);
        
    }

    protected IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if(coll.wallSide != side)
         anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        
        rb.velocity = new Vector2(push, -slideSpeed);
    }

    protected void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    protected void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    protected IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    protected void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            interactable.DetectPlayer(this);
        }
        else if (collision.transform.parent.TryGetComponent(out IInteractable interactableParent))
        {
            interactableParent.DetectPlayer(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EndDash();
    }
}
