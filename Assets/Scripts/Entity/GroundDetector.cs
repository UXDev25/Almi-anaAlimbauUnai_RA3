using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
	private Entity groundUser;
	public float detectionRadius;
	public LayerMask groundMask;
	public bool IsGrounded { get; protected set; }
	public Transform groundChecker;

	private void Start()
	{
		groundUser = GetComponent<Entity>();
	}

    public virtual void DetectGround()
	{
		IsGrounded = Physics2D.OverlapCircle(groundChecker.position, detectionRadius, groundMask);
		if (groundUser.IsStunned && IsGrounded)
		{
            Debug.Log(gameObject.name + " grounded && stunned");
            groundUser.IsStunned = false;
        }
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundChecker.position, detectionRadius);

	}
}
