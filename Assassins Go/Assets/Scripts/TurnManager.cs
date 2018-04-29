using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    protected GameManager _gameManager;

    protected bool _isTurnComplete = false;

    public bool IsTurnComplete
    {
        get
        {
            return _isTurnComplete;
        }
        set
        {
            _isTurnComplete = value;
        }
    }

	protected virtual void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public void FinishTurn()
    {
        _isTurnComplete = true;

        // update the GameManager

        if (_gameManager != null)
        {
            _gameManager.UpdateTurn();
        }
    }

}
