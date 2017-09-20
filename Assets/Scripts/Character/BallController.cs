using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	[Header("MOVEMENT")]
	public float speed = 5.0f;
	public Vector3 MoveVector{set;get;}
	private Transform camTransform;
	private Vector3 movement;

	[Header("JUMP")]
	public float jumpHeight = 5f;
	private bool isFalling = false;

	[Header("COIN")]
	Text CountText;
	public int Count;

	[Header("Platforms")]
	public GameObject gameobj;
	public GameObject startTxt;
	public GameObject endTxt;
	public GameObject endPoint;

    public AudioClip[] audioClips;

    private Rigidbody rb;
    private AudioSource source;

    void Start()
	{
		rb = GetComponent<Rigidbody> ();
        source = GetComponent<AudioSource>();
        CountText = GameObject.Find("GameManager/Canvas/Coin Text").GetComponent<Text>();
        Count = 0;
		SetCountText ();
    }

	void Update()
    {

        // Get the original Input
        MoveVector = PoolInput ();

		// Rotate our MoveVector
		MoveVector = RotateWithView();

		// Move
        Move();

        if(Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(0, 3f, 0);
        }

        if(Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(0, -3f, 0);
        }

		// JUMPING
		if (Input.GetKeyDown(KeyCode.Space) && isFalling == false)
		{
			rb.velocity = new Vector3(0, jumpHeight, 0);
        }
		isFalling = true;
        
    }

    private void Move()
    {
        rb.AddForce(movement * speed);
    }

	private Vector3 PoolInput()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		return movement;
	}

	private Vector3 RotateWithView()
	{
		if (camTransform != null) 
		{
			Vector3 dir = camTransform.TransformDirection (MoveVector);
			dir.Set (dir.x, 0, dir.z);
			return dir.normalized * MoveVector.magnitude;
		}
		else 
		{
			camTransform = GetComponent<BallCamera>().CamTransform;
			return MoveVector;
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		// Coin
		if (other.gameObject.CompareTag ("Coin")) 
		{
			other.gameObject.SetActive (false);	
            source.clip = audioClips[0];
			source.Play ();
			Count = Count + 1;
			SetCountText ();
			SetCountDB (Count);
		}

		// Key interact
		if (other.gameObject.CompareTag ("Key")) 
		{
			other.gameObject.SetActive (false);
			GameObject.FindGameObjectWithTag ("Door").SetActive (false);
		}

		if (other.gameObject.CompareTag ("Engel")) {
			print ("Engel!");
			startTxt.SetActive (false);
			gameobj.SetActive (true);
			endTxt.SetActive (true);
			endPoint.SetActive (true);
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        source.PlayOneShot(audioClips[1]);
    }

    void SetCountText()
	{
		CountText.text = "Coin: " + Count.ToString ();
	}

	public int SetCountDB(int count){
		print ("Count" + count);
		return count;
	}

    void OnCollisionStay(Collision collision)
	{
		isFalling = false;
	}

    
 }
