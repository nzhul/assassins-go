using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Board _board;
    PlayerManager _player;

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

    public float delay = 1f;

    public UnityEvent setupEvent;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;

    private void Awake()
    {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
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
}
