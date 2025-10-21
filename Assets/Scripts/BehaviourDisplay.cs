using System;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourDisplay : MonoBehaviour
{
    private int _displayedIndex;
    private List<ISteering> _steerings = new List<ISteering>();
    private Dictionary<Color, List<Vector3>> _lines;
    private List<LineRenderer> _lineRenderers = new List<LineRenderer>();
    private bool _displayOn;


    public string GetDisplayName()
    {
        return _steerings[_displayedIndex].ToString();
    }

    private void OnEnable()
    {
        foreach (ISteering s in GetComponents<ISteering>())
        {
            _steerings.Add(s);
        }

        foreach (LineRenderer lr in GetComponents<LineRenderer>())
        {
            _lineRenderers.Add(lr);
        }
    }

    private void Start()
    {
        _displayedIndex = 0; 
    }

    private void FixedUpdate()
    {
        if (_displayOn)
        {
            DisplayBehaviour();
        }
    }

    public void StartDisplay()
    {
        _displayOn = true;
    }

    public void NextBehaviour()
    {
        ClearDisplay();
        
        _displayedIndex += 1;

        if (_displayedIndex >= _steerings.Count)
        {
            _displayedIndex = 0;
        }
    }

    private void DisplayBehaviour()
    {
        if (!_displayOn)
        {
            return; 
        }
        
        _lines = _steerings[_displayedIndex].LineRenderDisplay();

        int i = 0; 

        foreach(var line in _lines)
        {
            if (i >= _lineRenderers.Count)
            {
                NewLineRenderer();
            }
            
            _lineRenderers[i].startColor = line.Key;
            _lineRenderers[i].endColor = line.Key;
            
            _lineRenderers[i].positionCount = line.Value.Count;

            for(int j = 0; j < line.Value.Count; j++)
            {
                _lineRenderers[i].SetPosition(j, line.Value[j]);
            }

            i += 1; 
        }
    }

    public void ClearDisplay()
    {
        foreach (LineRenderer lr in _lineRenderers)
        {
            lr.positionCount = 0;
        }
    }

    private void NewLineRenderer()
    {
        GameObject newGo = new GameObject();
        newGo.transform.SetParent(transform);
        newGo.transform.localPosition = Vector3.zero;
        
        LineRenderer newRenderer = newGo.AddComponent<LineRenderer>();
        newRenderer.material = new Material(Shader.Find("Sprites/Default"));
        newRenderer.startWidth = .5f;
        newRenderer.endWidth = .5f;
        _lineRenderers.Add(newRenderer);
    }
}
