using UnityEngine;
using UnityEngine.UI;

public class TextTranslate : MonoBehaviour
{
    [SerializeField] string ID;
    string myText = "";

    private LangHandler langHandler;
    private Text myView;

    void Awake()
    {
        langHandler = FindObjectOfType<LangHandler>();
        myView = GetComponent<Text>();

        myText = myView.text;
        langHandler.OnUpdate += ChangeLang;
    }

    private void Start()
    {
        ChangeLang();
    }

    void ChangeLang()
    {
        myView.text = langHandler.GetTranslate(ID);
    }
}
