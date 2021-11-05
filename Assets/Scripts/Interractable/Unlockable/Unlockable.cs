using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour, IUnlockable
{
    [SerializeField] private Puzzle puzzle;
    private bool isUnlock;

    public bool IsUnlock {
        get
        {
            bool res = false;
            if (puzzle.PuzzlePieces != null)
            {
                if (puzzle.IsPuzzleSolved) { res = true;}
            }
            return res;
        }
    }
        

    void Start()
    {
        isUnlock = false;
    }
}
