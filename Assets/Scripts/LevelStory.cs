using UnityEngine;

public class LevelStory : MonoBehaviour
{
    [System.Serializable]
    public class Dialogues{
        public string pose;
        public Sprite page;
    }
    [System.Serializable]
    public class Story{
        public string levelName;
        public Dialogues[] dialogues;
    }
    public Story[] stories;
    public GameObject storyCanvas;
    public GameObject dialoguePanel;
    SpriteRenderer spriteRenderer;
    public GameObject human;
    Animator animator;
    int currentPage;
    int levelIndex;
    private void OnEnable()
    {
        storyCanvas.SetActive(false);
        animator = human.GetComponent<Animator>();
        spriteRenderer = dialoguePanel.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void StartStory(int level){
        levelIndex = level;
        PlayerPrefs.SetInt("HasSeenLvl" + (level+1) + "Story", 1);
        storyCanvas.SetActive(true);
        currentPage = 0;
        spriteRenderer = dialoguePanel.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stories[level].dialogues[currentPage].page;
        animator.Play(stories[level].dialogues[currentPage].pose);
    }

    public void NextPage(){
        currentPage++;
        if(currentPage >= stories[levelIndex].dialogues.Length){
            currentPage = 0;
            GetComponent<ShopUIManager>().PlayButton();
            return;
        }
        spriteRenderer.sprite = stories[levelIndex].dialogues[currentPage].page;
        animator.Play(stories[levelIndex].dialogues[currentPage].pose);
    }


}
