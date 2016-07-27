using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using ADInterstitialAd = UnityEngine.iOS.ADInterstitialAd;

public class GameController : Singleton<GameController>
{
    public GameObject Player;
    public GameObject trajectoryPointPrefab;
    [Range(30, 60)] public int Angle = 50;
    [Range(300, 600)] public int Force = 300;
    [Range(5, 20)] public int numOfTrajectoryPoints = 20;
    private bool isJumpSucceed = false;
    private Vector3 playerInitialPosition;
    private Rigidbody2D playerRigidbody2D;


    private List<GameObject> trajectoryPoints;
    private int _lastTrajectoryAngle = 50;
    private int _lastForce = 50;
    private GameObject _trajectoryPointHolder;

    private void initEnvironment()
    {
        var platformPrefab = Resources.Load("Prefabs/Platform") as GameObject;
        var playerWidth = Player.GetComponent<SpriteRenderer>().bounds.size.x;
        var playerHeight = Player.GetComponent<SpriteRenderer>().bounds.size.y;
        var platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        var platformHeight = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        var startPlatform = Instantiate(platformPrefab) as GameObject;
        var screenPosBottomLeft = new Vector3(0, 0, 10);
        var worldPosBottomLeft = Camera.main.ScreenToWorldPoint(screenPosBottomLeft);

        //Set start platform position
        startPlatform.transform.position = new Vector3(worldPosBottomLeft.x + platformWidth / 2,
            worldPosBottomLeft.y + platformHeight / 2, worldPosBottomLeft.z);

        //Set player start position
        Player.transform.position = new Vector3(startPlatform.transform.position.x,
            startPlatform.transform.position.y + platformHeight / 2 + playerHeight / 2, worldPosBottomLeft.z);

        Debug.Log("initEnvironment::Completed");
    }

    void Start ()
	{
	    Application.targetFrameRate = 60;
	    initEnvironment();
	    playerInitialPosition = Player.transform.position;
	    playerRigidbody2D = Player.GetComponent<Rigidbody2D>();
	    trajectoryPoints = new List<GameObject>();
	    _trajectoryPointHolder = new GameObject {name = "TrajectoryPointHolder"};
	    for (var i = 0; i < numOfTrajectoryPoints; i++)
	    {
	        var point = Instantiate(trajectoryPointPrefab);
	        point.transform.SetParent(_trajectoryPointHolder.transform, false);
	        trajectoryPoints.Insert(i, point);
	    }
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
        if (_lastTrajectoryAngle == Angle && _lastForce == Force && trajectoryPoints.Count == numOfTrajectoryPoints)
            return;
        if (trajectoryPoints.Count != numOfTrajectoryPoints)
        {
            foreach (var point in trajectoryPoints)
            {
                point.GetComponent<SpriteRenderer>().enabled = false;
            }
            trajectoryPoints.Clear();
            for (var i = 0; i < numOfTrajectoryPoints; i++)
            {
                var point = _trajectoryPointHolder.transform.GetChild(i).gameObject;
                point.GetComponent<SpriteRenderer>().enabled = true;
                trajectoryPoints.Insert(i, point);
            }
        }

        Debug.Log("DrawTrajectory::StartDrawing");
        //FirePlayer();
        var dir = Quaternion.AngleAxis(Angle, Vector3.forward) * Vector3.right;
        var f = dir * Force;
        var velocity = (f / playerRigidbody2D.mass) * Time.fixedDeltaTime;
        setTrajectoryPoints(playerInitialPosition, velocity);
        _lastTrajectoryAngle = Angle;
        _lastForce = Force;
        //playerRigidbody2D.velocity = Vector2.zero;
        //Player.transform.position = playerInitialPosition;

    }

    void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        var velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        var angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
        fTime += 0.1f;
        for (var i = 0; i < numOfTrajectoryPoints; i++)
        {
            var dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            var dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) -
                       (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            var pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0,
                Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }

    private void FirePlayer()
    {
        var dir = Quaternion.AngleAxis(Angle, Vector3.forward) * Vector3.right;
        playerRigidbody2D.AddForce(dir * Force);
    }

    private void PlaceNextPlatform()
    {

        isJumpSucceed = false;
    }

    public void SetJumpFinalState(bool state)
    {
        isJumpSucceed = state;
    }
}
