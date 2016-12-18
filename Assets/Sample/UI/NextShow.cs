using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class NextShow : TTUIPage {
    public NextShow()
        : base(UIType.PopUp, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIPrefab/NextShow";
    }
    GameObject left_bu;
    GameObject left_panel;
    GameObject down_panel;
    GameObject down_panel_items;
    Image temp;
   //public Texture co_texture;
   //public string co_name;

    public override void Awake(GameObject go)
    {
        this.transform.Find("ConfigB").GetComponent<Button>().onClick.AddListener(Config);
        this.transform.Find("ExitB").GetComponent<Button>().onClick.AddListener(Hide);
        this.transform.Find("ExitB").GetComponent<Button>().onClick.AddListener(delegate(){ TTUIPage.ShowPage<UIMainPage>();});
        left_panel = this.transform.Find("LeftPanel/Scroll View").gameObject;
        left_bu = left_panel.transform.Find("Viewport/Content/Button").gameObject;
        down_panel = this.transform.Find("DownPanel/Scroll View").gameObject;
        down_panel_items = down_panel.transform.Find("Viewport/Content/DPanelitem").gameObject;
        //down_panel.AddComponent<Contentsize>();
        //dropd = this.transform.Find("UPPanel/Sort/SortDropdown").GetComponent<Dropdown>();
        //dropd.onValueChanged.AddListener(OnValueChange);
        //testtext = this.transform.Find("UPPanel/InputFieldSearch").GetComponentInChildren<Text>();
        //dropd.AddOptions(inside);
    }
    private Text testtext;
    private Dropdown dropd;
    List<string> inside = new List<string> { "sdadsa", "123daq", "点卡和万科", "!@^&@#", "科技" };
    public void OnValueChange(int a) 
    {
        Debug.Log(testtext == null);
        testtext.text = a.ToString();
        
    }


    public override void Refresh()
    {
        down_panel_items.SetActive(false);
        down_panel.transform.localScale = Vector3.zero;
        down_panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        SolutionItt Solut = this.data != null ? this.data as SolutionItt : GameData.Instance.playersolutions;
        for (int i = 0; i < Solut.Solut_data.Count; i++)
        {
            GameObject g = GameObject.Instantiate(down_panel_items) as GameObject;
            g.SetActive(true);
            g.transform.Find("ProductNameText").GetComponent<Text>().text = Solut.Solut_data[i].name_main;
            g.transform.localPosition = Vector3.zero;
            g.transform.SetParent(down_panel_items.transform.parent);
            GameObject gg = g.transform.Find("OutImage/InImage").gameObject;
            Image imag = gg.GetComponent<Image>();
            imag.preserveAspect = true;
            Texture2D t2d = Solut.Solut_data[i].tture_main as Texture2D;
           // imag.overrideSprite = Sprite.Create(t2d, new Rect(new Vector2(0, 0)
            //    ,new Vector2(Solut.Solut_data[i].tture_main.width,Solut.Solut_data[i].tture_main.height)),new Vector2(0.5f,0.5f));
            gg.AddComponent<SolutionData>();
            gg.AddComponent<Button>().onClick.AddListener(Status);
        }
    }

    void CreateDites() 
    {

    }

    public void Status() 
    {
         GameObject go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
         SolutionData sd = go.GetComponent<SolutionData>();
         if (sd != null)
         {
             //Debug.Log(sd.ishit);
             
             sd.ishit = sd.ishit == false ? true : false;
             if (sd.ishit)
             {
                 if (temp != null)
                 {
                       temp.color = Color.white;
                       temp.GetComponentInChildren<SolutionData>().ishit = false;
                 }
                 so.tec = go.transform.GetComponent<Image>().mainTexture;
                 so.name = go.transform.parent.parent.Find("ProductNameText").GetComponent<Text>().text;
                 go.transform.parent.GetComponent<Image>().color = Color.blue;
             }
             else {
                 go.transform.parent.GetComponent<Image>().color = Color.white; 
             }
         }
         else {
             Debug.Log("没有SolutionData脚本");
         }
         temp = go.transform.parent.GetComponent<Image>();
    }
    public solut so = new solut();
    public void Config() 
    {
        Hide();
//        NextShow NS=new NextShow();
//        NS.co_name = co_name;
//        NS.co_texture = co_texture;
        if (so != null)
        {
          //  ShowPage<thisShow>(so);
           // data = NS as object;
          //  Debug.Log("if");
        }
      //  Debug.Log("else");
    }



    //public override void Refresh()
    //{
    //    //default desc deactive
    //    left_bu.SetActive(false);

    //    /*skilllist出现时的放大动画*/
    //    left_panel.transform.localScale = Vector3.zero;
    //    left_panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f);

    //    //Get Skill Data.
    //    //NOTE:here,maybe you havent Show(...pageData),ofcause you can got your skill data from your data singleton
    //    /*如果这里没有给data赋值，就从GameData的构造中取出一个UDSkill的实例*/
    //    UDBu BuData = this.data != null ? this.data as UDBu : GameData.Instance.playerleftButtons;
    //    //create skill items in list.
    //    /*  在需要接受点击后产生子物体的button上添加脚本
    //     *  该脚本需要一个list用于记录该gameobject需要控制的物体的列表，动态添加到每个脚本所对应的列表里
    //     *  同样，在GameData脚本里也许要建立关联
    //     */
    //    for (int i = 0; i < BuData.bulist.Count; i++)
    //    {
    //        GameObject g = CreateSkillItem(BuData.bulist[i], false);
    //        ButtonData bd = g.AddComponent<ButtonData>();
    //        for (int j = 0; j < BuData.bulist[i].bu_sub_list.Count; j++)
    //        {
    //            GameObject gg = CreateSkillItem(BuData.bulist[i].bu_sub_list[j], true);
    //            bd.data.Add(gg);
    //        }
    //    }

    //}

    //private GameObject CreateSkillItem(UDBu.Bu b,bool ischild)
    //{
    //    /*skillitem已经是他的子物体*/
    //    GameObject go = GameObject.Instantiate(left_bu) as GameObject;
    //    if (!ischild)
    //    {
    //        go.SetActive(true);
    //        go.GetComponent<Button>().onClick.AddListener(OnClickGoBattle);
    // //       ButtonData bd = go.AddComponent<ButtonData>();
    //    }
    //    else 
    //    {
    //        go.SetActive(false);
    //    }
    //    go.transform.SetParent(left_bu.transform.parent);
    //    go.transform.localScale = Vector3.one;
    //    go.GetComponentInChildren<Text>().text = b.name;
    //    return go;
    //   // UISkillItem item = go.AddComponent<UISkillItem>();
    //    //item.Refresh(skill);
    //    //skillItems.Add(item);

    //    //add click btn
    //}
    //private void OnClickGoBattle()
    //{
    //    GameObject go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    //    ButtonData bd = go.GetComponent<ButtonData>();

    //    bd.ishit = bd.ishit == false ? true : false;
    //   // ison = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.activeSelf;
    //    if (bd.ishit)
    //    {
    //        for (int i = 0; i < bd.data.Count; i++)
    //        {
    //            if (!bd.data[i].GetComponent<Text>())
    //            {
    //                bd.data[i].gameObject.SetActive(true);
    //            }
    //        }
    //    }
    //    else {
    //        for (int i = 0; i < bd.data.Count; i++)
    //        {
    //            if (!bd.data[i].GetComponent<Text>())
    //            {
    //                bd.data[i].gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //    Debug.Log("没写东西!");
    //}

    //public override void Refresh()
    //{
    //    //default desc deactive
    //    skillDesc.SetActive(false);

    //    skillList.transform.localScale = Vector3.zero;
    //    skillList.transform.DOScale(new Vector3(1, 1, 1), 0.5f);

    //    //Get Skill Data.
    //    //NOTE:here,maybe you havent Show(...pageData),ofcause you can got your skill data from your data singleton
    //    UDSkill skillData = this.data != null ? this.data as UDSkill : GameData.Instance.playerSkill;

    //    //create skill items in list.
    //    for (int i = 0; i < skillData.skills.Count; i++)
    //    {
    //        CreateSkillItem(skillData.skills[i]);
    //    }

    //}


    //private void CreateSkillItem(UDSkill.Skill skill)
    //{
    //    GameObject go = GameObject.Instantiate(skillItem) as GameObject;
    //    go.transform.SetParent(skillItem.transform.parent);
    //    go.transform.localScale = Vector3.one;
    //    go.SetActive(true);

    //    UISkillItem item = go.AddComponent<UISkillItem>();
    //    item.Refresh(skill);
    //    skillItems.Add(item);

    //    //add click btn
    //    go.AddComponent<Button>().onClick.AddListener(OnClickSkillItem);
    //}
}
