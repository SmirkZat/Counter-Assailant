using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance;

    [SerializeField] float movementSpeed = 10f;
    [SerializeField] Collider2D confiner;
    [SerializeField] ParticleSystem healVFX;
    [SerializeField] ParticleSystem dustParticles;

    readonly int RUNNING_HASH = Animator.StringToHash("Running");
    readonly int IDLE_ANIMATION = Animator.StringToHash("Idle");

    float boundsPadding = .5f;

    Vector2 movement;
    Rigidbody2D rb;
    Animator animator;
    Health health;

    Bounds movementBounds;
    InventoryManager inventory;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        movementBounds = confiner.bounds;
    }

    void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    void Update()
    {
        if (IsControlsEnabled())
        {
            UpdateMovementInput();
            ClampPosition();
            HandleSpriteFlip();
            HandleActionInputs();
        }
    }

    public bool IsControlsEnabled()
    {
        return GameManager.Instance.State == GameManager.GameState.Playing;
    }

    void OnEnable()
    {
        LevelManager.OnLevelCompleted += DisablePlayerMovement;
        Heart.OnHeartCollected += HandleHealthPickup;
    }

    void OnDisable()
    {
        LevelManager.OnLevelCompleted -= DisablePlayerMovement;
        Heart.OnHeartCollected -= HandleHealthPickup;
    }

    void DisablePlayerMovement()
    {
        SetIdleState();
    }

    void SetIdleState()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero; // reset player position
        movement = Vector2.zero;
        animator.Play(IDLE_ANIMATION);
    }

    void UpdateMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void HandleActionInputs()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventory.InventoryItems.ContainsKey("medkit"))
        {
            // TODO add particles VFX for health restoration and sound effect
            MedKitSO medKitSO = inventory.InventoryItems["medkit"] as MedKitSO;
            if (medKitSO.amount > 0)
            {
                PlayHealVFX();
                health.AddHealth(medKitSO.healthRestoration);
                inventory.DecreaseItemAmount(medKitSO);
            }
        }
    }

    void FixedUpdate()
    {
        if (!health.isDead)
        {
            HandleMovementAnimation();
            HandleMovement();
        }
    }

    void PlayHealVFX()
    {
        Instantiate(healVFX, transform.position, Quaternion.identity);
    }

    void HandleHealthPickup(Heart sender)
    {
        PlayHealVFX();
        health.AddHealth(sender.HealthRestoration);
    }

    void HandleMovement()
    {
        rb.velocity = movement * movementSpeed;
    }

    void ClampPosition()
    {
        float clampX = Mathf.Clamp(transform.position.x,
        movementBounds.min.x + boundsPadding,
        movementBounds.max.x - boundsPadding);

        float clampY = Mathf.Clamp(transform.position.y,
        movementBounds.min.y + boundsPadding,
        movementBounds.max.y - boundsPadding);

        transform.position = new Vector2(clampX, clampY);
    }

    void HandleSpriteFlip()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180f, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void HandleMovementAnimation()
    {
        if (movement != Vector2.zero)
        {
            animator.Play(RUNNING_HASH);
            if (!dustParticles.isPlaying)
            {
                dustParticles.Play();
            }
        }
        else
        {
            animator.Play(IDLE_ANIMATION);
            if (dustParticles.isPlaying)
            {
                dustParticles.Stop();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        GetComponent<Flash>()?.StartFlash();
        GetComponent<CinemachineImpulseSource>()?.GenerateImpulse();
        health?.TakeDamage((int)damage);
    }
}
