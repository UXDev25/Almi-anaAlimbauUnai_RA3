using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletPool))]
public class JosephController : EnemyManager
{
    [SerializeField] bool isLookingRight = true;
    public JosephData data;
    private BulletPool bulletPool;
	private float shootTimer = 0;

    // -- ANIMATOR --
    public bool CanShoot { get; set; } = false;
    

    protected override void Start()
    {
        base.Start();
        bulletPool = GetComponent<BulletPool>();
    }
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) 
        {
            CanShoot = true;
            shootTimer = data.shootCooldown;
        }
    }

    // Animation events

    public void ShootJosephine() 
    {
		Vector2 finalDir = new Vector2(isLookingRight ? data.shootDirection.x : -data.shootDirection.x, data.shootDirection.y);
		bulletPool.GetBullet(transform.position, transform.rotation, finalDir, data.shootForce);
	}
}
