using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public delegate void BundleCall(AssetBundle bundle);

public class BundleStruct
{
    public AssetBundle s_Bundle;
    public BundleCall s_CallBack;
    public float s_time;
}


public class ResourceManager : MonoBehaviour {
    public static readonly string BundleUrl =
#if UNITY_ANDROID   //��׿
    "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE  //iPhone
    Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windowsƽ̨��webƽ̨
 "file://" + Application.dataPath + "/BundleWin64/";
#else
        string.Empty;
#endif

    public Dictionary<string, BundleStruct> m_BundleDic = new Dictionary<string, BundleStruct>(); 

    /// <summary>
    /// ������Դ
    /// </summary>
    /// <param name="bundleName">Bundle��</param>
    /// <param name="isAsync">�Ƿ��첽����</param>
    public void LoadAssetBundle(string bundleName,bool isAsync,BundleCall callBack)
    {
        if (m_BundleDic.ContainsKey(bundleName))
        {
            m_BundleDic[bundleName].s_time = Time.time;
            m_BundleDic[bundleName].s_CallBack = callBack;
            callBack(m_BundleDic[bundleName].s_Bundle);
            Debug.Log("�ֵ��д��ڣ�ֱ�Ӵ��ֵ��м���------"+bundleName);
        }
        else
        {
            BundleStruct temp_Struct = new BundleStruct();
            temp_Struct.s_time = Time.time;
            m_BundleDic.Add(bundleName, temp_Struct);
            m_BundleDic[bundleName].s_CallBack = callBack;

            if (isAsync)
            {
                StartCoroutine(AsyncLoad(bundleName));
            }
            else
            {
                AssetBundle temp_Bundle = Resources.Load(bundleName) as AssetBundle;
                m_BundleDic[bundleName].s_Bundle = temp_Bundle;
                callBack(m_BundleDic[bundleName].s_Bundle);
            }
        }
    }
    /// <summary>
    /// �첽����Bundl��������ɷŵ��ֵ�
    /// </summary>
    /// <param name="bundleName">Bundle����</param>
    /// <returns>WWW</returns>
    IEnumerator AsyncLoad(string bundleName)
    {
        WWW www = new WWW(BundleUrl + bundleName + ".assetbundle");
        yield return www;
        Debug.Log("�ֵ��в����ڣ�WWW����------" + bundleName);
        if (www.isDone && www.error == null)
        {
            m_BundleDic[bundleName].s_Bundle = www.assetBundle;
            m_BundleDic[bundleName].s_CallBack(m_BundleDic[bundleName].s_Bundle);
        }
        else
        {
            Debug.Log(string.Format("����ʧ��{0} , Erro: {1}",bundleName,www.error));
        }
    }
    /// <summary>
    /// ����ʱ�����ж��
    /// </summary>
    public void OnTimeDestroyBundle( )
    {
        List<string> temp_Struct=new List<string> ();
        foreach (BundleStruct item in m_BundleDic.Values)
        {
            if (Time.time - item.s_time > 10)
            {
                item.s_Bundle.Unload(false);
                /*  ѭ�������ֵ������еļ�ֵ��
                 *  ��������û�����ļ�/ֵ�ԡ�
                 */
                foreach (KeyValuePair<string, BundleStruct> kvp in m_BundleDic)
                {
                    if (kvp.Value.Equals(item))
                    {
                        temp_Struct.Add(kvp.Key);
                    }
                }
            }
        }
        if (temp_Struct.Count!=0)
        {
            for (int i = 0; i < temp_Struct.Count; i++)
            {
                Debug.Log("Remov: " + temp_Struct[i]+"   On Time:"+Time.time);
                m_BundleDic.Remove(temp_Struct[i]);
            }
            temp_Struct.Clear();
        }
    }
    /// <summary>
    /// ָ�����ж��Bundle
    /// </summary>
    /// <param name="bundleName">��Ҫɾ��Budle</param>
    public void DestroyThisBundle(string bundleName)
    {
        if (m_BundleDic.ContainsKey(bundleName))
        {
            m_BundleDic[bundleName].s_Bundle.Unload(false);
            m_BundleDic.Remove(bundleName);
        }
    }
	// Update is called once per frame
	void Update () 
    {
        OnTimeDestroyBundle();
	}
}
