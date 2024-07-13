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
    bool fromPlay;
    private void OnEnable()
    {
        tutorialCanvas.SetActive(false);
        animator = human.GetComponent<Animator>();
        spriteRenderer = dialoguePanel.transform.GetChild(0).GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && tutorialCanvas.activeSelf){
            NextPage();
        }
    }

    public void StartPages(bool isPlay){
        fromPlay = isPlay;
        PlayerPrefs.SetInt("HasSeenTutorial", 1);
        GetComponent<MainMenuManager>().hasSeenTutorial = true;
        tutorialCanvas.SetActive(true);
        human.SetActive(true);
        currentPage = 0;
        backButton.SetActive(false);
        spriteRenderer.sprite = dialogues[currentPage].page;
        animator.Play(dialogues[currentPage].pose);
    }

    public void NextPage(){
        currentPage++;
        // Debug.Log("Next Page " + currentPage);
        if(currentPage >= dialogues.Length){
            currentPage = 0;
            tutorialCanvas.SetActive(false);
            if(fromPlay){
                GetComponent<MainMenuManager>().LevelSelect();
            }
            return;
        }
        backButton.SetActive(true);
        spriteRenderer.sprite = dialogues[currentPage].page;
        animator.Play(dialogues[currentPage].pose);
    }

    public void PrevPage(){
        currentPage--;
        if(currentPage <= 0){
            currentPage = 0;
            backButton.SetActive(false);
            return;
        }
        backButton.SetActive(true);
        spriteRenderer.sprite = dialogues[currentPage].page;
        animator.Play(dialogues[currentPage].pose);
    }
}
