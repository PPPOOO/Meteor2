using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscPanel : BasePanel<EscPanel>
{

    public override void Start()
    {
        base.Start();
        Button MenuBtn = UITool.FindChild<Button>(gameObject, "MenuBtn");
        Button QuitBtn = UITool.FindChild<Button>(gameObject, "QuitBtn");

        MenuBtn.onClick.AddListener(OnMenuBtnClick);
        QuitBtn.onClick.AddListener(OnQuitBtnClick);
    }

    private void OnMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }
    private void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
