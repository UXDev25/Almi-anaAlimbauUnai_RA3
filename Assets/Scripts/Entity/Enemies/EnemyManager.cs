using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyManager : Entity, IDeadly
{
	
	protected bool isFacingRight = true;
	protected RaycastHit2D raycastHit;
	protected float dieTime = 2f;
	[SerializeField] float enemySize = 1;
	[SerializeField] private int damage = 1;
	[SerializeField] private float knockBackPow = 10;

	protected void FlipDirection(bool enemyCondition)
	{
		transform.localScale = enemyCondition ? new Vector3(enemySize, enemySize, 1) : new Vector3(-enemySize, enemySize, 1);
	}

    protected override IEnumerator DyingRoutine()
    {
		yield return new WaitForSeconds(dieTime);
		Destroy(this);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		DetectAndDamagePlayer(collision);
	}

	public void DetectAndDamagePlayer(Collision2D collision) 
	{
		BasePlayer player = collision.gameObject.GetComponent<BasePlayer>();
		if (collision.gameObject.CompareTag("Player") && player != null)
		{
			player.TakeDamage(damage, knockBackPow, collision.GetContact(0).point);
		}
	}
}