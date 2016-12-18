using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

public class Homepanel : TTUIPage {

    public Homepanel()
        : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIPrefab/Homepanel";
    }

    public delegate void Slid(Vector3 ve);
    public delegate void Callback(GameObject g,bool iscallback,int calltemp);
    public Slid sli;
    public Callback call;
    public Dictionary<GameObject, GameObject> TopBottom = new Dictionary<GameObject,GameObject>();
    public List<GameObject> topitems = new List<GameObject>();
    public List<GameObject> bottomitems = new List<GameObject>();
    public const int homepItems = 5;
	private const int offsett = 51;
    public GameObject panelshow0, Panel_bottom;
    public GameObject show0Items,bottomItems;
	UDSkill skillData;


    public override void Awake(GameObject go)
    {
        sli = SlidTochange;
        call = LimitItems;
       MobilInput m_input= gameObject.AddComponent<MobilInput>();
       m_input.home = this;
        panelshow0 = this.transform.FindChild("Panel_show0").gameObject;
        show0Items = panelshow0.transform.FindChild("Hitems").gameObject;
        Panel_bottom = this.transform.FindChild("Panel_bottom").gameObject;
        bottomItems = Panel_bottom.transform.FindChild("Pitems").gameObject;
        show0Items.SetActive(false);
        bottomItems.SetActive(false);
    }

    public override void Refresh()
    {
		/*如果这里没有给data赋值，就从GameData的构造中取出一个UDSkill的实例*/
		skillData = this.data != null ? this.data as UDSkill : GameData.Instance.playerSkill;

        ForShortItems(panelshow0,show0Items);
        ForShortItems(Panel_bottom,bottomItems);
        for (int i = 0; i < topitems.Count; i++)
        {
//            Debug.Log(topitems.Count + ">>" + bottomitems.Count);
            TopBottom.Add(bottomitems[i], topitems[i]);
        }
        choise = bottomitems[0];
        LimitItems(bottomitems[2]);
        /*panelshow0出现时的放大动画*/
       // show0Items.SetActive(true);
        //Get Skill Data.
        //NOTE:here,maybe you havent Show(...pageData),ofcause you can got your skill data from your data singleton
    }


    void ForShortItems(GameObject parent,GameObject prefab) 
    {
        prefab.SetActive(true);
        parent.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        parent.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        //Debug.Log(panelshow0.transform.GetComponent<RectTransform>().rect.size.x
            //+ "show0Items.transform.GetComponent<RectTransform>().rect.size.x" + show0Items.transform.GetComponent<RectTransform>().rect.size.x);
        for (int i = 0; i < homepItems; i++)
        {
			
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            go.SetActive(true);
            go.transform.SetParent(parent.transform);

            RectTransform rect = go.GetComponent<RectTransform>();
            if (prefab == show0Items)
            {
//				Debug.Log (PageImage().Length);
				go.GetComponent<Image>().overrideSprite = PageImage()[i];
               // Debug.Log(Resources.Load("card_bg_big_" + i));
                rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);/*相对父节点左端对齐，相对距离，宽度*/
                rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);/*相对父节点上端对齐，相对距离，高度*/
                topitems.Add(go);
            }
            else 
            {
                rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, prefab.GetComponent<RectTransform>().sizeDelta.x);/*相对父节点左端对齐，相对距离，宽度*/
                rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, prefab.GetComponent<RectTransform>().sizeDelta.y);/*相对父节点上端对齐，相对距离，高度*/
                go.AddComponent<Button>().onClick.AddListener(delegate() { LimitItems(go); });
//				Debug.Log (go.name);
				go.transform.FindChild("TextUp").GetComponent<Image>().overrideSprite =  PageImage()[i];
