using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectAR : MonoBehaviour
{
    public GameObject objectPrefab;
    private GameObject spawnedObject;
    private ARRaycastManager _aRRaycastManager;
    private Vector2 touchPosition;

    private float initialDistance;
    private Vector3 initialScale;

    private Touch touchZero, touchOne;

    private Vector2 initialPosition;
    private Quaternion initialRotation;

    private Quaternion rotationY;
    private float rotateSpeed = 0.1f;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if(Input.touchCount > 0)
        {
            SwipeToRotate();
        }
        TouchToSpawn();
        PinchToScale();
    }

    void SwipeToRotate()
    {
            touchZero = Input.GetTouch(0);

            if(touchZero.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touchZero.deltaPosition.x * rotateSpeed, 0f);

                spawnedObject.transform.rotation = rotationY * spawnedObject.transform.rotation;
            }

    }
        void TouchToSpawn()
        {
            if (_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                var hitPose = hits[0].pose;

                    if (spawnedObject == null)
                    {
                        spawnedObject = Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
                        spawnedObject.transform.localScale = new Vector3(0, 0, 0);
                        initialPosition = hitPose.position;
                        initialRotation = hitPose.rotation;
                        Debug.Log("Hit Pose Location" + hitPose.position.ToString());
                        LeanTween.scale(spawnedObject, new Vector3(0.2f, 0.2f, 0.2f), 0.5f).setEase(LeanTweenType.easeOutBounce);
                    }
                    else
                    {
                        spawnedObject.transform.position = hitPose.position;
                    }
                }
        }

        void PinchToScale()
        {
            if (Input.touchCount == 2)
            {
                touchZero = Input.GetTouch(0);
                touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                    touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    return;
                }

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = spawnedObject.transform.localScale;
                }
                else
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                    if (Mathf.Approximately(initialDistance, 0))
                    {
                        return;
                    }

                    var factor = currentDistance / initialDistance;
                    spawnedObject.transform.localScale = initialScale * factor;
                }
            }
        }

    public void ResetObject()
    {
        AudioController.PlayMusic("ObjectReset");
        spawnedObject.transform.localScale = initialScale;
        spawnedObject.transform.position = initialPosition;
        spawnedObject.transform.rotation = initialRotation;
    }
}
