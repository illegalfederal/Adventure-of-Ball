using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 10f;

    [Header("CAMERA SETTINGS")]
    public float MouseSensitivity = 4f;
    public float ScrollSensitivity = 2f;
    public float OrbitDamepening = 10f;
    public float ScrollDampening = 6f;

    public bool CameraDisabled = false;

    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
	}

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            CameraDisabled = !CameraDisabled;

        if (!CameraDisabled)
        {
            // Rotation f the Camera bsed on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") !=0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y += Input.GetAxis("Mouse Y") * MouseSensitivity;

                // Clamp the y Rotation to horizon and not flipping over at the top
                _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
            }

            // Zooming Input from our Mouse Scroll Wheel
            if(Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;

                // Makes camera zoom faster the further away it is from the target
                ScrollAmount *= (this._CameraDistance * 0.3f);

                this._CameraDistance += ScrollAmount * -1f;

                // This makes camera go no closer than 1.5 meters from target, and no further than 100 meters
                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, 100f);
            }
        }


        // Actuall Camera Rig Transformations
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDamepening);

        if(this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
        {
            this._XForm_Camera.localPosition = 
                new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, 
                this._CameraDistance * -1f, 
                Time.deltaTime * ScrollDampening));
        }
    }
}
