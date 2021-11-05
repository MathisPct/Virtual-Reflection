using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour, IOpenable, IStartable, IAwakable
{
    [SerializeField] private bool isOpen = false;
    [SerializeField] private List<Puzzle> puzzles = new List<Puzzle>();
    [SerializeField] protected Animator animator = null;

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (ArePuzzlesSolved(puzzles))
        {
            if (!isOpen)
            {
                Open();
            }
                
        }
        else
        {
            if (isOpen)
            {
                Close();
            }
        }       
    }

    protected bool ArePuzzlesSolved(List<Puzzle> puzzles)
    {
        bool puzzlesSolved = true;
        foreach(Puzzle puzzle in puzzles)
        {
            if (!puzzle.IsPuzzleSolved)
            {
                puzzlesSolved = false;
            }
        }
        return puzzlesSolved;
    }

    public virtual void Open()
    {
        isOpen = true;
        animator.Play("Open", 0, 0.0f);
    }

    public virtual void Close()
    {
        isOpen = false;
        animator.Play("Close", 0, 0.0f);
    }

    public void OnAwake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnStart()
    {
        isOpen = false;  
    }
}
