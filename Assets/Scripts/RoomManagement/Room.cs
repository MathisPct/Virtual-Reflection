using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Attributes
    [SerializeField] private Openable exitDoor;
    [SerializeField] private Room nextRoom;
    [SerializeField] private Room previousRoom;
    [SerializeField] private List<Puzzle> puzzles = new List<Puzzle>();



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
            if (exitDoor != null)
            {

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
    void Start()
    {
        exitDoor = GetComponentInChildren<Openable>();
    }

    private void OnChangingRoom()
    {

    }



}
