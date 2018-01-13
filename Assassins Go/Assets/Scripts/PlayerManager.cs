using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{

    public PlayerMover playerMover;
    public PlayerInput playerInput;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.InputEnabled = true;
    }

    private void Update()
    {
        if (playerMover.isMoving)
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
