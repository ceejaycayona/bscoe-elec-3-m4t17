using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	public ParticleSystem part;
	AudioSource _audioSource;
	bool _isSoundPlaying = false;


		

	void OnParticleCollision(GameObject other){
		_audioSource = GetComponent<AudioSource> ();
		if (other.gameObject.tag == "bullet") {
			part.Play ();
			Destroy(gameObject, 1f);
				_audioSource.Play ();
				_isSoundPlaying = true;
			}
			else {
				_audioSource.Stop ();
				_isSoundPlaying = false;
			}
			
	}
}
