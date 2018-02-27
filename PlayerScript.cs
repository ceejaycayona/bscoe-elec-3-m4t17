using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	GameObject rocket;
	GameObject terrain;
	Rigidbody _rigidbody;
	ParticleSystem Explode;
	AudioSource _audioSource;
	bool _isSoundPlaying = false;

	[SerializeField] float speed = 100f;
	[SerializeField]float rotation_Y = 0f;
	[SerializeField]float rotation_x = 0f;
	[SerializeField]float minimum_X = -50f;
	[SerializeField]float maximum_X = 50f;
	[SerializeField]float minimum_Y = -50f;
	[SerializeField]float maximum_Y = 50f;
	[SerializeField]float tiltation = 10f;
	[SerializeField]float xThrow,yThrow;
	Vector3 rocketposition;
	Vector3 initialpos;
	// Use this for initialization
	void Start () {
		rocket = GameObject.Find ("PlayerShip");
		terrain = GameObject.Find ("Terrain");
		_rigidbody = GetComponent<Rigidbody> ();
		Explode = GetComponentInChildren<ParticleSystem>();
		_audioSource = GetComponent<AudioSource> ();
	//	initialpos = gameObject.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		rocketposition = transform.localPosition;		
		float xThrow = CrossPlatformInputManager.GetAxis ("Horizontal");
		print (xThrow);
		float yThrow = CrossPlatformInputManager.GetAxis ("Vertical");
		print (yThrow);
		rocketposition.x += xThrow * speed * Time.deltaTime;
		rocketposition.y += yThrow * speed * Time.deltaTime;
		rocketposition = new Vector3 (Mathf.Clamp (rocketposition.x, minimum_X, maximum_X), Mathf.Clamp (rocketposition.y, minimum_Y, maximum_Y), transform.localPosition.z);

		rotation_x = Mathf.Clamp (rotation_x, -30f, 30f);
		rotation_Y = Mathf.Clamp (rotation_Y, -30f, 30f); 
        if (yThrow == 0 && (transform.localEulerAngles.x >= 1 || transform.localEulerAngles.x <= -1))
        {
			if (rotation_Y >= -50.0f && rotation_Y <= 0.0f)
            {
				rotation_Y += 1f;
            }
			else if (rotation_Y <= 50.0f)
            {
				rotation_Y -= 1f;
            }
        }
        else if (yThrow == 0)
        {
			rotation_Y = 0f;
        }

		rotation_Y += -yThrow * tiltation * Time.deltaTime * 10;
		rotation_Y = Mathf.Clamp(rotation_Y, -50.0f, 50.0f);

        // For Up and Down
        if (xThrow == 0 && (transform.localEulerAngles.z >= 1 || transform.localEulerAngles.z <= -1))
        {
			if (rotation_x >= -70.0f && rotation_x <= 0.0f)
            {
				rotation_x += 1f;
            }
			else if (rotation_x <= 70.0f)
            {
				rotation_x -= 1f;
            }
        }
        else if (xThrow == 0)
        {
			rotation_x = 0f;
        }

		rotation_x += -xThrow * tiltation * Time.deltaTime * 10;
		rotation_x = Mathf.Clamp(rotation_x, -38.0f, 50.0f);


        transform.localEulerAngles = new Vector3(
			Mathf.Clamp(rotation_Y, -50.0f, 50.0f),
            0,
			Mathf.Clamp(rotation_x, -50.0f, 50.0f)
            );

		transform.localPosition = rocketposition;
	}

	IEnumerator ExecuteAfterTime(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene("GameScene");

	}

	private void OnTriggerEnter (Collider collider) {
		if (!_isSoundPlaying) {
			_audioSource.Play ();
			_isSoundPlaying = true;
		}
	else {
		_audioSource.Stop ();
		_isSoundPlaying = false;
	}

		print("collide");
		Explode.Play();
		StartCoroutine ("ExecuteAfterTime");

	}
		
}
