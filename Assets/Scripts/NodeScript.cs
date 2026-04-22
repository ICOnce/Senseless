using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour, IPointerClickHandler
{
    private Image nodeImage;

    public Color lockedColor = Color.grey;
    public Color unlockedColor = Color.green;

    public StatType stat;
    public int statAmount;

    private bool isUnlocked = false;
    public GameObject? conditionNode;
    public List<GameObject> childNodes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nodeImage = GetComponent<Image>();
        nodeImage.color = lockedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (conditionNode == null)
            {
                LearnNode();
            }
            if (conditionNode.GetComponent<Image>().color == unlockedColor)
            {
                LearnNode();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UnlearnNode();
        }
    }


    void LearnNode()
    {
        if (isUnlocked) return;

        PlayerStats.Learn(stat, statAmount);

        isUnlocked = true;
        nodeImage.color = unlockedColor;
    }

    void UnlearnNode()
    {
        if (!isUnlocked) return;

        foreach (GameObject child in childNodes)
        {
            child.GetComponent<NodeScript>().UnlearnNode();
        }

        PlayerStats.UnLearn(stat, statAmount);
        isUnlocked = false;
        nodeImage.color = lockedColor;
    }
}
