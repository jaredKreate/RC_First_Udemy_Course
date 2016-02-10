using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//© 2016 RENAISSANCE CODERS ALL RIGHTS RESERVED

/// <summary>
/// The UIButton_PageLoader class is responsible for receiving click events to inform the Menu Manager which page to load.
/// It implements the IPointerClick interface.
/// </summary>
public class UIButton_PageLoader : MonoBehaviour, IPointerClickHandler {

    public string pageID = "Enter Page to Load Here.";

    MenuManager menu;

    //Unity calls this function for us
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
    }

    /// <summary>
    /// This is a click method we must implement using the IPointerClickHandler interface.
    /// This listens for clicks on our button so we may take appropriate actions.
    /// </summary>
    /// <param name="ped"></param>
    public void OnPointerClick(PointerEventData ped)
    {
        menu.AddPage(pageID);
    }
}
