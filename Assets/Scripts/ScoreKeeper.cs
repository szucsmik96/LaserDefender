using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	public int score = 0;
	private Text myText;

	void Start(){
		myText=GetComponent<Text>();
		Reset ();
	}
	public void Score(int points){
		score += points;
		myText.text = score.ToString();
	}
	public void Reset(){
		score = 0;
		myText.text = score.ToString();
	}
}
