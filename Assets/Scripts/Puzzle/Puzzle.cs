using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour, IActivable
{
    [SerializeField] private Room room;
    [SerializeField] private List<GameObject> puzzlePieces = new List<GameObject>();
    public bool IsActivated { get => IsPuzzleSolved; }
    public bool IsPuzzleSolved 
    { get
        {
            bool puzzlePiecesAreActivated = true;
            if(puzzlePieces.Count > 0)
            {
                foreach (GameObject puzzle in puzzlePieces)
                {
                    var selection = puzzle.GetComponent<IActivable>();
                    if (selection != null)
                    {
                        if (!selection.IsActivated)
                        {
                            puzzlePiecesAreActivated = false;
                        }
                    }

                }
            }
            return puzzlePiecesAreActivated;
        } 
    }

    public List<GameObject> PuzzlePieces { get => puzzlePieces; set => puzzlePieces = value; }

    public void AddPuzzlePiece(GameObject puzzlePiece)
    {
        puzzlePieces.Add(puzzlePiece);
    }
}


