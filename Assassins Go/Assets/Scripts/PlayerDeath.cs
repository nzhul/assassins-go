using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public Animator playerAnimController;

    public string playerDeathTrigger = "IsDead";

    public void Die()
    {
        if (playerAnimController != null)
        {
            playerAnimController.SetTrigger(playerDeathTrigger);
        }
    }
}