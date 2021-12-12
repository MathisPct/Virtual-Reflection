using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Openable : MonoBehaviour, IOpenable
{
    private bool isOpen = false;
    [SerializeField] protected Animator animator;

    public virtual bool OpeningCondition{ get; }

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        OpenableBehaviour();
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

    public void OpenableBehaviour()
    {
        if (OpeningCondition)
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
}
