using System.Collections;
using UnityEngine;

public class PlayerMover : Mover
{
    PlayerCompass _playerCompass;

    protected override void Awake()
    {
        base.Awake();
        _playerCompass = FindObjectOfType<PlayerCompass>().GetComponent<PlayerCompass>();
    }

    protected override void Start()
    {
        base.Start();
        UpdateBoard();
    }

    void UpdateBoard()
    {
        if (_board != null)
        {
            _board.UpdatePlayerNode();
        }
    }

    protected override IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
    {
        

        if (_playerCompass != null)
        {
            _playerCompass.ShowArrows(false);
        }

        yield return StartCoroutine(base.MoveRoutine(destinationPos, delayTime));

        UpdateBoard();

        if (_playerCompass != null)
        {
            _playerCompass.ShowArrows(true);
        }
    }
}
