using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour, IActivable
{
    protected IShadable shadable;

    public bool isActivated;
    //[SerializeField] private bool hasBeenActivatedOnce;
    //[SerializeField] private bool isActivableOnce;

    #region Propreties
    public virtual bool IsActivated { get => isActivated; set => isActivated = value; }

    public void ActivationBehavior()
    {
        
    }

    #endregion
    void Awake()
    {
        var selectionShadable = GetComponent<IShadable>();
        if (selectionShadable != null)
        {
            shadable = selectionShadable;
        }
    }

    void Start()
    {

    }


    void Update()
    {
        ActivationBehavior();
        //shadable.OnColorize();
    }
}
