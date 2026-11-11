using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BasePlayer : Entity, InputSystem_Actions.IPlayerActions, IMovable
{
	// -- COMPONENTS
	private InputSystem_Actions inputActions;
	public PlayerData data;
	private GroundDetector groundDetector;
	private SpriteRenderer sprite;
	

	// -- VARIABLES
	public List<LevelCollider> InSceneColliders { get; set; } = new List<LevelCollider>();
	Vector2 MoveValue;
	private bool isGravityUpside = false;
	private bool canPlay = true;
	private bool isPaused = false;
    [SerializeField] private float GravityMult = 1;
	[SerializeField] private float dieTime = 2;
	[SerializeField] private float TurnAroundTime = 0.5f;
	[SerializeField] private float gravityChangeImpulse = 5f;

	protected override void Start()
	{
		base.Start();
		groundDetector = GetComponent<GroundDetector>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		ResetCharacter();
	}

	void Awake()
	{
		inputActions = new InputSystem_Actions();
		inputActions.Player.SetCallbacks(this);
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

    private void DisablePlay() => canPlay = false;
    void FixedUpdate()
	{
		if (!canPlay) return;
		groundDetector.DetectGround();
		CalculateMoveSpeed(MoveValue);
		Debug.Log($"colliders player is touching: {InSceneColliders.Count}");
	}

	protected override IEnumerator DyingRoutine()
	{
		canPlay = false;
		yield return new WaitForSeconds(dieTime);
		GameEvents.PlayerDied();
		ResetCharacter();
	}

	protected virtual void ResetCharacter()
	{
		canPlay = true;
		Lives = 1;
		transform.position = LevelManager.Instance.FirstSpawn.position;
		DeathState = EDeathState.Alive;
		gameObject.layer = LayerMask.NameToLayer(lifeMaskHash);
		groundDetector.groundChecker.localPosition = new Vector3(0, -Mathf.Abs(groundDetector.groundChecker.localPosition.y), 0);
		sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
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
	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Lógica de interacción
		}
	}
	public void OnTurnGravity(InputAction.CallbackContext context)
	{
		if (context.performed && groundDetector.IsGrounded)
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
