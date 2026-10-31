using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
	private Entity groundUser;
	[SerializeField] private float detectionRadius;
	[SerializeField] private LayerMask groundMask;
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
}
