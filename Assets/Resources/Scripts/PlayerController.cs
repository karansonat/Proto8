using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private GameObject _targetPlatform;
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTargetPlatform(GameObject target)
    {
        _targetPlatform = target;
    }

    void OnCollisionEnter2D(Collision2D col)
    {/*
        if (col.gameObject.GetInstanceID() == _targetPlatform.GetInstanceID())
        {
            GameController.Instance.SetJumpFinalState(true);
        }
        else if (col.gameObject.tag == "Ground")
        {
            Debug.Log("GameOver");
            GameController.Instance.SetJumpFinalState(false);
        }
*/
    }
}
