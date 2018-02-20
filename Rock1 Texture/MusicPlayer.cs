using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	bool input;
	// Use this for initialization
	void Start () {
		input = true;	
	}
	
	void LoadGameScene(){
		
		SceneManager.LoadScene("GameScene");
	}
	void Awake(){
		
		DontDestroyOnLoad (gameObject);
	
	}
	void Update () {
		
		if (Input.anyKey && input) {
			Invoke ("LoadGameScene", 1f);
			input = false;
		}
	}
}
