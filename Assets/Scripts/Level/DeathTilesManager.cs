using System;
using UnityEngine;

public class DeathTilesManager : MonoBehaviour, IDeadly
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockBackPow = 10;
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
