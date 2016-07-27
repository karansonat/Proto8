using UnityEngine;
using System.Collections;

public class PlayerController : Singleton<PlayerController>
{
    private float ratio = 10.0f;
    private GameObject _targetPlatform;

    void Awake()
    {
        var s = GetComponent<SpriteRenderer>().sprite;
        var unitWidth = s.textureRect.width / s.pixelsPerUnit;
        var unitHeight = s.textureRect.height / s.pixelsPerUnit;
        var height = Camera.main.orthographicSize * 2.0;
        var width = height * Screen.width / Screen.height;
        var scaledWidth = width / unitWidth;
        var scaledHeight = height / unitHeight;
        transform.localScale = new Vector3((float) scaledWidth / ratio, (float) scaledWidth / ratio);
    }

    public void SetTargetPlatform(GameObject target)
    {
        _targetPlatform = target;
    }
}
