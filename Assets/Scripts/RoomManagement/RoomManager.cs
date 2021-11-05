using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] private Room[] rooms;

    public Room[] Rooms { get; set; }

    void Start()
    {
        rooms = GetComponentsInChildren<Room>();
        RoomChain();
    }

    void Update()
    {

    }

    private void RoomChain()
    {
        int nbRooms = rooms.Length;
        for (int i = 0; i < nbRooms-1; i++)
        {
            rooms[i].NextRoom = rooms[i+1];
            rooms[i+1].PreviousRoom = rooms[i];
        }
    }

}
