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

    List<Node> _allNodes = new List<Node>();

    public List<Node> AllNodes
    {
        get
        {
            return _allNodes;
        }
    }

    private void Awake()
    {
        GetNodeList();
    }

    private void GetNodeList()
    {
        Node[] nList = FindObjectsOfType<Node>();
        _allNodes = new List<Node>(nList);
    }
}
