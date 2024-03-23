using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoSingleton<InputHandler>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SelectionHandler selectionHandler;
    private Slot lastSelectedSlot;
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
        if (Input.GetMouseButton(0) && CanGetInput)
        {
            Drag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSelection();
        }
    }

    private void Drag()
    {
        var slot = Detect();
        if (slot != null && slot.tile != null)
        {
            selectionHandler.Select(slot);
        }
    }

    private void EndSelection()
    {
        selectionHandler.EndSelection();
    }

    private Slot Detect()
    {
        var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
        Ray ray = mainCamera.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            return hit.collider.GetComponent<Slot>();
        }

        return null;
    }
}
