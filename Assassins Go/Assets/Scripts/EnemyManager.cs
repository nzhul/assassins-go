using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
public class EnemyManager : TurnManager
{

    EnemyMover _enemyMover;
    EnemySensor _enemySensor;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemySensor = GetComponent<EnemySensor>();
    }

    public void PlayTurn()
    {
        StartCoroutine(PlayTurnRoutine());
    }

    IEnumerator PlayTurnRoutine()
    {
        if (_gameManager != null && !_gameManager.IsGameOver)
        {
            // detect player
            _enemySensor.UpdateSensor();

            // wait 
            yield return new WaitForSeconds(0f);

            if (_enemySensor.FoundPlayer)
            {
                // attack player

                // notify the gamemanager to lose the level

                _gameManager.LoseLevel();
            }
            else
            {
                // movement
                _enemyMover.MoveOneTurn();
            }
        }
    }
}
