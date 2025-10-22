using System;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourDisplay : MonoBehaviour
{
    private int _displayedIndex;
    private List<ISteering> _steerings = new List<ISteering>();
    private Dictionary<Color, List<Vector3>> _lines;
    private List<LineRenderer> _lineRenderers = new List<LineRenderer>();
    private LineRenderer _pathLine; 
    private bool _displayOn;
    public List<string> GetDescription() { return _steerings[_displayedIndex].LineRenderDescription(); }


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

    public void StopDisplay()
    {
        _displayOn = false;
        ClearDisplay();
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

    public void DisplayPath()
    {
        if (TryGetComponent(out PathFollow follow)) 
        {
            List<Node> path = follow.GetPath();
            
            if (_pathLine == null)
            {
                GameObject newGo = new GameObject();
                newGo.transform.SetParent(transform);
                newGo.transform.localPosition = Vector3.zero;
        
                _pathLine = newGo.AddComponent<LineRenderer>();
                _pathLine.material = new Material(Shader.Find("Sprites/Default"));
                _pathLine.startColor = Color.magenta;
                _pathLine.endColor = Color.magenta;
            }

            _pathLine.positionCount = path.Count;

            for (int i = 0; i < path.Count; i++)
            {
                _pathLine.SetPosition(i, path[i].location);
            }
        }
        
    }

    public void ClearDisplay()
    {
        foreach (LineRenderer lr in _lineRenderers)
        {
            lr.positionCount = 0;
        }

        if (_pathLine)
        {
            _pathLine.positionCount = 0;
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
