using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Vector2 _movementSpeed;
    [SerializeField] private float _movementSpeedMultiplier = 0.1f;
    [SerializeField] private float _baseMovementMult = 0;
    private Vector2 _offset;
    private Material _material;
    private bool isPlayerDead = false;

    public void OnEnable()
    {
        GameEvents.OnPlayerDied += () => isPlayerDead = true;
        GameEvents.OnPlayerRespawned += () => isPlayerDead = false;
    }

    public void OnDisable()
    {
        GameEvents.OnPlayerDied -= () => isPlayerDead = true;
        GameEvents.OnPlayerRespawned -= () => isPlayerDead = false;
    }

    private void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
         if (isPlayerDead)
         {
                return;
         }
         _offset = (((LevelManager.Instance == null ? 0 : LevelManager.Instance.Player.rb.linearVelocity.x) + _baseMovementMult) * _movementSpeedMultiplier) * _movementSpeed * Time.deltaTime;
         _material.mainTextureOffset += _offset;
        
    }
}
