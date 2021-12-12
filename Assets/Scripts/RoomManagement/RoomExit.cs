using Assets.Scripts.TeleportationManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class RoomExit : XRBaseInteractable, IAwareness
{
    [SerializeField] private string nextLevelName;

    public void BehaviourWhenPlayerEnter()
    {
        if(nextLevelName != null)
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    public void BehaviourWhenPlayerExit()
    {

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        BehaviourWhenPlayerEnter();
    }

}
