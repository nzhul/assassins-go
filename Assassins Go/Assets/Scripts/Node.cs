using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    Vector2 _coordinate;
    List<Node> _neighborNodes = new List<Node>();
    List<Node> _linkedNodes = new List<Node>();
    Board _board;

    public Vector2 Coordinate
    {
        get
        {
            return Utility.Vector2Round(_coordinate);
        }
    }

    public List<Node> NeighborNodes
    {
        get
        {
            return _neighborNodes;
        }
    }

    public List<Node> LinedNodes
    {
        get
        {
            return _linkedNodes;
        }
    }


    public GameObject linkPrefab;
    public GameObject geometry;
    public float scaleTime = .3f;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    public bool autoRun = false;
    public float initDelay = 1f;
    bool _isInitialized = false;

    private void Awake()
    {
        _board = FindObjectOfType<Board>();
        _coordinate = new Vector2(transform.position.x, transform.position.z);
    }

    private void Start()
    {
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;

            if (autoRun)
            {
                InitNode();
            }
        }

        if (_board != null)
        {
            _neighborNodes = FindNeighbors(_board.AllNodes);
        }
    }

    public void ShowGeometry()
    {
        if (geometry != null)
        {
            iTween.ScaleTo(geometry, iTween.Hash(
                "time", scaleTime,
                "scale", Vector3.one,
                "easetype", easeType,
                "delay", initDelay
            ));
        }
    }

    public List<Node> FindNeighbors(List<Node> nodes)
    {
        List<Node> nList = new List<Node>();

        foreach (Vector2 dir in Board.directions)
        {
            Node foundNeighbor = nodes.Find(n => n.Coordinate == this.Coordinate + dir);
            if (foundNeighbor != null && !nList.Contains(foundNeighbor))
            {
                nList.Add(foundNeighbor);
            }
        }

        return nList;
    }

    public void InitNode()
    {
        if (!_isInitialized)
        {
            ShowGeometry();
            InitNeighbors();
            _isInitialized = true;
        }
    }

    private void InitNeighbors()
    {
        StartCoroutine(InitNeighborsRoutine());
    }

    private IEnumerator InitNeighborsRoutine()
    {
        yield return new WaitForSeconds(initDelay);

        foreach (Node n in _neighborNodes)
        {
            if (!_linkedNodes.Contains(n))
            {
                LinkNode(n);
                n.InitNode();
            }
        }
    }

    void LinkNode(Node targetNode)
    {
        GameObject linkInstance = Instantiate(linkPrefab, transform.position, Quaternion.identity);
        linkInstance.transform.parent = transform;

        Link link = linkInstance.GetComponent<Link>();
        if (link != null)
        {
            link.DrawLink(transform.position, targetNode.transform.position);
        }

        if (!_linkedNodes.Contains(targetNode))
        {
            _linkedNodes.Add(targetNode);
        }
        if (!targetNode.LinedNodes.Contains(this))
        {
            targetNode.LinedNodes.Add(this);
        }
    }
}
