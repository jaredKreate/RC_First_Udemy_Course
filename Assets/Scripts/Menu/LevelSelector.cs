using UnityEngine;
using System.Collections;

///<summary>
///This class is responsible for listening for the 'next' and 'previous' button taps and taking
///appropriate action to bring up the next available splash screen.
///</summary>
public class LevelSelector : MonoBehaviour {

	public string[] pageIDs;

	MenuManager menu;
	int activeLevel = 0;
	
	// Use this for initialization
	void Start () {
		menu = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
		SetActiveLevel(1);
	}

	///<summary>
	///Accepts a parameter 'dir' to dictate the direction this function call will traverse the levels.
	///</summary>
	public void SetActiveLevel(int dir)
	{
		int value = activeLevel + dir;

		if (value > pageIDs.Length)
			value = 1;
		else if (value < 1)
			value = pageIDs.Length;

		//Remove page with ID 'activeLevel'
		menu.RemovePage("level"+activeLevel);
		//Set new activeLevel
		activeLevel = value;
		//Add page with ID 'activeLevel'
		menu.AddPage("level"+activeLevel);
	}

}
