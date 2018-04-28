using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelLabel : MonoBehaviour
{
    Text _text;

    void Awake()
    {
		_text = GetComponent<Text>();

		if (_text != null)
		{
			_text.text = SceneManager.GetActiveScene().name;
		}
    }
}
