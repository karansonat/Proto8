using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
    private float _ratio = 5.0f;

    void Awake()
    {
        ResizeAccordingToScreenSize();
    }

    public void ResizeAccordingToScreenSize(float ratio = 0.0f, bool random = false)
    {
        var s = GetComponent<SpriteRenderer>().sprite;
        var unitWidth = s.textureRect.width / s.pixelsPerUnit;
        var unitHeight = s.textureRect.height / s.pixelsPerUnit;
        var height = Camera.main.orthographicSize * 2.0;
        var width = height * Screen.width / Screen.height;
        var scaledWidth = width / unitWidth;
        var scaledHeight = height / unitHeight;
        if (random)
        {
            _ratio += ratio;
        }
        transform.localScale = new Vector3((float)scaledWidth / _ratio, transform.localScale.y);
    }
}
