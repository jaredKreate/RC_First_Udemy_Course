using UnityEngine;
using System.Collections;

/// <summary>
/// This class is responsible for handling entrance and exit strategies for our pages.
/// This class will be attached to each "physical" page game object.
/// </summary>
public class Page : MonoBehaviour {

    public string pageID = "Enter Page ID Here"; //This will be used when we use buttons to spawn new pages.

    Animator anim; //used to access entrance and exit animations
    bool active = true; //when this page is not active (eg false), the Menu Manager will destroy it.

    public bool Active { get { return active; } }//This property protects our data from getting set by outsiders.

    //Unity calls this function for us
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Tell the page to begin animating out of the game view.
    /// </summary>
    public void Exit()
    {
        
    }
}
