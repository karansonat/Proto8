using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public int Angle = 50;
    private bool isJumpSucceed = false;
    private Vector3 playerInitialPosition;

	void Start ()
	{
	    Application.targetFrameRate = 60;
	    playerInitialPosition = Player.transform.position;
	}

	void Update () {

	    //Draw trajectory each frame;
	    DrawTrajectory();

        //Throw player if user touch the screen.
	    if (Input.GetMouseButtonDown(0))
	    {
	        FirePlayer();
	    }

        //If Player reach the target create next platform.
	    if (isJumpSucceed)
	    {
	        PlaceNextPlatform();
	    }
	}

    private void DrawTrajectory()
    {

    }

    private void FirePlayer()
    {
        var rad = Angle * Mathf.Deg2Rad;
        Debug.Log("Sin" + Angle + " = " + Mathf.Sin(rad) + "  Cos" + Angle + " = " + Mathf.Cos(rad));
        var Fx = Mathf.Sin(rad);
        var Fy = Mathf.Cos(rad);
       
    }

    private void PlaceNextPlatform()
    {

        isJumpSucceed = false;
    }
}
