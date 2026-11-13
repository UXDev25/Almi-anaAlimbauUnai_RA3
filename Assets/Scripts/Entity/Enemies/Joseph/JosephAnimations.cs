using UnityEngine;

public class JosephAnimations : MonoBehaviour
{
    private readonly int canShootHash = Animator.StringToHash("canShoot");
	private Animator animator;
    private JosephController joseph;
	private void Start()
    {
        joseph = GetComponent<JosephController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(canShootHash, joseph.CanShoot);
    }
	public void SetCanShootFalse()
	{
        joseph.CanShoot = false;
	}

}
