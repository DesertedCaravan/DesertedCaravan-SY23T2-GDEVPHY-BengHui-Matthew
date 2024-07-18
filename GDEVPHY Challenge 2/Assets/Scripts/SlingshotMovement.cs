using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlingshotMovement : MonoBehaviour
{
    // Background Link: https://www.pinterest.com/pin/556405728936887534/
    // Slingshot Image Link: https://www.pngwing.com/en/free-png-nwstc
    // Red Bird Image Link: https://angrybirdsfanon.fandom.com/wiki/Angry_Birds_Adventure_(Gui7814)?file=Angry_Bird_red.png

    [Header("Reference Points")]
    [SerializeField] private GameObject backSpoke;
    [SerializeField] private GameObject foreSpoke;
    [SerializeField] private GameObject anchor;

    [Header("Other Game Objects")]
    [SerializeField] private BirdMovement redBird;
    [SerializeField] private GameObject trailDotPrefab;
    [SerializeField] private List<GameObject> trajectoryPreview = new List<GameObject>();

    private Camera camera;

    [Header("Important Variables")]
    [SerializeField] private float _maxPullDistance;
    [SerializeField] private float _maxForce;

    private Vector3 _rubberDirection;
    private float _rubberLength;
    private float _pullRestart;


    // Start is called before the first frame update
    void Start()
    {
        redBird.transform.position = anchor.transform.position;

        camera = Camera.main;

        // Ideal _maxPullDistance = 1.5f;

        foreach (GameObject dot in trajectoryPreview)
        {
            dot.transform.position = anchor.transform.position;
        }

        _pullRestart = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        // GetMouseButton = mouse is held down
        // GetMouseButtonDown = mouse is pressed

        if (!redBird.BirdReleased)
        {
            // Handle screen touches.
            if (Input.GetMouseButton(0) && redBird.Dragging)
            {
                // Reference: https://stackoverflow.com/questions/46998241/getting-mouse-position-in-unity

                Vector3 mouse = Input.mousePosition;

                redBird.transform.position = new Vector3(camera.ScreenToWorldPoint(mouse).x, camera.ScreenToWorldPoint(mouse).y, 0);

                // Set Max Stretch Distance for Rubber
                // _rubberDirection is the vector pointing from anchor to redBird
                _rubberDirection = redBird.transform.position - anchor.transform.position;
                _rubberLength = _rubberDirection.magnitude;
                float rubberClamp = Mathf.Clamp(_rubberLength, 0, _maxPullDistance);

                if (_rubberLength > _maxPullDistance)
                {
                    // rescale rubberDirection size based on percentage of "clamp / actual length (with mouse)"
                    // new rubberDirection has a magnitude equal to rubberClamp
                    _rubberDirection *= (rubberClamp / _rubberLength);

                    // set redBird position to anchor, then move away from it by new rubberDirection
                    redBird.transform.position = anchor.transform.position + _rubberDirection;

                    // stop velocity (may remove later)
                    redBird.BirdStop();
                }

                for (int i = 0; i < trajectoryPreview.Count; i++)
                {
                    // WIP
                    trajectoryPreview[i].transform.position = new Vector3(
                        (anchor.transform.position + (_rubberDirection).normalized * -1 * (i)).x,
                        (anchor.transform.position + (_rubberDirection).normalized * -1 * (i)).y,
                        0
                        );

                    Debug.Log(trajectoryPreview[i].transform.position);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                foreach (GameObject dot in trajectoryPreview)
                {
                    dot.transform.position = anchor.transform.position;
                }

                if (_rubberLength > _pullRestart)
                {
                    // Calculate slingForce
                    _rubberDirection = redBird.transform.position - anchor.transform.position;
                    _rubberLength = _rubberDirection.magnitude;
                    float stretch = _rubberLength - 0.1f;

                    Vector3 slingForce = _rubberDirection.normalized * (-1 * stretch * _maxForce);

                    Debug.Log(slingForce);

                    redBird.BirdRelease(slingForce); // Must be applied continuously
                }
                else
                {
                    redBird.transform.position = anchor.transform.position;
                }
            }
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}