using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public GameObject objToTP;
    public GameObject tpLoc;
    
    [SerializeField] 
    private PhysicMaterial ballPhys;
    private float tpSpeed;

    void Start()
    {
        if(objToTP == null)
        objToTP = GameObject.FindGameObjectWithTag("Player");    
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(1);
        ballPhys.bounciness = 0.4f;
        // Wait koy
        print(Time.time);
    }

    void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "Player" || other.gameObject.tag == "CubeAccess"))
        {
            objToTP.transform.position = tpLoc.transform.position;
            Debug.Log("Teleport!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            Debug.Log("Teleport Disi");
            StartCoroutine(Example());
            ballPhys.bounciness = 0.0f;
        }
    }
}
