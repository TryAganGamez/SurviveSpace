using UnityEngine;
using System.Collections;

public class bgMove : MonoBehaviour {

	public float speed;
	Vector3 offset;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		offset = new Vector3 (0, Time.time * speed);
		GetComponent<Renderer> ().material.mainTextureOffset = offset;
	}
}
