using System.Collections;
using UnityEngine;

public class JosephineController : EnemyManager
{
	
	[SerializeField] private float despawnTime = 2f;
    [SerializeField] private float despawnWallTime = 1f;
    private BulletPool parentPool;

	// -- Animator --
	private Animator animator;
	private bool isDead;
	private readonly int isDeadHash = Animator.StringToHash("isDead");


	public void SetPool(BulletPool pool) => parentPool = pool;

	public void Init(Vector2 direction, float shootForce)
	{
		if (animator == null) animator = GetComponent<Animator>();
		if (rb == null) rb = GetComponent<Rigidbody2D>();

		isDead = false;
		animator.SetBool(isDeadHash, isDead);
		rb.linearVelocity = Vector2.zero;
		rb.angularVelocity = 0f;
		rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
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
			base.rb.simulated = false;
			isDead = true;
			animator.SetBool(isDeadHash, isDead);
            StartCoroutine(DespawnRoutine(despawnWallTime));
        }
    }
}