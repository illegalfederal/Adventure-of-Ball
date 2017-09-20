using UnityEngine;

public class Coin : MonoBehaviour {
	
    [SerializeField]
    private float rotateSpeed;
    
    void Update () {
		gameObject.transform.Rotate(0,0,Time.deltaTime * rotateSpeed);
    }
}
