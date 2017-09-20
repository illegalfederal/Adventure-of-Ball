using UnityEngine;

public class BallCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 180.0f;

    private Transform thisTransform;
    private Camera cam;
    public Transform CamTransform { set; get; }

    // Camera Control
    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    [HideInInspector]
    public float sensivityX = 4.0f; // 4.0
    [HideInInspector]
    public float sensivityY = 1.0f; // 1.0

    // Camera Zoom
    private float minFov = 15f;
    private float maxFov = 90F;
    private float sensivity = 10f;

    void Start()
    {
        CamTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cam = CamTransform.gameObject.GetComponent<Camera>();
        thisTransform = transform;
    }

    void Update()
    {
        currentX += (Input.GetAxis("Mouse X")) * sensivityX;
        currentY += Input.GetAxis("Mouse Y") * sensivityY;

        currentY = ClampAngle(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        // Camera zoom
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        CamTransform.position = thisTransform.position + rotation * dir;
        CamTransform.LookAt(thisTransform.position);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        do
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
        } while (angle < -360 || angle > 360);

        return Mathf.Clamp(angle, min, max);
    }

    //[System.Serializable]
    //public class CollisionHandler
    //{
    //    public LayerMask collisionLayer;

    //    [HideInInspector]
    //    public bool colliding = false;
    //    [HideInInspector]
    //    public Vector3[] adjustCameraClipPoints;
    //    [HideInInspector]
    //    public Vector3[] desiredCameraClipPoints;

    //    Camera camera;

    //    public void Initialize(Camera cam)
    //    {
    //        camera = cam;
    //        adjustCameraClipPoints = new Vector3[5];
    //        desiredCameraClipPoints = new Vector3[5];
    //    }

    //    public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
    //    {
    //        if (!camera)
    //            return;

    //        // clear cthe contents of intoArray
    //        intoArray = new Vector3[5];

    //        float z = camera.nearClipPlane;
    //        float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z;
    //        float y = x / camera.aspect;

    //        // top left
    //        intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; // added and rotated the point relative to camera
    //        // top right
    //        intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition; // added and rotated the point relative to camera
    //        // bottom left
    //        intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition; // added and rotated the point relative to camera
    //        // bottom right
    //        intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition; // added and rotated the point relative to camera
    //        // camera's position
    //        intoArray[4] = cameraPosition - camera.transform.forward;
    //    }

    //    bool CollisionDetectedClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
    //    {
    //        for (int i = 0; i < clipPoints.Length; i++)
    //        {
    //            Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
    //            float distance = Vector3.Distance(clipPoints[i], fromPosition);
    //            if (Physics.Raycast(ray, distance, collisionLayer))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    public float GetAdjustedDistanceWithRayFrom(Vector3 form)
    //    {
    //        float distance = -1;

    //        for (int i = 0; i < desiredCameraClipPoints.Length; i++)
    //        {


    //            Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
    //            RaycastHit hit;
    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                if (distance == -1)
    //                    distance = hit.distance;
    //                else
    //                {
    //                    if (hit.distance < distance)
    //                        distance = hit.distance;
    //                }
    //            }
    //        }

    //        if (distance == -1)
    //            return 0;
    //        else
    //            return distance;
    //    }

    //    public void CheckColliding(Vector3 targetPosition)
    //    {
    //        if (CollisionDetectedClipPoints(desiredCameraClipPoints, targetPosition))
    //        {
    //            colliding = true;
    //        }
    //        else
    //        {
    //            colliding = false;
    //        }
    //    }
    //}

}
