using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    [Header("MOVEMENT")]
    public float HorSpeed = 150.0f;
    public float VertSpeed = 3.0f;

    [Header("JUMP")]
    public float jumpHeight = 5f;
    private bool isFalling = false;

	[Header("COIN")]
	public Text CountText;
	public int Count;

    private Rigidbody rb;

   void Start()
    {
        rb = GetComponent<Rigidbody>();
		Count = 0;
		SetCountText ();
    }

    void Update ()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * HorSpeed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * VertSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        // JUMPING
        if (Input.GetKeyDown(KeyCode.Space) && isFalling == false)
        {
            rb.velocity = new Vector3(0, jumpHeight, 0);
        }
        isFalling = true;
    }

    void OnCollisionStay(Collision collision)
    {
        isFalling = false;
    }
		
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Coin")) 
		{
			other.gameObject.SetActive (false);	
			AudioSource source = GetComponent<AudioSource>();
			source.Play();
			Count = Count + 1;
			SetCountText ();
			SetCountDB (Count);
		}

	}
	void SetCountText()
	{
		CountText.text = "Coin: " + Count.ToString ();
	}

	public int SetCountDB(int count){
		print ("Count" + count);
		return count;
	}
}
