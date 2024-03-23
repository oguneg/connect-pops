using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    public List<Slot> selection = new List<Slot>();
    public int selectionValue;
    public int selectionLength;
    public Slot lastSelection => selectionLength == 0 ? null : selection[selectionLength - 1];

    public void Select(Slot slot)
    {
        if (selectionLength == 0)
        {
            selectionValue = slot.tile.Value;
        }
        else if (slot.tile.Value != selectionValue)
        {
            return;
        }

        if (slot.IsSelected)
        {
            if (selectionLength > 1)
            {
                if (slot == selection[selectionLength - 2])
                {
                    lastSelection.Deselect();
                    selection.Remove(lastSelection);
                    selectionLength--;
                    lastSelection?.HideLineRenderer();
                    return;
                }
            }
            return;
        }

        lastSelection?.SetLineRendererEnd(slot);
        selection.Add(slot);
        selectionLength++;
        slot.Select();
    }

    public void EndSelection()
    {
        if (selectionLength < 2)
        {
            DeselectSelection();
        }
        else
        {
            MergeSelection();
        }
        selection.Clear();
        selectionLength = 0;
    }

    private void DeselectSelection()
    {
        for (int i = 0; i < selectionLength; i++)
        {
            selection[i].Deselect();
        }
    }

    private void MergeSelection()
    {
        var last = lastSelection;
        for (int i = 0; i < selectionLength - 1; i++)
        {
            selection[i].tile.MergeInto(last);
            selection[i].Deselect();
        }
        last.Deselect();
        TileManager.instance.IncreaseTileValue(last.tile);
    }
}
