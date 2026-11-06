using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JohnnyManager : EnemyManager
{
	//Components
	private GroundDetector groundDetector;
	private Transform _edgeChecker;

	// --VARIABLES
	[SerializeField] private float maxWalkSpeed = 2f;
	[SerializeField] private float wallRayCastSize = 0.6f;
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
		FlipDirection(isFacingRight);
		if (!IsStunned) rb.linearVelocityX = maxWalkSpeed * transform.localScale.x;
		raycastHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallRayCastSize, groundAndPlayerMask);
		if ((raycastHit.collider != null || IsNearEdge()) && groundDetector.IsGrounded && !IsStunned)
		{
			isFacingRight = !isFacingRight;
		}
	}



	private bool IsNearEdge()
	{
		Vector3 targetPos = _edgeChecker.position;
		targetPos.y -= groundDetector.detectionRadius;

		Debug.DrawLine(_edgeChecker.position, targetPos, Color.green);

		return !Physics2D.Linecast(_edgeChecker.position, targetPos, groundDetector.groundMask);
	}
}