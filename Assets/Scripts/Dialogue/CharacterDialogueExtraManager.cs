using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Yarn.Unity;

public class CharacterDialogueExtraManager : MonoBehaviour
{
    private static Image _portraitImage;

    public void Start()
    {
        _portraitImage = GetComponent<Image>();
        _portraitImage.enabled = false;
    }

    [YarnCommand("set_player_canPlay")]
    public static void SetPlayerCanPlay(bool canPlay)
    {
        LevelManager.Instance.Player.CanPlay = canPlay;
        LevelManager.Instance.Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    [YarnCommand("set_portrait")]
    public static void SetPortrait(string characterName)
    {
        if (LevelManager.Instance.characterPortraits.ContainsKey(characterName))
        {
            _portraitImage.sprite = LevelManager.Instance.characterPortraits[characterName];
            _portraitImage.enabled = true;
        }
        else if (characterName == "none")
        {
            _portraitImage.enabled = false;
        }
    }
}