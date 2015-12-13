using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// The UIButton_SceneLoader class is responsible for receiving click events to load our game scenes.
/// It implements the IPointerClick interface.
/// </summary>
public class UIButton_SceneLoader : MonoBehaviour, IPointerClickHandler {

    public string sceneID = "Enter Scene to Load Here.";

    /// <summary>
    /// This is a click method we must implement using the IPointerClickHandler interface.
    /// This listens for clicks on our button so we may take appropriate actions.
    /// </summary>
    /// <param name="ped"></param>
    public void OnPointerClick(PointerEventData ped)
    {
        Application.LoadLevel(sceneID);
    }
}
