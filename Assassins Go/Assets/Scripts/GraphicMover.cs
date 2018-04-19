using UnityEngine;

public enum GraphicMoverMode
{
    MoveTo,
    ScaleTo,
    MoveFrom
}

public class GraphicMover : MonoBehaviour
{

    public GraphicMoverMode mode;

    public Transform startXform;
    public Transform endXForm;

    public float moveTime = 1f;
    public float delay = 0f;
    public iTween.LoopType loopType = iTween.LoopType.none;
    public iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    private void Awake()
    {
        if (endXForm == null)
        {
            endXForm = new GameObject(gameObject.name + "XformEnd").transform;
            endXForm.position = transform.position;
            endXForm.rotation = transform.rotation;
            endXForm.localScale = transform.localScale;
        }

        if (startXform == null)
        {
            startXform = new GameObject(gameObject.name + "XformStart").transform;
            startXform.position = transform.position;
            startXform.rotation = transform.rotation;
            startXform.localScale = transform.localScale;
        }
    }

    public void Reset()
    {
        switch (mode)
        {
            case GraphicMoverMode.MoveTo:
                if (startXform != null)
                {
                    transform.position = startXform.position;
                }
                break;
            case GraphicMoverMode.ScaleTo:
                if (startXform != null)
                {
                    transform.localScale = startXform.localScale;
                }

                break;
            case GraphicMoverMode.MoveFrom:
                if (endXForm != null)
                {
                    transform.position = endXForm.position;
                }
                break;
            default:
                break;
        }
    }

    public void Move()
    {
        switch (mode)
        {
            case GraphicMoverMode.MoveTo:
                iTween.MoveTo(gameObject, iTween.Hash(
                    "position", endXForm.position,
                    "time", moveTime,
                    "delay", delay,
                    "easetype", easeType,
                    "looptype", loopType
                ));
                break;
            case GraphicMoverMode.ScaleTo:
                iTween.ScaleTo(gameObject, iTween.Hash(
                    "scale", endXForm.localScale,
                    "time", moveTime,
                    "delay", delay,
                    "easetype", easeType,
                    "looptype", loopType
                ));
                break;
            case GraphicMoverMode.MoveFrom:
                iTween.MoveFrom(gameObject, iTween.Hash(
                    "position", startXform.position,
                    "time", moveTime,
                    "delay", delay,
                    "easetype", easeType,
                    "looptype", loopType
                ));
                break;
            default:
                break;
        }
    }


}