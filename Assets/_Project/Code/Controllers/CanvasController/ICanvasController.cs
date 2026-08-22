using System.Collections.Generic;
using UnityEngine;

public interface ICanvasController
{
    public void TurnOnCanvases(int[] pos);
    public void TurnOnCanvases(HashSet<string> names);
    public void TurnOnCanvas(int pos);
    public void TurnOnCanvas(string name);
    public void TurnOffCanvases(int[] pos);
    public void TurnOffCanvases(HashSet<string> names);
    public void TurnOffCanvas(int pos);
    public void TurnOffCanvas(string name);
}
