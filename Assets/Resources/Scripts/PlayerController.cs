using UnityEngine;
using System.Collections;

public class PlayerController : Singleton<PlayerController>
{
    private float ratio = 10.0f;
    public GameObject _currentPlatform;
    public GameObject _targetPlatform;

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

    public void SetCurrentPlatform(GameObject target)
    {
        _currentPlatform = target;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GameController.Instance.SetJumpFinalState(false);
            GameController.Instance.nextJumpReady = false;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetInstanceID() == _targetPlatform.GetInstanceID())
        {
            GetComponent<Rigidbody2D>().freezeRotation = true;
            GameController.Instance.SetJumpFinalState(true);
            Destroy(_currentPlatform);
            _currentPlatform = col.gameObject;
            GameController.Instance.nextJumpReady = false;
        }
    }
}
