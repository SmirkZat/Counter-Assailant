using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class UITextGroup
{
    [Serializable]
    public struct TextElement
    {
        public string id;
        public TextMeshProUGUI textComponent;
    }

    [SerializeField] List<TextElement> textElementsSetup;
    Dictionary<string, TextMeshProUGUI> textElements = new Dictionary<string, TextMeshProUGUI>();

    public void Init()
    {
        foreach (TextElement element in textElementsSetup)
        {
            if (!textElements.ContainsKey(element.id))
            {
                textElements.Add(element.id, element.textComponent);
            }
            else
            {
                Debug.LogWarning($"Text element by the id of {element.id} already exists");
            }
        }
    }

    public void SetText(string id, string text)
    {
        if (textElements.ContainsKey(id))
        {
            textElements[id].text = text;
        }
        else
        {
            Debug.LogWarning($"text element by the id of {id} not found");
        }
    }
}
