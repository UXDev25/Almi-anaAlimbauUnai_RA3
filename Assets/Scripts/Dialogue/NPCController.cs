using UnityEngine;
using Yarn.Unity;

public class NPCController : MonoBehaviour
{
    [Header("Yarn Spinner Settings")]
    [SerializeField] private string _dialogueStartNode = "Start";
    private DialogueRunner _dialogueRunner; // Global dialogue runner

    [Header("Player detection")]
    private readonly string _playerTag = "Player";
    private BoxCollider2D _dialogueTriggerBox;
    private bool isTouchingPlayer = false;

    [Header("Components")]
    [SerializeField] private Transform _leftPlayerPos;
    [SerializeField] private Transform _rightPlayerPos;

    void Start()
    {
        _dialogueTriggerBox = GetComponent<BoxCollider2D>();
        _dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }
    public async void Interact()
    {
        if (!_dialogueRunner.IsDialogueRunning && isTouchingPlayer)
        {
            Debug.Log("Interacting");
            await _dialogueRunner.StartDialogue(_dialogueStartNode);
        }
    }

    public void CalculatePlayerTalkPos()
    {
        Vector3 talkPos = LevelManager.Instance.Player.transform.position.x < _leftPlayerPos.position.x ? _leftPlayerPos.position : _rightPlayerPos.position;
    }

    private void AsignControllerToPlayer(BasePlayer player, bool deasign)
    {
        isTouchingPlayer = deasign;
        if (deasign)
        {
            Debug.Log("Touching Player");
            player.foundNPC = this;
            return;
        }
        Debug.Log("Not Touching Player");
        player.foundNPC = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag) && collision.GetComponent<BasePlayer>() != null)
        {
            AsignControllerToPlayer(collision.GetComponent<BasePlayer>(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag) && collision.GetComponent<BasePlayer>() != null)
        {
            AsignControllerToPlayer(collision.GetComponent<BasePlayer>(), false);
        }
    }
}