using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Attributes
    //[SerializeField] private Openable exitDoor;
    [SerializeField] private Room nextRoom;
    [SerializeField] private Room previousRoom;
    [SerializeField] private List<Puzzle> puzzles = new List<Puzzle>();
    [SerializeField] private IShadable emissiveLightShading;


    private bool isVisited;
    public bool isUnlocked;
    #endregion

    #region propreties
    public bool IsVisited { get; set; }
    public bool IsUnlocked
    {
        get
        {
            bool res = false;
            if (puzzles != null)
            {
                if(puzzles.Count > 0)
                {
                    res = true;
                    foreach (Puzzle puzzle in puzzles)
                    {
                        if (!puzzle.IsActivated)
                        {
                            res = false;
                        }
                    }
                }

            }
            return res;
        }
        set
        {
            isUnlocked = value;         
        }
    }

    public Room NextRoom { get; set; }
    public Room PreviousRoom { get; set; }
    #endregion


    // Start is called before the first frame update
    void Awake()
    {
        //exitDoor = GetComponentInChildren<Openable>();
        emissiveLightShading = GetComponent<IShadable>();
    }

    private void OnChangingRoom()
    {

    }

    void Update()
    {
        if (IsUnlocked == true)
        {
            emissiveLightShading.OnColorize();
        } else
        {
            emissiveLightShading.OnDecolorize();
        }
    }

}
