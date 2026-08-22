using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour, ICanvasController
{

    [SerializeField] private  Canvas[] canvases;


    // used hashset for O(1) search
    public void TurnOnCanvases(int[] pos)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            canvases[pos[i]].enabled = true;
        }
    }

    public void TurnOnCanvases(HashSet<string> names)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (names.Contains(canvases[i].name)) 
                canvases[i].enabled = true;
        }
    }

    public void TurnOnCanvas(int pos)
    {
        canvases[pos].enabled = true; 
    }

    public void TurnOnCanvas(string name)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (name== canvases[i].name) 
                canvases[i].enabled = true;
        }
    }

    public void TurnOffCanvases(int[] pos)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            canvases[pos[i]].enabled = false;
        }
    }

    public void TurnOffCanvases(HashSet<string> names)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (names.Contains(canvases[i].name)) 
                canvases[i].enabled = false;
        }
    }

    public void TurnOffCanvas(int pos)
    {
        canvases[pos].enabled = true; 
    }

    public void TurnOffCanvas(string name)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (name== canvases[i].name) 
                canvases[i].enabled = false;
        }
    }

}
