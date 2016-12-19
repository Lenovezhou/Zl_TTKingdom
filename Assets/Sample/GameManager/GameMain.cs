using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using System.Security.AccessControl;


/*  声明一个新的类（需继承TTUIPage<using TinyTeam.UI;>）调用相同的方法，
 *  在该类的构造方法为uipath赋值，填好枚举值（决定父节点），
 *  重写Awake（）方法，为子节点添加事件
 */
public class GameMain : MonoBehaviour {
	// Use this for initialization
	void Start () {
        TTUIPage.ShowPage<Homepanel>();
        //TTUIPage.ShowPage<UITopBar>();
        //TTUIPage.ShowPage<UIMainPage>();
        //TTUIPage.ShowPage<thisShow>();
      // CreatXML();
    }
    public static void CreatXML()
    {

        XmlDocument doc = new XmlDocument();
        XmlNode declare = doc.CreateXmlDeclaration("1.0", "utf-8", "");
        doc.AppendChild(declare);
        XmlElement root = doc.CreateElement("Program");
        doc.AppendChild(root);
        //for (int i = 0; i < roomid.Count; i++)
        //{
            XmlElement id = doc.CreateElement("id");
            id.SetAttribute("layoutID","verygood");
        //    id.SetAttribute("type", sceneindex[i].ToString());
            root.AppendChild(id);
        //}
            if (!Directory.Exists(Application.dataPath+"/XML"))
            {
                Directory.CreateDirectory(Application.dataPath+"/XML");
            }
            if (!File.Exists(Application.dataPath + "/XML/INFO.xml"))
                {
                    Debug.Log("不包含");
                    Stream s= File.Create(Application.dataPath + "/XML/INFO.xml");
                    s.Close();
                }
            Debug.Log("写XML" + Application.dataPath + "/XML/INFO.xml");
            doc.Save(Application.dataPath + "/XML/INFO.xml");
    }

    void CreateModelFile(string path, string name, byte[] info, int length)
    {
        //文件流信息
        //StreamWriter sw;
        Stream sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
         //   sw = t.Create();
        }
        else
        {
            return;
        }
        //以行的形式写入信息
        //sw.WriteLine(info);
//        sw.Write(info, 0, length);
        //关闭流
 //       sw.Close();
        //销毁流
//        sw.Dispose();
    }
	// Update is called once per frame
	void Update () {
        
	}

}
