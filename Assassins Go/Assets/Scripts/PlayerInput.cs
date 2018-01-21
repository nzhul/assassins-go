using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private float _h;
    private float _v;
    private bool _inputEnabled = false;

    public float H
    {
        get { return _h; }
    }

    public float V
    {
        get { return _v; }
    }

    public bool InputEnabled
    {
        get { return _inputEnabled; }
        set { _inputEnabled = value; }
    }


    public void GetKeyInput()
    {
        if (this.InputEnabled)
        {
            _h = Input.GetAxisRaw("Horizontal");
            _v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            _h = 0;
            _v = 0;
        }
    }

}
