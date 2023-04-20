using TMPro;
using UnityEngine;

public class MultipleChoiceManager : MonoBehaviour
{
    public static MultipleChoiceManager multipleChoiceManager;

    [HideInInspector] public TextMeshProUGUI[] text = new TextMeshProUGUI[5];

    private MultipleChoice multipleChoice = null;
    private GameObject player = null;

    private bool active;

    private void Awake()
    {
        if (multipleChoiceManager != null)
            Destroy(gameObject);
        else
            multipleChoiceManager = this;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>())
                text[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
        }

        if (!active)
            Deactivate();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && active)
            Deactivate();
    }
    public void Activate(MultipleChoice multipleChoiceController)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);

        multipleChoice = multipleChoiceController;
        multipleChoice.cam.SetActive(true);

        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(true);

        active = true;
    }
    public void Deactivate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (player != null)
            player.SetActive(true);
        player = null;

        if (multipleChoice != null)
            multipleChoice.cam.SetActive(false);
        multipleChoice = null;

        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);

        active = false;
    }
    public void Answer(int buttonClicked)
    {
        multipleChoice.AnswerQuestion(buttonClicked);
    }
}
