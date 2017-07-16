using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	public Text highscoreText;
	private bool gameOver;
	private int score;
	int highscore;
	public float highscoreCount;
	public Button[] buttons;
	private bool restart;


	void Start (){
		if (PlayerPrefs.GetFloat ("HIGHEST") != null) {
			highscoreCount = PlayerPrefs.GetFloat ("HIGHEST");
		}

		gameOver = false;
		score = 0;
		highscore = PlayerPrefs.GetInt ("SCORE",highscore);
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update (){
		if (score > highscoreCount) {
			highscoreCount = score;
			PlayerPrefs.SetFloat ("HIGHEST", highscoreCount);
		}
		highscoreText.text = "HIGHEST: " + Mathf.Round (highscoreCount);

		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				SceneManager.LoadScene ("one");
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);


		}
	}

	public void AddScore (int newScoreValue){
		
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore (){
		
		scoreText.text = "SCORE: " + score;
	}

	public void GameOver(){

		gameOver = true;
		foreach (Button button in buttons) {
			button.gameObject.SetActive(true);

		}

	}
	public void Play(){
		SceneManager.LoadScene ("one");	
	}

	public void Pause(){

		if (Time.timeScale == 1) {
			Time.timeScale = 0;
		} 
		else if (Time.timeScale == 0) {
			Time.timeScale = 1;
		}
	}
	public void Menu(){

		SceneManager.LoadScene ("menu");
	}
	public void Exit(){
		Application.Quit ();

	}
}