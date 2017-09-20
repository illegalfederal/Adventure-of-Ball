using UnityEngine;

public class RotateAround : MonoBehaviour {
    [Header("OBJECT ROTATE")]
    public Transform target;
    public Transform ChTransform;
    public bool random_target;
    public int speed = 50;

    [Header("STAY")]
    public bool inside = false;

    private void Start()
    {
        ChTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate ()
    {
       transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        // Collission options
       if(inside == true)
        {
            ChTransform.transform.position = this.transform.position;
            ChTransform.transform.position += Vector3.up * 0.99f;
        }

       if(inside == false && Input.GetKeyDown(KeyCode.Space))
        {
            ChTransform.transform.position = ChTransform.transform.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inside = !inside;
            Debug.Log("Platform!");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            inside = false;
        }
    }
}
