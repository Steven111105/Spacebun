using System;
        using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [Serializable]
    public class Dialogues{
        public string pose;
        public Sprite page;
    }
    [SerializeField]
    public Dialogues[] dialogues;
    public GameObject human;
    Animator animator;
    public GameObject tutorialCanvas;

    public GameObject dialoguePanel;
    SpriteRenderer spriteRenderer;

    int currentPage = 0;
    public GameObject backButton;
    private void OnEnable()
    {
        tutorialCanvas.SetActive(false);
        animator = human.GetComponent<Animator>();
        spriteRenderer = dialoguePanel.transform.GetChild(0).GetComponent<SpriteRenderer>();
        
    }

    public void StartPages(){
        PlayerPrefs.SetInt("HasSeenTutorial", 1);
        tutorialCanvas.SetActive(true);
        currentPage = 0;
        backButton.SetActive(false);
        spriteRenderer.sprite = dialogues[currentPage].page;
        animator.Play(dialogues[currentPage].pose);
    }

    public void NextPage(){
        currentPage++;
        Debug.Log("Next Page " + currentPage);
        if(currentPage >= dialogues.Length){
            currentPage = 0;
            tutorialCanvas.SetActive(false);
            GetComponent<MainMenuManager>().LevelSelect();
            return;
        }
        spriteRenderer.sprite = dialogues[currentPage].page;
        animator.Play(dialogues[currentPage].pose);
    }

    public void PrevPage(){
        currentPage--;
        if(currentPage < 0){
            currentPage = 0;
            backButton.SetActive(false);
            return;
        }
        backButton.SetActive(true);
        spriteRenderer.sprite = dialogues[currentPage].page;
        animator.Play(dialogues[currentPage].pose);
    }
}
