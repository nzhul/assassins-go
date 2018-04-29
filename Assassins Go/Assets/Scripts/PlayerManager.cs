using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : TurnManager
{

    public PlayerMover playerMover;
    public PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerMover = GetComponent<PlayerMover>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.InputEnabled = true;
    }

    private void Update()
    {
        if (playerMover.isMoving || _gameManager.CurrentTurn != Turn.Player)
        {
            return;
        }

        playerInput.GetKeyInput();

        if (playerInput.V == 0)
        {
            if (playerInput.H < 0)
            {
                playerMover.MoveLeft();
            }
            else if (playerInput.H > 0)
            {
                playerMover.MoveRight();
            }
        }
        else if (playerInput.H == 0)
        {
            if (playerInput.V < 0)
            {
                playerMover.MoveBackward();
            }
            else if (playerInput.V > 0)
            {
                playerMover.MoveForward();
            }
        }
    }

}
