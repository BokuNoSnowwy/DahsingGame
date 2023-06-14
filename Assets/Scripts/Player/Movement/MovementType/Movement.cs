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
    public float dashSpeed = 0.1f;
    public float gravity = 2;
    public float dashDistanceMax = 6;
    protected float dashDistance;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool isDashing;
    public bool noGravity;
    public bool hasDashed;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    [Header("Axis")]
    protected float x;
    protected float y;
    protected float xRaw;
    protected float yRaw;

    [Header("Unity Listener")]
    public UnityEvent dashEvent;

    [Header("Raycast")]
    int layerMaskGround;
    int layerMaskWall;
    [SerializeField] private GameObject rightRay, leftRay;

    // Start is called before the first frame update
    protected void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        dashDistance = dashDistanceMax;
        layerMaskGround = LayerMask.GetMask("Ground");
        layerMaskWall = LayerMask.GetMask("Wall");

    }

    // Update is called once per frame
    protected void Update()
    {
        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if (coll.onGround && !isDashing)
        {
            GetComponent<BetterJumping>().enabled = true;
        }

        if (!isDashing && hasDashed)
        {
            RaycastHit2D leftHit = Physics2D.Raycast(leftRay.transform.position, Vector2.down, 1f, layerMaskGround);
            RaycastHit2D rightHit = Physics2D.Raycast(rightRay.transform.position, Vector2.down, 1f, layerMaskGround);

            Debug.DrawRay(leftRay.transform.position, Vector2.down, Color.red, 2f);
            Debug.DrawRay(rightRay.transform.position, Vector2.down, Color.red, 2f);

            if (leftHit.collider != null || rightHit.collider != null)
            {
                hasDashed = false;
                coll.onGround = true;
            }
        }
    }

    Vector3 start = Vector3.zero;
    float startTime = 0;
    protected void Dash(float x, float y)
    {
        rb.isKinematic = false;
        rb.gravityScale = 0;
        GetComponent<Collider2D>().isTrigger = true;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        anim.SetTrigger("dash");

        Vector2 dir = new Vector2(x, y);

        rb.velocity = dir * dashSpeed;
        hasDashed = true;
        noGravity = false;

        start = transform.position;
        startTime = Time.realtimeSinceStartup;

        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        GetComponent<BetterJumping>().enabled = false;
        isDashing = true;

        //Raycast
        if(dashDistance >= dashDistanceMax)
        {
            
        }

        RaycastHit2D hit = Physics2D.Raycast(start, dir, dashDistanceMax + 2, layerMaskWall);
        Debug.DrawRay(start, dir, Color.red, 2f);
        if (hit.collider != null)
        {
            if(hit.distance < dashDistance)
            {
                Debug.Log("small");
                dashDistance = hit.distance - 0.5f;

                if (hit.distance < 0.8f)
                {
                    Debug.Log("small2");
                    EndDash();
                }
            }
            else
                Debug.Log("large");
        }
        else
            Debug.Log("large2");
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

    public void EndDash()
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
        isDashing = false;
        GetComponent<Collider2D>().isTrigger = false;
    }

    protected IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
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
        if (collision.gameObject.layer == 3)
        {
            hasDashed = false;
            coll.onGround = true;
        }

        
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("mur");
            EndDash();
        }     
    }
}
