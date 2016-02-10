using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//© 2016 RENAISSANCE CODERS ALL RIGHTS RESERVED

/// <summary>
/// A class that is responsible for the management of our game's User Interface elements.
/// The MenuManager should be able to add or remove pages to the game view.
/// </summary>
public class MenuManager : MonoBehaviour {

    public static MenuManager Instance; //there can only be one instance of a menu manager in the game

    public Page[] Menu; //A collection of all of our menu pages.
    public string entry ="Enter Entry Page ID Here";

    public List<Page> RenderedPages; //A collection of the menu pages currently loaded. 

    //Unity calls this function for us (before Start())
    void Awake()
    {
        if (Instance != null) //When loading 'this' Menu Manager, if an Instance already exists
        {
            Destroy(gameObject); //Destroy this game object
        }
        else //If no instance has been created yet...
        {
            Instance = this; //We need to initialize the Instance variable
            DontDestroyOnLoad(gameObject); //Call this unity function to save an data transferring from scene to scene
            RenderedPages = new List<Page>();
            AddPage(entry);
            StartCoroutine("UpdateMenu", 0.25f); //Update the menu every quarter second.
        }
    }

    IEnumerator UpdateMenu(float update_time)
    {
        while (true)//while the game is running, we loop through the following code
        {
            RemoveInActivePages();
            yield return new WaitForSeconds(update_time); //we will wait this amount of time before the next loop iteration
        }
    }

    /// <summary>
    /// The AddPage function will be called via Unity Button event.
    /// </summary>
    /// <param name="pageID"></param>
    public void AddPage(string pageID)
    {
        for (int i = 0; i < Menu.Length; i++)
        {
            if (Menu[i].pageID == pageID) //If we find the page we are trying to add with this function...
            {
                //We need to add the page to our game
                GameObject newPage = Instantiate(Menu[i].gameObject) as GameObject;
                RectTransform rect = newPage.GetComponent<RectTransform>();
                rect.SetParent(transform);
                rect.offsetMax = Vector2.zero;
                rect.offsetMin = Vector2.zero;
                rect.localScale = Vector2.one;
                Page p = newPage.GetComponent<Page>();
                RenderedPages.Add(p);
                Debug.Log("Adding "+pageID);
                break; //get out of the loop - no need to look any further when we find the page that needs to be added.
            }
        }
    }

    /// <summary>
    /// The RemovePage function will be called via Unity Button event.
    /// </summary>
    /// <param name="pageID"></param>
    public void RemovePage(string pageID)
    {
        for (int i = 0; i < RenderedPages.Count; i++)
        {
            if (RenderedPages[i].pageID == pageID) //If we find the page we are trying to remove with this function...
            {
                //We need to tell that page to run its exit function
                RenderedPages[i].Exit();
                break; //get out of the loop - no need to look any further when we find the page that needs to be removed.
            }
        }
    }

    /// <summary>
    /// Clean-up method that we will call every couple of frames.
    /// Removes any pages whose 'Active' bool is currently false.
    /// </summary>
    void RemoveInActivePages()
    {
        for (int i = RenderedPages.Count - 1; i >= 0; i--)
        {
            if (RenderedPages[i].Active == false)
            {
                Debug.Log("Removing "+RenderedPages[i].pageID);
                Destroy(RenderedPages[i].gameObject); //Remove the page game object from the game.
                RenderedPages.RemoveAt(i); //Remove the page reference from the list.
            }
        }
    }

    /// <summary>
    /// Removes all pages currently active. 
    /// This will likely only be used when switching scenes.
    /// </summary>
    public void RemoveAllPages()
    {
        for (int i = 0; i < RenderedPages.Count; i++)
        {
            //We need to tell this page to run its exit function
            RenderedPages[i].Exit();
        }
    }
}
