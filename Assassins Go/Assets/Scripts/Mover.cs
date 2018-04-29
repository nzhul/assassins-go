using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{

    public Vector3 destination;

    public bool faceDestination = false;

    public bool isMoving = false;

    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    public float moveSpeed = 1.5f;

    // time to rotate to face destination
    public float rotateTime = 0.5f;

    public float iTweenDelay = 0;

    protected Board _board;

    protected Node _currentNode;

    public UnityEvent finishMovement;

    protected virtual void Awake()
    {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
    }

    protected virtual void Start()
    {
        UpdateCurrentNode();
    }

    public void Move(Vector3 destinationPos, float delayTime = .25f)
    {
        if (isMoving)
        {
            return;
        }

        if (_board != null)
        {
            Node targetNode = _board.FindNodeAt(destinationPos);
            if (targetNode != null && _currentNode != null)
            {
                if (_currentNode.LinkedNodes.Contains(targetNode))
                {
                    StartCoroutine(MoveRoutine(destinationPos, delayTime));
                }
            }
            else
            {
                Debug.Log("MOVER: " + _currentNode.name + " not connected " + targetNode.name);
            }
        }
    }

    protected virtual IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
    {
        isMoving = true;

        destination = destinationPos;

        // optional turn to face destination
        if (faceDestination)
        {
            FaceDestination();
            yield return new WaitForSeconds(0.25f);
        }

        // pause the coroutine for a brief period
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
        UpdateCurrentNode();
    }

    public void MoveLeft()
    {
        Vector3 newPosition = transform.position + new Vector3(-Board.spacing, 0, 0);
        Move(newPosition, 0);
    }

    public void MoveRight()
    {
        Vector3 newPosition = transform.position + new Vector3(Board.spacing, 0, 0);
        Move(newPosition, 0);
    }

    public void MoveForward()
    {
        Vector3 newPosition = transform.position + new Vector3(0, 0, Board.spacing);
        Move(newPosition, 0);
    }

    public void MoveBackward()
    {
        Vector3 newPosition = transform.position + new Vector3(0, 0, -Board.spacing);
        Move(newPosition, 0);
    }

    protected void UpdateCurrentNode()
    {
        if (_board != null)
        {
            _currentNode = _board.FindNodeAt(transform.position);
        }
    }

    void FaceDestination()
    {
        Vector3 relativePosition = destination - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

        float newY = newRotation.eulerAngles.y;

        iTween.RotateTo(gameObject, iTween.Hash(
              "y", newY,
              "delay", 0f,
              "easetype", easeType,
              "time", rotateTime
            ));
    }

}
