using UnityEngine;

public class Ladder : MonoBehaviour {

    public Transform ChTransform;
    public bool inside;
    public float heightFactor = 3.2f;

	private BallController ChController; 

    void Start()
    {
		ChController = GetComponent<BallController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ladder")
        {
            ChController.enabled = false;
            inside = !inside;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            ChController.enabled = true;
            inside = !inside;
        }
    }

    void Update()
    {
        if(inside == true && Input.GetKey("w"))
        {
            ChTransform.transform.position += Vector3.up / heightFactor;
        }
    }
}
