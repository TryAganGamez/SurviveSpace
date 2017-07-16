using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class mAdView : MonoBehaviour {

	void Start(){

		Advertisement.Initialize ("1224702", false);
		StartCoroutine (ShowAdWhenReady ());
	}
	IEnumerator ShowAdWhenReady(){
		while (!Advertisement.IsReady ())
			yield return null;
		Advertisement.Show ();

		}


	}