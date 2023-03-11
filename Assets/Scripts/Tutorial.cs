using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Texture[] texture;
    [SerializeField] Button nextButton, prevButton;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RawImage>().texture = texture[index];
    }

    // Update is called once per frame
    void Update()
    {
        if(index == 0)
        {
            prevButton.gameObject.SetActive(false);
        }else if(index == texture.Length-1)
        {
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
        }
        
    }

    public void NextSlide()
    {
        if(index != texture.Length)
        {
            index++;
            GetComponent<RawImage>().texture = texture[index];
        }
            
    }
    public void PrevSlide()
    {
        if(index != 0)
        {
            index--;
            GetComponent<RawImage>().texture = texture[index];
        }
            
        Debug.Log(index);
    }

    public void CloseTutorial()
    {
        gameObject.SetActive(false);
    }
}
