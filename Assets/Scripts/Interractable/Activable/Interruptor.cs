using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : Activable
{
    [SerializeField] private Activable[] activables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsActivated = AreInputsActivated();
    }

    public bool AreInputsActivated()
    {
        bool areInputsActivated = false;
        if(activables != null && activables.Length > 0)
        {
            areInputsActivated = true;
            foreach (Activable a in activables)
            {
                if (a != null)
                {
                    if (!a.IsActivated)
                    {
                        areInputsActivated = false;
                    }
                }
            }
        }
        return areInputsActivated;
    }
}
