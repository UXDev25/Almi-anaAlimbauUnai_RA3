using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayer : Entity, InputSystem_Actions.IPlayerActions, IMovable
{
	// -- COMPONENTS
	private InputSystem_Actions inputActions;
	public PlayerData data;
	private GroundDetector groundDetector;
	private SpriteRenderer sprite;
	[SerializeField] private float TurnAroundTime = 0.5f;
    [SerializeField] private float gravityChangeImpulse = 5f;


    // -- VARIABLES
    Vector2 MoveValue;
	private bool isGravityUpside = false;
	[SerializeField] private float GravityMult = 1;

	protected override void Start() 
	{ 
		base.Start();
		isGravityUpside=false;
		groundDetector = GetComponent<GroundDetector>();
		sprite = GetComponentInChildren<SpriteRenderer>();
    }

	void Awake()
    {
		inputActions = new InputSystem_Actions();
		inputActions.Player.SetCallbacks(this);
	}

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    // Update is called once per frame
    void FixedUpdate()
    {
        groundDetector.DetectGround();
        CalculateMoveSpeed(MoveValue);
    }

    protected override IEnumerator DyingRoutine()
    {
        throw new System.NotImplementedException("Cannot Die Yet");
    }

	public void CalculateMoveSpeed(Vector2 moveValue) 
	{
		float targetSpeed = MoveValue.x * data.maxSpeed;
		float speedDif = targetSpeed - rb.linearVelocity.x;
		float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.acceleration : data.deceleration;
		float movement = speedDif * accelRate;
		rb.AddForce(movement * Vector2.right.normalized);
	}

	private void TurnGravity() 
	{
		rb.linearVelocityY = 0;
        isGravityUpside = !isGravityUpside;
        rb.gravityScale = isGravityUpside ? -GravityMult : GravityMult;
		Vector2 gravityDir = isGravityUpside ? Vector2.up : Vector2.down;

        rb.AddForce(gravityDir * gravityChangeImpulse * GravityMult, ForceMode2D.Impulse);
		groundDetector.groundChecker.localPosition = new Vector3(0, -groundDetector.groundChecker.localPosition.y, 0);
        StartCoroutine(TurnCharacterUpside());
	}

    private IEnumerator TurnCharacterUpside() // -- Coroutine to smoothly rotate the character sprite when gravity is flipped
    {
        float startAngle = sprite.transform.eulerAngles.z;
        float targetAngle = isGravityUpside ? 180f : 0f;
        float elapsed = 0f;

        while (elapsed < TurnAroundTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / TurnAroundTime);
            float angle = Mathf.LerpAngle(startAngle, targetAngle, t);
            sprite.transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null; 
        }

        sprite.transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }


    // --INPUTS
    public void OnMove(InputAction.CallbackContext context)
	{
		MoveValue = context.ReadValue<Vector2>();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Lógica de ataque
		}
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Lógica de interacción
		}
	}

	public void OnCrouch(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Lógica de agacharse
		}
	}

	public void OnTurnGravity(InputAction.CallbackContext context)
	{
		if (context.performed && groundDetector.IsGrounded)
		{
			TurnGravity();
		}
	}
}
