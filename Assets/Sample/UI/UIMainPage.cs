using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainPage : TTUIPage {

    public UIMainPage() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIPrefab/UIMain";
    }

    public override void Awake(GameObject go)
    {
        this.transform.Find("btn_skill").GetComponent<Button>().onClick.AddListener(() =>
        {
            ShowPage<UISkillPage>();
        });

        this.transform.Find("btn_battle").GetComponent<Button>().onClick.AddListener(() =>
        {
            ShowPage<UIBattle>();
        });
    }

    //继承后，没有重写虚方法，调用的是父类的方法，如果有重写则是调用自己重写后的方法
  //  public override void Refresh() { Debug.Log("step,that"); }
    
}
