using UnityEngine;
using TMPro;

public class name1 : MonoBehaviour
{
    [Header("Name Tag Settings")]
    public string npcName = "Nick";
    public float tagHeight = 2f;
    public float fontSize = 0.1f;

    private TextMeshPro nameText;

    void Start()
    {
        CreateNameTag();
    }

    void CreateNameTag()
    {
        // Create a GameObject for the name tag
        GameObject tagObject = new GameObject("NameTag");
        tagObject.transform.SetParent(transform);
        tagObject.transform.localPosition = new Vector3(0, tagHeight, 0);

        // Add TextMeshPro component
        nameText = tagObject.AddComponent<TextMeshPro>();

        // Configure the text
        nameText.text = npcName;
        nameText.fontSize = fontSize;
        nameText.alignment = TextAlignmentOptions.Center;
        nameText.color = Color.white;

        // Make text always face camera (optional)
        tagObject.AddComponent<Billboard>();
    }
}

// Simple billboard script to always face camera
public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Flip to face properly
    }
}