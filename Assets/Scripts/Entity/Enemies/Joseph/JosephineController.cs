using System.Collections;
using UnityEngine;

public class JosephineController : EnemyManager
{
	
	[SerializeField] private float despawnTime = 2f;
    [SerializeField] private float despawnWallTime = 1f;
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
		StartCoroutine(DespawnRoutine(despawnTime));
	}

	private IEnumerator DespawnRoutine(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
        rb.simulated = true;
        parentPool.ReturnBullet(this);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Death")) 
		{
			StopCoroutine(DespawnRoutine(despawnTime));
			rb.simulated = false;
            StartCoroutine(DespawnRoutine(despawnWallTime));
        }
    }
}