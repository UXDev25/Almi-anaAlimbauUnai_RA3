using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
	[SerializeField] private JosephineController bulletPrefab;
	[SerializeField] private int initialSize = 10;

	private readonly Stack<JosephineController> pool = new Stack<JosephineController>();

	private void Awake()
	{
		for (int i = 0; i < initialSize; i++)
			pool.Push(CreateBullet());
	}

	private JosephineController CreateBullet()
	{
		JosephineController bullet = Instantiate(bulletPrefab);
		bullet.SetPool(this);
		bullet.gameObject.SetActive(false);
		return bullet;
	}

	public JosephineController GetBullet(Vector3 position, Quaternion rotation, Vector2 direction, float shootForce)
	{
		JosephineController bullet = pool.Count > 0 ? pool.Pop() : CreateBullet();
		bullet.transform.SetPositionAndRotation(position, rotation);
		bullet.gameObject.SetActive(true);
		bullet.Init(direction, shootForce);
		return bullet;
	}

	public void ReturnBullet(JosephineController bullet)
	{
		bullet.gameObject.SetActive(false);
		pool.Push(bullet);
	}
}