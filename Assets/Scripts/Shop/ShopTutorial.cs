using UnityEngine;

public class ShopTutorial : MonoBehaviour
{
    [System.Serializable]
    public class Dialogues{
        public string pose;
        public Sprite page;
    }
    public Dialogues[] story;
    public GameObject storyCanvas;
    public GameObject dialoguePanel;
    SpriteRenderer spriteRenderer;
    public GameObject human;
    Animator animator;
    int currentPage;
    public GameObject backbutton;
    private void OnEnable()
    {
        animator = human.GetComponent<Animator>();
        spriteRenderer = dialoguePanel.transform.GetChild(0).GetComponent<SpriteRenderer>();
        if(PlayerPrefs.GetInt("FirstTimeShop", 0) == 1)
        {
            storyCanvas.SetActive(true);
            PlayerPrefs.SetInt("FirstTimeShop", 2);
            StartStory();
        }else{
            storyCanvas.SetActive(false);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && storyCanvas.activeSelf){
            NextPage();
        }
    }

    public void StartStory(){
        storyCanvas.SetActive(true);
        currentPage = 0;
        backbutton.SetActive(false);
        spriteRenderer = dialoguePanel.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = story[currentPage].page;
        animator.Play(story[currentPage].pose);
    }

    public void NextPage(){
        currentPage++;
        if(currentPage >= story.Length){
            currentPage = 0;
            storyCanvas.SetActive(false);
            return;
        }
        backbutton.SetActive(true);
        spriteRenderer.sprite = story[currentPage].page;
        animator.Play(story[currentPage].pose);
    }

    public void PrevPage(){
        currentPage--;
        if(currentPage <= 0){
            currentPage = 0;
            backbutton.SetActive(false);
            spriteRenderer.sprite = story[currentPage].page;
            return;
        }
        backbutton.SetActive(true);
        spriteRenderer.sprite = story[currentPage].page;
        animator.Play(story[currentPage].pose);
    }

}
