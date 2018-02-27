using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	public ParticleSystem ExplodeEnemy;

	void onParticleCollision(GameObject other){
		if (other.gameObject.tag == "bullet") {
			ExplodeEnemy.Play ();
			Destroy(other, 1f);
			print ("boom");
		
		}

	}
}
