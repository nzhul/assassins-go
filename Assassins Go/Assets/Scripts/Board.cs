using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public static float spacing = 2f;

    public static readonly Vector2[] directions =
    {
        new Vector2(spacing, 0f),
        new Vector2(-spacing, 0f),
        new Vector2(0, spacing),
        new Vector2(0, -spacing),
    };


    Node _playerNode;
    PlayerMover _player;
    Node _goalNode;
    List<Node> _allNodes = new List<Node>();

    public Node PlayerNode
    {
        get
        {
            return _playerNode;
        }
    }

    public List<Node> AllNodes
    {
        get
        {
            return _allNodes;
        }
    }

    public Node GoalNode
    {
        get
        {
            return _goalNode;
        }
    }

    public GameObject goalPrefab;
    public float drawGoalTime = 2f;
    public float drawGoalDelay = 2f;
    public iTween.EaseType drawGoalEaseType = iTween.EaseType.easeOutExpo;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
        GetNodeList();

        _goalNode = FindGoalNode();
    }

    private void GetNodeList()
    {
        Node[] nList = FindObjectsOfType<Node>();
        _allNodes = new List<Node>(nList);
    }

    public Node FindNodeAt(Vector3 pos)
    {
        Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));
        return _allNodes.Find(n => n.Coordinate == boardCoord);
    }

    public Node FindPlayerNode()
    {
        if (_player != null && !_player.isMoving)
        {
            return FindNodeAt(_player.transform.position);
        }

        return null;
    }

    public void UpdatePlayerNode()
    {
        _playerNode = FindPlayerNode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f, .5f);
        if (_playerNode != null)
        {
            Gizmos.DrawSphere(_playerNode.transform.position, .2f);
        }
    }

    Node FindGoalNode()
    {
        return _allNodes.Find(n => n.isLevelGoal);
    }

    public void DrawGoal()
    {
        if (goalPrefab != null && this.GoalNode != null)
        {
            GameObject goalInstance = Instantiate(goalPrefab, this.GoalNode.transform.position, Quaternion.identity);
            iTween.ScaleFrom(goalInstance, iTween.Hash(
                "scale", Vector3.zero,
                "time", drawGoalTime,
                "delay", drawGoalDelay,
                "easetype", drawGoalEaseType
            ));
        }
    }

    public void InitBoard()
    {
        if (_playerNode != null)
        {
            _playerNode.InitNode();
        }
    }
}
