using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public enum Turn
{
    Player,
    Enemy
}

public class GameManager : MonoBehaviour
{

    Board _board;
    PlayerManager _player;

    List<EnemyManager> _enemies;

    Turn _currentTurn = Turn.Player;

    bool _hasLevelStarter = false;
    bool _isGamePlaying = false;
    bool _isGameOver = false;
    bool _hasLevelFinished = false;

    public bool HasLevelStarter
    {
        get
        {
            return _hasLevelStarter;
        }

        set
        {
            _hasLevelStarter = value;
        }
    }

    public bool IsGamePlaying
    {
        get
        {
            return _isGamePlaying;
        }

        set
        {
            _isGamePlaying = value;
        }
    }

    public bool IsGameOver
    {
        get
        {
            return _isGameOver;
        }

        set
        {
            _isGameOver = value;
        }
    }

    public bool HasLevelFinished
    {
        get
        {
            return _hasLevelFinished;
        }

        set
        {
            _hasLevelFinished = value;
        }
    }

    public Turn CurrentTurn
    {
        get
        {
            return _currentTurn;
        }

        set
        {
            _currentTurn = value;
        }
    }

    public float delay = 1f;

    public UnityEvent setupEvent;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;
    public UnityEvent loseLevelEvent;

    private void Awake()
    {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>() as EnemyManager[];
        _enemies = enemies.ToList();
    }

    private void Start()
    {
        if (_player != null && _board != null)
        {
            StartCoroutine(RunGameLoop());
        }
        else
        {
            Debug.LogWarning("GAMEMANAGER Error: no player or board found!");
        }
    }

    private IEnumerator RunGameLoop()
    {
        yield return StartCoroutine(StartLevelRoutine());
        yield return StartCoroutine(PlayLevelRoutine());
        yield return StartCoroutine(EndLevelRoutine());
    }

    private IEnumerator StartLevelRoutine()
    {
        Debug.Log("START LEVEL");
        if (setupEvent != null)
        {
            setupEvent.Invoke();
        }
        _player.playerInput.InputEnabled = false;
        while (!_hasLevelStarter)
        {
            // show start screen
            // user presses button to start
            // hasLevelStarted = true
            yield return null;
        }
        if (startLevelEvent != null)
        {
            startLevelEvent.Invoke();
        }
    }

    private IEnumerator PlayLevelRoutine()
    {
        Debug.Log("PLAY LEVEL");
        _isGamePlaying = true;
        yield return new WaitForSeconds(delay);
        _player.playerInput.InputEnabled = true;

        if (playLevelEvent != null)
        {
            playLevelEvent.Invoke();
        }

        while (!IsGameOver)
        {
            yield return null;
            // check for game over condition

            // win
            // reach the end of the level

            _isGameOver = IsWinner();

            // lose
            // player dies

            // _isGameOver = true

        }

        Debug.Log("WIN! ===========================");

    }

    private IEnumerator EndLevelRoutine()
    {
        Debug.Log("END LEVEL");
        _player.playerInput.InputEnabled = false;

        if (endLevelEvent != null)
        {
            endLevelEvent.Invoke();
        }

        // show end screen
        while (!_hasLevelFinished)
        {
            // user presses button to continue

            // HasLevelFinished = true
            yield return null;
        }

        RestartLevel();
    }

    public void LoseLevel()
    {
        StartCoroutine(LoseLevelRoutine());
    }

    IEnumerator LoseLevelRoutine()
    {
        _isGameOver = true;

        yield return new WaitForSeconds(1.5f);

        if (loseLevelEvent != null)
        {
            loseLevelEvent.Invoke();
        }

        yield return new WaitForSeconds(2f);

        Debug.Log("LOSE! =============================");

        RestartLevel();
    }

    private void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void PlayLevel()
    {
        _hasLevelStarter = true;
    }

    private bool IsWinner()
    {
        if (_board.PlayerNode != null)
        {
            return (_board.PlayerNode == _board.GoalNode);
        }

        return false;
    }

    void PlayPlayerTurn()
    {
        _currentTurn = Turn.Player;
        _player.IsTurnComplete = false;

        // allow player to move
    }

    void PlayEnemyTurn()
    {
        _currentTurn = Turn.Enemy;

        foreach (EnemyManager enemy in _enemies)
        {
            if (enemy != null)
            {
                enemy.IsTurnComplete = false;

                enemy.PlayTurn();
            }
        }
    }

    private bool IsEnemyTurnComplete()
    {
        foreach (EnemyManager enemy in _enemies)
        {
            if (!enemy.IsTurnComplete)
            {
                return false;
            }
        }

        return true;
    }

    public void UpdateTurn()
    {
        if (_currentTurn == Turn.Player && _player != null)
        {
            if (_player.IsTurnComplete)
            {
                // switch to enemyTurn and play enemies

                PlayEnemyTurn();
            }
        }
        else if (_currentTurn == Turn.Enemy)
        {
            if (IsEnemyTurnComplete())
            {
                PlayPlayerTurn();
            }
        }
    }
}
