using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    public Vector3 destination;
    public bool isMoving = false;

    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    public float moveSpeed = 1.5f;
    public float iTweenDelay = 0;

    private void Start()
    {
        //StartCoroutine(Test());
    }

    //public IEnumerator Test()
    //{
    //    yield return new WaitForSeconds(1f);
    //    MoveRight();
    //    yield return new WaitForSeconds(2f);
    //    MoveRight();
    //    yield return new WaitForSeconds(2f);
    //    MoveForward();
    //    yield return new WaitForSeconds(2f);
    //    MoveForward();


    //}

    public void Move(Vector3 destinationPos, float delayTime = .25f)
    {
        StartCoroutine(MoveRoutine(destinationPos, delayTime));
    }

    IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
    {
        isMoving = true;
        destination = destinationPos;
        yield return new WaitForSeconds(delayTime);
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destinationPos.x,
            "y", destinationPos.y,
            "z", destinationPos.z,
            "delay", iTweenDelay,
            "easetype", easeType,
            "speed", moveSpeed
        ));

        while (Vector3.Distance(destinationPos, transform.position) > 0.01f)
        {
            yield return null;
        }

        iTween.Stop(gameObject);
        transform.position = destinationPos;
        isMoving = false;
    }

    public void MoveLeft()
    {
        Vector3 newPosition = transform.position + new Vector3(-2, 0, 0);
        Move(newPosition, 0);
    }

    public void MoveRight()
    {
        Vector3 newPosition = transform.position + new Vector3(2, 0, 0);
        Move(newPosition, 0);
    }

    public void MoveForward()
    {
        Vector3 newPosition = transform.position + new Vector3(0, 0, 2);
        Move(newPosition, 0);
    }

    public void MoveBackward()
    {
        Vector3 newPosition = transform.position + new Vector3(0, 0, -2);
        Move(newPosition, 0);
    }

}
