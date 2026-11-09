using System.Collections;
using UnityEngine;

public class JosephineController : EnemyManager
{
	
	[SerializeField] private float despawnTime = 2f;

	private BulletPool parentPool;
	private Rigidbody2D bulletRb;

	private void Awake()
	{
		bulletRb = GetComponent<Rigidbody2D>();
	}

	public void SetPool(BulletPool pool) => parentPool = pool;

	public void Init(Vector2 direction, float shootForce)
	{
		bulletRb.linearVelocity = Vector2.zero;
		bulletRb.angularVelocity = 0f;
		bulletRb.AddForce(direction * shootForce, ForceMode2D.Impulse);
		StartCoroutine(DespawnRoutine());
	}

	private IEnumerator DespawnRoutine()
	{
		yield return new WaitForSeconds(despawnTime);
		parentPool.ReturnBullet(this);
	}
}