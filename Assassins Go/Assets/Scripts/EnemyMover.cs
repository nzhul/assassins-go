using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover {

    protected override void Awake()
    {
        base.Awake();
        faceDestination = true;
    }

    protected override void Start()
    {
        base.Start();
        //StartCoroutine(TestMovementRoutine()); // use this for testing the movement
    }

    IEnumerator TestMovementRoutine()
    {
        yield return new WaitForSeconds(5f);
        MoveForward();

        yield return new WaitForSeconds(2f);
        MoveRight();

        yield return new WaitForSeconds(2f);
        MoveForward();

        yield return new WaitForSeconds(2f);
        MoveForward();

        yield return new WaitForSeconds(2f);
        MoveBackward();

        yield return new WaitForSeconds(2f);
        MoveBackward();

    }

}
