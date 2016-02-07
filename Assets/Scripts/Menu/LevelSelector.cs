using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
///This class is responsible for listening for the 'next' and 'previous' button taps and taking
///appropriate action to bring up the next available splash screen.
///</summary>
public class LevelSelector : MonoBehaviour {

    public Sprite[] levelImages;

    MenuManager menu;
	int activeLevel = 0;
    string level;
	
	// Use this for initialization
	void Start () {
        menu = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        GetComponent<Image>().sprite = levelImages[activeLevel];
		SetActiveLevel(1);
	}

	///<summary>
	///Accepts a parameter 'dir' to dictate the direction this function call will traverse the levels.
	///</summary>
	public void SetActiveLevel(int dir)
	{
		int value = activeLevel + dir;

		if (value > levelImages.Length)
			value = 1;
		else if (value < 1)
			value = levelImages.Length;
        
		//Set new activeLevel
		activeLevel = value;
        //Change sprite based on new active level
        GetComponent<Image>().sprite = levelImages[activeLevel-1];
        //Set the level to be set by the menu manager
        level = "Level" + activeLevel;
		
	}

    /// <summary>
    /// Switches scenes based on 'level'
    /// </summary>
    public void GoToLevel()
    {
        menu.RemoveAllPages();
        SceneManager.LoadScene(level);
    }

}
