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
		Debug.Log($"near edge: {IsNearEdge()}, raycast: {raycastHit}, isgrounded {groundDetector.IsGrounded}, isStunned{IsStunned}");
		if (IsStunned) return;
		if (Lives <= 0) Die();
		FlipDirection(isFacingRight);
		groundDetector.DetectGround();
		if (!IsStunned) rb.linearVelocityX = maxWalkSpeed * transform.localScale.x;
		raycastHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallRayCastSize, groundAndPlayerMask);
		if ((raycastHit.collider.CompareTag("Ground") || IsNearEdge()) && groundDetector.IsGrounded && !IsStunned)
		{
			isFacingRight = !isFacingRight;
		}
	}



	private bool IsNearEdge()
	{
		Vector3 targetPos = edgeChecker.position;
		targetPos.y -= groundDetector.detectionRadius;

		Debug.DrawLine(edgeChecker.position, targetPos, Color.green);

		return !Physics2D.Linecast(edgeChecker.position, targetPos, groundDetector.groundMask);
	}
}