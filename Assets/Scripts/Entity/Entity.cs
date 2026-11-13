using System;
using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	// --VARIABLES
	public bool IsStunned { get; set; }
	public virtual int Lives { get; protected set; }
	public virtual EDeathState DeathState { get; protected set; }
	protected readonly float defKnockBack = 5;
	protected readonly string deathMaskHash = "Death";
	protected readonly string lifeMaskHash = "Default";

	// -- COMPONENTS
	public Rigidbody2D rb { get; protected set; }

	

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Die()
	{
		IsStunned = true;
		gameObject.layer = LayerMask.NameToLayer(deathMaskHash);
		DeathState = EDeathState.Dying;
		StartCoroutine(DyingRoutine());
	}

	public virtual void TakeDamage(int damage, float knockBackPower, Vector2 enemyPos) 
	{
		Stun(knockBackPower, enemyPos);
		Lives -= damage;
		if (Lives <= 0) Die();
	}

	protected virtual void Stun(float knockBackPower, Vector2 enemyPos) 
	{
		if (IsStunned) return;
		rb.linearVelocity = Vector2.zero;
		Vector2 direction = enemyPos - (Vector2)transform.position;
		float pseudoDirection = -Mathf.Sign(direction.x);
		rb.AddForce(new Vector2(knockBackPower * pseudoDirection, knockBackPower * rb.gravityScale), ForceMode2D.Impulse);
		IsStunned = true;
	}

	protected abstract IEnumerator DyingRoutine();
}
