using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class thisShow :TTUIPage {

    public thisShow()
        : base(UIType.PopUp, UIMode.HideOther, UICollider.None)
    {
        uiPath = "UIPrefab/thisShow";
    }

    public Texture mainimage;
    public string mainname;

    public override void Awake(GameObject go)
    {
        this.transform.Find("SolutionB").GetComponent<Button>().onClick.AddListener(OnClickGoBattle);
        this.transform.Find("ExitB").GetComponent<Button>().onClick.AddListener(Hide);
    }

    

    private void OnClickGoBattle()
    {
        ShowPage<NextShow>();
        //Debug.Log("NextShow!");
    }

    public override void Refresh()
    {
        if (data != null)
        {
            solut ts = data as solut;
            Debug.Log(ts.tec + ts.name);
            this.transform.Find("DownPanel/Text/Text (4)").GetComponent<Text>().text = ts.name;
            this.transform.Find("UpPanel/Image").GetComponent<RawImage>().texture = ts.tec;
        }
    }

}
