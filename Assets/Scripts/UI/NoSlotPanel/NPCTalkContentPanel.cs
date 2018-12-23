using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCTalkContentPanel : BasePanel<NPCTalkContentPanel>
{
    private Text RoleName;
    private Text Content;
    public override void Start()
    {
        base.Start();
        RoleName = UITool.FindChild<Text>(gameObject, "RoleName");
        Content = UITool.FindChild<Text>(gameObject, "Content");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Hide();
        }
    }

    public void ShowContent(string name,string content)
    {
        gameObject.SetActive(true);
        Show();
        RoleName.text = name;
        Content.text = content;
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        gameObject.SetActive(false);
    }

}
