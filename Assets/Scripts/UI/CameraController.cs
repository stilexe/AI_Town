using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform supermarketPosition, cornerPosition, hoverPosition;
    [SerializeField] private Vector3 followPosition;
    [SerializeField] private Quaternion followRotation;
    [SerializeField] private TextMeshProUGUI displayHeader;
    [SerializeField] private GameObject descriptionPanel; 
    

    private bool _following;
    //private GameObject _followTarget;
    private BehaviourDisplay _followDisplay;
    

    public void ShowSupermarket()
    {
        if (_following)
        {
            Unfollow();
        }
        
        transform.position = supermarketPosition.position;
        transform.rotation = supermarketPosition.rotation;
    }

    public void ShowCorner()
    {
        if (_following)
        {
            Unfollow();
        }
        
        transform.position = cornerPosition.position;
        transform.rotation = cornerPosition.rotation;
    }

    public void ShowHover()
    {
        if (_following)
        {
            Unfollow();
        }
        
        transform.position = hoverPosition.position;
        transform.rotation = hoverPosition.rotation;
    }

    public void FollowRandom(string targetTag)
    {
        Debug.Log("Finding random " + targetTag);
        GameObject[] characters = GameObject.FindGameObjectsWithTag(targetTag);
        Follow(characters[Random.Range(0, characters.Length)]);
    }

    public void Follow(GameObject target)
    {
        if (_following)
        {
            transform.parent = null; 
        }
        else
        {
            _following = true;
        }
        
        transform.parent = target.transform;
        transform.localPosition = followPosition;
        transform.localRotation = followRotation;
        
        _followDisplay = target.GetComponent<BehaviourDisplay>();
        _followDisplay.StartDisplay();
        displayHeader.text = _followDisplay.GetDisplayName();
        UpdateDescription();
    }

    private void Unfollow()
    {
        _following = false;
        _followDisplay.StopDisplay();
        transform.parent = null;
    }

    public void ChangeFollowDisplay()
    {
        if (_following)
        {
            _followDisplay.NextBehaviour();
            displayHeader.text = _followDisplay.GetDisplayName();
            UpdateDescription();
        }
    }

    public void DisplayFollowingPath()
    {
        if (_following)
        {
            _followDisplay.DisplayPath();
        }
    }

    private void UpdateDescription()
    {
        List<string> description = _followDisplay.GetDescription();
        
        for (int i = 0; i < descriptionPanel.transform.childCount; i++)
        {
            if (i >= description.Count) //not a description
            {
                descriptionPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                descriptionPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = description[i];
            }
        }
    }

}
