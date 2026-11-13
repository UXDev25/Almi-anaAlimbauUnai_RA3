using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JohnnyManager : EnemyManager
{
	//Components
	private GroundDetector groundDetector;
	[SerializeField] private Transform edgeChecker;

	// --VARIABLES
	[SerializeField] private float wallRayCastSize = 0.6f;
	[SerializeField] private float maxWalkSpeed = 2f;
	[SerializeField] private float GroundRayCastSize = 0.6f;
	[SerializeField] private LayerMask groundAndPlayerMask;

    protected override void Start()
	{
		base.Start();
		Lives = 1;
		groundDetector = GetComponent<GroundDetector>();
	}

	private void FixedUpdate()
	{
		if (IsStunned) return;
		if (Lives <= 0) Die();

		groundDetector.DetectGround();

		Vector2 dir = Vector2.right * transform.localScale.x;
		raycastHit = Physics2D.Raycast(transform.position, dir, wallRayCastSize, groundAndPlayerMask);
		Debug.DrawRay(transform.position, dir * wallRayCastSize, Color.red);

		bool hitWall = raycastHit.collider != null && (raycastHit.collider.CompareTag("Ground") || raycastHit.collider.CompareTag("Death"));

		if (hitWall || (IsNearEdge() && groundDetector.IsGrounded))
		{
			isFacingRight = !isFacingRight;
		}

		FlipDirection(isFacingRight);
		rb.linearVelocityX = maxWalkSpeed * transform.localScale.x;
	}



	private bool IsNearEdge()
	{
		Vector3 targetPos = edgeChecker.position;
		targetPos.y -= groundDetector.detectionRadius;

		Debug.DrawLine(edgeChecker.position, targetPos, Color.green);

		return !Physics2D.Linecast(edgeChecker.position, targetPos, groundDetector.groundMask);
	}
}