using UnityEngine;

public class OpenDoor : MonoBehaviour {

    private Animator DoorAnim;

    private void Start()
    {
        DoorAnim = GameObject.Find("Door").GetComponent<Animator>();
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.tag == "Door")
    //    {
    //        DoorAnim.SetBool("open", true);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CubeAccess")
        {
            DoorAnim.SetBool("open", true);
        }
    }
}
