using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BasePlayer : Entity, InputSystem_Actions.IPlayerActions, IMovable
{
	// -- COMPONENTS
	private InputSystem_Actions inputActions;
	public PlayerData data;
	public GroundDetector groundDetector;
	private SpriteRenderer sprite;
	public NPCController foundNPC;
	

	// -- VARIABLES
	public List<LevelCollider> InSceneColliders { get; set; } = new List<LevelCollider>();
	public Vector2 MoveValue { get; set; }
	private bool isGravityUpside = false;
	public bool CanPlay { get; set; } = true;
	public bool IsTurning { get; set; } = false;
    private bool isPaused = false;
    [SerializeField] private float GravityMult = 1;
	[SerializeField] private float dieTime = 2;
	[SerializeField] private float TurnAroundTime = 0.5f;
	[SerializeField] private float gravityChangeImpulse = 5f;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }

    protected override void Start()
	{
		base.Start();
		groundDetector = GetComponent<GroundDetector>();
		sprite = GetComponent<SpriteRenderer>();
		ResetCharacter();
	}

	private void OnEnable() 
	{
		inputActions.Player.Enable();
        GameEvents.OnPlayerWon += DisablePlay;
    }
	private void OnDisable() 
	{
		inputActions.Player.Disable();
        GameEvents.OnPlayerWon -= DisablePlay;
    }

    private void DisablePlay() => CanPlay = false;
    void FixedUpdate()
	{
		if (!CanPlay) return;
		groundDetector.DetectGround();
		if (groundDetector.IsGrounded) IsTurning = false;
        CalculateMoveSpeed(MoveValue);
    }

	protected override IEnumerator DyingRoutine()
	{
		CanPlay = false;
        GameEvents.PlayerDied();
        yield return new WaitForSeconds(dieTime);
		ResetCharacter();
	}

	protected virtual void ResetCharacter()
	{
		GameEvents.PlayerRespawned();
        IsTurning = false;
        CanPlay = true;
		Lives = 1;
		transform.position = LevelManager.Instance.FirstSpawn.position;
		DeathState = EDeathState.Alive;
		gameObject.layer = LayerMask.NameToLayer(lifeMaskHash);
		groundDetector.groundChecker.localPosition = new Vector3(0, -Mathf.Abs(groundDetector.groundChecker.localPosition.y), 0);
		sprite.flipY = false;
        IsStunned = false;
		isGravityUpside = false;
		rb.linearVelocity = Vector3.zero;
		rb.gravityScale = GravityMult;
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
		
		IsTurning = true;
		rb.linearVelocityY = 0;
        isGravityUpside = !isGravityUpside;
        rb.gravityScale = isGravityUpside ? -GravityMult : GravityMult;
		Vector2 gravityDir = isGravityUpside ? Vector2.up : Vector2.down;

        rb.AddForce(gravityDir * gravityChangeImpulse * GravityMult, ForceMode2D.Impulse);
		groundDetector.groundChecker.localPosition = new Vector3(0, -groundDetector.groundChecker.localPosition.y, 0);
	}


    // --INPUTS
    public void OnMove(InputAction.CallbackContext context)
	{
		MoveValue = context.ReadValue<Vector2>();
	}
	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (foundNPC != null)
			{
                foundNPC.Interact();
			}
		}
	}
	public void OnTurnGravity(InputAction.CallbackContext context)
	{
		if (context.performed && groundDetector.IsGrounded && CanPlay)
		{
			TurnGravity();
		}
	}

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPaused = !isPaused;
            GameEvents.ShowPauseMenu(isPaused);
        }
    }

}
