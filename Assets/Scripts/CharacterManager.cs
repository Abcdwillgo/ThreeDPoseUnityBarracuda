using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    int characterIndex;
    string CharacterName;
    
    public TextMeshProUGUI characterName;
    public Button left, right;
    public Image characterImage;
    public Sprite[] characterImages;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnClickLeft()
    {
        if (characterIndex == 0)
        {
            characterIndex = characterPrefabs.Length - 1;

        }
        else
        {
            characterIndex--;
        }
            characterImage.sprite = characterImages[characterIndex];
            CharacterName = characterPrefabs[characterIndex].name;
            characterName.text = CharacterName;
    }
    public void OnClickRight()
    {
        if (characterIndex == characterPrefabs.Length - 1)
        {
            characterIndex = 0;
        }
        else
        {
            characterIndex++;
        }
        characterImage.sprite = characterImages[characterIndex];
        CharacterName = characterPrefabs[characterIndex].name;
        characterName.text = CharacterName;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
