using UnityEngine;
using System.Collections;

public class mPlayerMove : MonoBehaviour {

	public float playerSpeed;
	Vector3 position;
	public float maxPos = 3.2f;
//	public uiManager ui;
	bool currentPlatformAndroid;
	Rigidbody rb;
//	public AudioManager am;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Awake(){

		rb = GetComponent<Rigidbody> ();
		#if UNITY_ANDROID
		currentPlatformAndroid = true;
		#else
		currentPlatformAndroid = false;
		#endif

	}
	// Use this for initialization
	void Start () {
		//	ui = GetComponent<uiManager> ();
		position = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (currentPlatformAndroid == true) {
			//android specific code
			//	TouchMove();
			AccellerometerMove ();

		} else {
			
			position.x += Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime;
			position.x = Mathf.Clamp (position.x, -3.2f, 3.2f);

			transform.position = position;
		}
		position = transform.position;
		position.x = Mathf.Clamp (position.x, -3.2f, 3.2f);
		transform.position = position;
	
	}
	void FixedUpdate (){


		if (Input.GetButton ("Fire1") && Time.time > nextFire) {

			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource> ().Play ();
		}
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);

	}
void OnCollisionEnter(Collision col){

		if (col.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		//	ui.gameOverActivated ();
		}
	}

	void AccellerometerMove(){
		//stores x value in mem 
		float y = Input.acceleration.y;

		float x = Input.acceleration.x;
		if (x < -0.1f) {
			MoveLeft ();
		} else if (x > 0.1f) {
			MoveRight ();
		} else if(y < -0.1f) {
			MoveDown ();
		} else if (y > 0.1f) {
			MoveUp ();
		} else {
			SetVelocityZero ();
		}

	}

	public void MoveLeft(){
		rb.velocity = new Vector3 (-playerSpeed, 0);

	}
	public void MoveRight(){
		rb.velocity = new Vector3 (playerSpeed, 0);

	}
	public void SetVelocityZero(){
		rb.velocity = Vector3.zero;
	
	}
	public void MoveUp(){
		rb.velocity = new Vector3 (-playerSpeed, 0);
	}
	public void MoveDown(){
		rb.velocity = new Vector3 (playerSpeed, 0);

	}
}