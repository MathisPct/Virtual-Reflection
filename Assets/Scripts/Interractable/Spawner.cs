using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Puzzle puzzle;

    void Start()
    {
        GameObject puzzlePiece = Instantiate(prefab, this.transform.position, this.transform.rotation);
        puzzle.AddPuzzlePiece(puzzlePiece);
    }

}