//				Debug.Log (go.GetComponentInChildren<Text> ().text);
				go.GetComponentInChildren<Text> ().text = skillData.skills [i].name;
                bottomitems.Add(go);

				childoldpos = (go.transform.GetChild (0).transform.localPosition);
				/*只赋值一次*/
				if (shortbottem == 0)
				{
					shortbottem = rect.sizeDelta.x;
				}
				//Debug.Log("shortbottem" + shortbottem);

            }
            rect.anchorMax = prefab.GetComponent<RectTransform>().anchorMax;
            rect.anchorMin = prefab.GetComponent<RectTransform>().anchorMin;
            //rect.anchorMin = Vector2.zero;
            //rect.anchorMax = Vector2.one;
           
            rect.transform.localScale = Vector3.one;
            if (prefab == show0Items)
            {
                topitemslength = prefab.transform.GetComponent<RectTransform>().rect.size.x;
                go.transform.localPosition = new Vector3((i * topitemslength), 0, 0);

            }
            else {
                go.transform.localPosition
                    = new Vector3((i * prefab.transform.GetComponent<RectTransform>().rect.size.x), 0, 0);
            }
        }
        prefab.SetActive(false);
    }


	bool isresoureceimage=false;
	Sprite[] spr;
	Sprite[] PageImage()
	{
		if (!isresoureceimage) 
		{
		 spr = new Sprite[homepItems];
			for (int i = 0; i < homepItems; i++) 
			{
				spr[i]=Resources.Load("card_bg_big_"+i, typeof(Sprite)) as Sprite;
			}
		}
		return spr;
	}


   	public GameObject choise ;
    float shortbottem,topitemslength;
	Vector3 childoldpos;

    void LimitItems(GameObject cureentselect,bool iscall = false,int calltemp=0) 
    {
        //Debug.Log(cureentselect);
        //Debug.Log("bottomitems.IndexOf(cureentselect) + calltemp" + bottomitems.IndexOf(cureentselect) + calltemp);

        if (iscall)
        {
//            Debug.Log(bottomitems.IndexOf(choise)+">>>"+calltemp);
            if (bottomitems.IndexOf(choise) >= homepItems-1 || bottomitems.IndexOf(choise) <= 0)
            {
				if (bottomitems.IndexOf(choise) ==homepItems-1)
				{
					if (calltemp < 0) {
						cureentselect = bottomitems [bottomitems.IndexOf (choise) + calltemp];

					}
					else 
					{
						cureentselect = choise;
					}
				}
				else if (bottomitems.IndexOf(choise)==0) 
				{
					if (calltemp > 0) {
						cureentselect = bottomitems [bottomitems.IndexOf (choise) + calltemp];
					} 
					else 
					{
						cureentselect = choise;
					}
				}
             //   
            }
            else
            {
//                Debug.Log("elselselfjeslk");
                cureentselect = bottomitems[bottomitems.IndexOf(choise) + calltemp];
            }
        }
        if (choise != null )
            {
                int temp = -1;
               
                for (int i = 0; i < bottomitems.Count; i++)
                {
                    //调整宽度。
                    RectTransform rect = bottomitems[i].GetComponent<RectTransform>();

                    if (bottomitems[i].Equals(cureentselect))
                    {
					rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, rect.transform.localPosition.x,shortbottem + offsett);
                        temp = i;
					bottomitems[i].transform.FindChild("TextUp").transform.DOLocalMove(new Vector3(childoldpos.x+offsett/2,childoldpos.y+120,childoldpos.z),0.25f);
					bottomitems [i].transform.FindChild("TextUp").transform.DOScale (new Vector3(1.2f,1.2f,1.2f),0.25f);
					bottomitems [i].transform.FindChild ("TextDown").transform.DOScale (Vector3.one,0.25f);
					bottomitems [i].transform.FindChild ("Left").transform.DOScale (Vector3.one, 0.25f);
				//	bottomitems [i].transform.FindChild ("Left").transform.DOShakePosition(1f,new Vector3(10,0,0));
                    }
                    else {

                    rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, rect.transform.localPosition.x, shortbottem);
					bottomitems[i].transform.FindChild("TextUp").transform.DOLocalMove(childoldpos,0.25f);
					bottomitems [i].transform.FindChild("TextUp").transform.DOScale (new Vector3(0.8f,0.8f,0.8f),0.25f);
					bottomitems [i].transform.FindChild ("TextDown").transform.DOScale (Vector3.zero,0.25f);
					bottomitems [i].transform.FindChild ("Left").transform.DOScale (Vector3.zero, 0.25f);
                    }
                }
                for (int i =0; i < bottomitems.Count; i++)
                {
                    RectTransform rect = bottomitems[i].GetComponent<RectTransform>();
                    //先重置坐标
                    rect.transform.localPosition = new Vector3((i * shortbottem), 0, 0);
                    //再调整位置
                    if (i > temp)
                    {
                        rect.transform.localPosition = new Vector3(bottomitems[i].transform.localPosition.x + 51, 0f, 0f);
                    }
                    else {
                        //Debug.Log(shortbottem+">>>>>>>>"+temp);
                        rect.transform.localPosition = new Vector3((i * shortbottem), 0, 0);
                    }

                }
            //调整showpanel的Items的坐标
                for (int i = 0; i <topitems.Count ; i++)
                {
                    if (topitems[i].Equals(TopBottom[cureentselect]))
                    {
                        //TopBottom[cureentselect].transform.localPosition = Vector3.zero;
                        for (int j = 0; j < topitems.Count; j++)
                        {
                            topitems[j].transform.DOLocalMove(new Vector3((j - i) * topitemslength, 0f, 0f), 0.25f, false);
                         //   topitems[j].transform.localPosition = new Vector3((j-i)*topitemslength,0f,0f);
                        }
                        break;
                    }
                }
            }
        choise = cureentselect;
    }

    void SlidTochange(Vector3 ve) 
    {
        for (int j = 0; j < topitems.Count; j++)
        {
            topitems[j].transform.DOLocalMove(new Vector3(topitems[j].transform.localPosition.x+ve.x,0f,0f), 0.25f, false);
        }
    }


}
