using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyManager : TurnManager
{

    EnemyMover _enemyMover;
    EnemySensor _enemySensor;
    EnemyAttack _enemyAttack;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemySensor = GetComponent<EnemySensor>();
        _enemyAttack = GetComponent<EnemyAttack>();
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
                // notify the gamemanager to lose the level
                _gameManager.LoseLevel();

                Vector3 playerPosition = new Vector3(_board.PlayerNode.Coordinate.x, 0f, _board.PlayerNode.Coordinate.y);

                _enemyMover.Move(playerPosition, 0f);

                while (_enemyMover.isMoving)
                {
                    yield return null;
                }

                // attack player
                _enemyAttack.Attack();
            }
            else
            {
                // movement
                _enemyMover.MoveOneTurn();
            }
        }
    }
}
