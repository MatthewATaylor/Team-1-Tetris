using System;
using System.Collections.Generic;
using UnityEngine;

public class FontRenderer : MonoBehaviour
{
    private const float characterWidth = 1;

    [SerializeField] private string initialText = "";
    [SerializeField] private int order = 0;
    [SerializeField] private GameObject border;
    [SerializeField] private List<char> characterChars;
    [SerializeField] private List<Sprite> characterSprites;

    // Dictionary of all characters supported by font
    private Dictionary<char, Sprite> characters = new Dictionary<char, Sprite>();

    // List of characters currently being displayed
    private List<GameObject> activeCharacters = new List<GameObject>();

    private float borderWidth = 0.0f;

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

        if (border != null)
        {
            borderWidth = border.GetComponent<Renderer>().bounds.size.x;
        }

        SetText(initialText);
    }

    void Update()
    {
        
    }

    public void SetText(string text)
    {
        RemoveActiveCharacters();

        float textWidth = text.Length * characterWidth;
        float firstCharacterX = transform.position.x - textWidth / 2.0f + characterWidth / 2.0f;

        // Add borders
        if (border != null)
        {
            // Left
            Vector3 leftBorderPosition = transform.position;
            leftBorderPosition.x = firstCharacterX - characterWidth / 2.0f - borderWidth / 2.0f;
            AddBorder(leftBorderPosition);

            // Right
            Vector3 rightBorderPosition = transform.position;
            rightBorderPosition.x += textWidth / 2.0f + borderWidth / 2.0f;
            AddBorder(rightBorderPosition);
        }

        // Update with new text
        for (int i = 0; i < text.Length; ++i)
        {
            Vector2 characterPosition = transform.position;
            characterPosition.x = firstCharacterX + i * characterWidth;
            CreateGameObjectForCharacter(text[i], characterPosition);
        }
    }

    private void CreateGameObjectForCharacter(char character, Vector3 position)
    {
        GameObject characterObject = new GameObject(character + " Character");
        characterObject.transform.position = position;
        characterObject.transform.parent = transform;
        characterObject.layer = gameObject.layer;  // Same layer as parent
        SpriteRenderer renderer = characterObject.AddComponent<SpriteRenderer>();
        characterObject.GetComponent<Renderer>().sortingOrder = order;
        try
        {
            renderer.sprite = characters[character];
        }
        catch (KeyNotFoundException)
        {
            throw new KeyNotFoundException("Key: '" + character + "' not found in FontRenderer");
        }
        activeCharacters.Add(characterObject);
    }

    private void AddBorder(Vector3 position)
    {
        GameObject borderObject = Instantiate(border, position, Quaternion.identity);
        borderObject.transform.parent = transform;
        borderObject.layer = gameObject.layer;
        borderObject.GetComponent<Renderer>().sortingOrder = order;
        activeCharacters.Add(borderObject);
    }

    private void RemoveActiveCharacters()
    {
        for (int i = 0; i < activeCharacters.Count; ++i)
        {
            Destroy(activeCharacters[0]);
            activeCharacters.RemoveAt(0);
        }
    }
}
