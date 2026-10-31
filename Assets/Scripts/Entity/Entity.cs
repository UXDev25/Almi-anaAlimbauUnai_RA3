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
	private readonly string deathMaskHash = "Death";

	// -- COMPONENTS
	protected Rigidbody2D rb;

	

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

	protected virtual void TakeDamage(int damage, float knockBackPower, Vector2 enemyPos) 
	{
		Stun(knockBackPower, enemyPos);
		Lives -= damage;
		if (Lives <= 0) Die();
	}

	protected virtual void Stun(float knockBackPower, Vector2 enemyPos) 
	{
		if (IsStunned) return;
		Vector2 direction = enemyPos - (Vector2)transform.position;
		float pseudoDirection = -Mathf.Sign(direction.x);
		rb.AddForce(new Vector2(knockBackPower * pseudoDirection, knockBackPower), ForceMode2D.Impulse);
		IsStunned = true;
	}

	protected abstract IEnumerator DyingRoutine();
}
