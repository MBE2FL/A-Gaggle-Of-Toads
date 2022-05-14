using UnityEngine;
using UnityEngine.UI;

public class ChangeAlpha : MonoBehaviour
{
    private Text _text;

    private Color _color;

    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(!_text)
            _text = GetComponent<Text>();

        _color = _text.color;
    }

    // Update is called once per frame
    void Update()
    {
        _color.a = Mathf.Abs(Mathf.Sin(Time.time * speed));
        _text.color = _color;
    }
}
