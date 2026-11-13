using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private static readonly int IsMidAirHash = Animator.StringToHash("isMidAir");
    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
    private static readonly int IsDeadHash = Animator.StringToHash("isDead");
    private static readonly int TurningHash = Animator.StringToHash("isTurning");
    private BasePlayer player;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool isMidAir;
    private bool isWalking;
    private bool isStunned;

    void Start()
    {
        player = GetComponent<BasePlayer>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!player.IsStunned && player.CanPlay) FlipDirection();
        isWalking = !isMidAir && player.MoveValue.x != 0;
        animator.SetBool(IsWalkingHash, isWalking);
        isMidAir = !player.groundDetector.IsGrounded && !player.IsTurning;
        animator.SetBool(IsMidAirHash, isMidAir);
        isStunned = player.IsStunned;
        animator.SetBool(IsDeadHash, isStunned);
        animator.SetBool(TurningHash, player.IsTurning);
        //Debug.Log($"is walking: {isWalking}, isMidAir: {isMidAir}");
    }

    private void FlipDirection()
    {
        if (player.MoveValue.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.MoveValue.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void CheckIfGroundedAtTurn()
    {
        if (player.IsTurning && player.groundDetector.IsGrounded)
        {
            player.IsTurning = false;
            FlipCharacter();
        }
    }

    public void FlipCharacter() 
    {
        sprite.flipY = !sprite.flipY;
    }
    public void SetIsTurningFalse()
    {
        player.IsTurning = false;
    }
}