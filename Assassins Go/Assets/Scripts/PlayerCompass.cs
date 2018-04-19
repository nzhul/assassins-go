using System.Collections.Generic;
using UnityEngine;

public class PlayerCompass : MonoBehaviour
{

    Board _board;
    public GameObject arrowPrefab;
    List<GameObject> _arrows = new List<GameObject>();
    public float scale = 1f;

    public float startDistance = .25f;
    public float endDistance = .5f;

    public float moveTime = 1f;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    public float delay = 0f;


    private void Awake()
    {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        SetupArrows();
    }

    void SetupArrows()
    {
        if (arrowPrefab == null)
        {
            Debug.LogWarning("PLAYERCOMPASS SetupArrows ERROR: Missing arrow prefab!");
        }

        foreach (Vector2 dir in Board.directions)
        {
            Vector3 dirVector = new Vector3(dir.normalized.x, 0f, dir.normalized.y);
            Quaternion rotation = Quaternion.LookRotation(dirVector);

            GameObject arrowInstance = Instantiate(arrowPrefab, transform.position + dirVector * startDistance, rotation);
            arrowInstance.transform.localScale = new Vector3(scale, scale, scale);
            arrowInstance.transform.parent = transform;

            _arrows.Add(arrowInstance);
        }
    }

    void MoveArrow(GameObject arrowInstance)
    {
        iTween.MoveBy(arrowInstance, iTween.Hash(
            "z", endDistance - startDistance,
            "looptype", iTween.LoopType.loop,
            "time", moveTime,
            "easetype", easeType
        ));
    }

    void MoveArrows()
    {
        foreach (GameObject arrow in _arrows)
        {
            MoveArrow(arrow);
        }
    }

    public void ShowArrows(bool state)
    {
        if (_board == null)
        {
            Debug.LogWarning("PLAYERCOMPASS ShowArrows ERROR: no Board found!");
            return;
        }

        if (_arrows == null || _arrows.Count != Board.directions.Length)
        {
            Debug.LogWarning("PLAYERCOMPASS ShowArrows ERROR: no arrows found!");
            return;
        }
    }
}