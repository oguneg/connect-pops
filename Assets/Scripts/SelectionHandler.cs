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

        if (slot.IsSelected)
        {
            if (selectionLength > 1)
            {
                if (slot == selection[selectionLength - 2])
                {
                    lastSelection.Deselect();
                    selection.Remove(lastSelection);
                    selectionLength--;
                    return;
                }
            }
            return;
        }

        selection.Add(slot);
        selectionLength++;
        slot.Select();
    }

    public void EndSelection()
    {
        if (selectionLength < 2)
        {
            for (int i = 0; i < selectionLength; i++)
            {
                selection[i].Deselect();
            }
        }
    }
}
