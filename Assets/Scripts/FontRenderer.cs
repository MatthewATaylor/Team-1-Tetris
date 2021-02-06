using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FontRenderer : MonoBehaviour
{
    private const float characterWidth = 1;

    [SerializeField] private string initialText = "";
    [SerializeField] private List<char> characterChars;
    [SerializeField] private List<Sprite> characterSprites;

    // Dictionary of all characters supported by font
    private Dictionary<char, Sprite> characters = new Dictionary<char, Sprite>();

    // List of characters currently being displayed
    private List<GameObject> activeCharacters = new List<GameObject>();

    void Start()
    {
        if (characterChars.Count != characterSprites.Count)
        {
            throw new Exception("Different character and sprite counts in FontRenderer");
        }

        // Convert separate char and sprite lists to dictionary for easier lookup
        for (int i = 0; i < characterChars.Count; ++i)
        {
            characters.Add(characterChars[i], characterSprites[i]);
        }

        SetText(initialText);
    }

    void Update()
    {
        
    }

    public void SetText(string text)
    {
        // Remove currently active characters
        for (int i = 0; i < activeCharacters.Count; ++i)
        {
            Destroy(activeCharacters[0]);
            activeCharacters.RemoveAt(0);
        }

        // Update with new text
        for (int i = 0; i < text.Length; ++i)
        {
            Vector2 characterPosition = transform.position;
            characterPosition.x += characterWidth * i;
            GameObject characterObject = CreateGameObjectForCharacter(text[i], characterPosition);
            activeCharacters.Add(characterObject);
        }
    }

    private GameObject CreateGameObjectForCharacter(char character, Vector3 position)
    {
        GameObject characterObject = new GameObject(character + " Character");
        characterObject.transform.position = position;
        characterObject.transform.parent = transform;
        SpriteRenderer renderer = characterObject.AddComponent<SpriteRenderer>();
        try
        {
            renderer.sprite = characters[character];
        }
        catch (KeyNotFoundException)
        {
            throw new KeyNotFoundException("Key: '" + character + "' not found in FontRenderer");
        }
        return characterObject;
    }
}
