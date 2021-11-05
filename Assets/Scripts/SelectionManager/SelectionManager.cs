using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    [SerializeField] private XRInteractionManager manager;

    private ISelectionResponse _selectionResponse;

    [SerializeField] private Transform _selection;
    [SerializeField] private Transform _lastSelection;

    [SerializeField] private GameObject truc;

    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    public Transform getSelection()
    {
        return _selection;
    }

    public Transform getLastSelection()
    {
        return _lastSelection;
    }

    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }

        _selection = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                if(_lastSelection != selection)
                {
                    _selection = selection;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    _lastSelection = selection;                 
                }
                Debug.Log("cursor selection ID:" + _selection.gameObject.name);                
            }
        }

        if (_selection != null)
        {
             _selectionResponse.OnSelect(_selection);           
        }
    }
}
