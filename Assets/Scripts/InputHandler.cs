using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoSingleton<InputHandler>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform clickIndicator;
    //private Slot lastSelectedSlot => tileManager.LastSlot;
    private bool isDragging;
    private TileManager tileManager;
    public bool CanGetInput = true;

    public void Initialize()
    {
        tileManager = TileManager.instance;
    }

    public void Tick()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && CanGetInput)
        {
            StartSelection();
        }
        else if (isDragging && Input.GetMouseButton(0) && CanGetInput)
        {
            Drag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSelection();
        }
    }

    private void StartSelection()
    {
        /*
        var slot = Detect();
        if (slot != null && slot.hasTile)
        {
            isDragging = true;
            slot.Select();
        }
        */
    }

    private void Drag()
    {
        /*
        var slot = Detect();
        if (slot != null && slot.hasTile)
        {
            slot.Select();
        }
        */
    }

    private void EndSelection()
    {
        //tileManager.FingerUp();
    }

    private Slot Detect()
    {
        /*
        var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
        Ray ray = mainCamera.ScreenPointToRay(pos);
        RaycastHit hit;

        if (tileManager.CanSelect)
        {
            var indicatorPos = mainCamera.ScreenToWorldPoint(pos);
            indicatorPos.z = 0;
            clickIndicator.position = indicatorPos;
            lastSelectedSlot?.UpdateLineRendererEnd(clickIndicator.position);
        }

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            return hit.collider.GetComponent<Slot>();
        }
        */
        return null;
    }
}
