using UnityEngine;

public class FollowBall : MonoBehaviour {

    private GameObject BallCharacter;
    public Transform asd;

	void Start () {
        BallCharacter = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        this.transform.position = BallCharacter.transform.position;
        this.transform.position += Vector3.up * 2.12f;
        this.transform.position += Vector3.forward * 0.53f;
        this.transform.position += Vector3.left * 0.93f;
    }
}
