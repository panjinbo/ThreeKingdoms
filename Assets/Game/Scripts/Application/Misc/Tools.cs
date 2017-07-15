using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

public class Tools
{
    //读取关卡列表
    // public static List<FileInfo> GetLevelFiles()
    // {
    //     string[] files = Directory.GetFiles(Consts.LevelDir, "*.xml");
    //
    //     List<FileInfo> list = new List<FileInfo>();
    //     for (int i = 0; i < files.Length; i++)
    //     {
    //         FileInfo file = new FileInfo(files[i]);
    //         list.Add(file);
    //     }
    //     return list;
    // }

    //填充Level类数据
    public static void FillLevel(int count, ref Level level)
    {
       TextAsset textAsset = (TextAsset) Resources.Load(Consts.LevelDir+"level"+count);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
        level.CardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;
        level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
        level.Road = doc.SelectSingleNode("/Level/Road").InnerText;
        level.InitScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);
        level.RowNum = int.Parse(doc.SelectSingleNode("/Level/RowNum").InnerText);
        level.ColNum = int.Parse(doc.SelectSingleNode("/Level/ColNum").InnerText);
        XmlNodeList nodes;

        nodes = doc.SelectNodes("/Level/Holder/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];
            Point p = new Point(
                int.Parse(node.Attributes["X"].Value),
                int.Parse(node.Attributes["Y"].Value));

            level.Holder.Add(p);
        }

        nodes = doc.SelectNodes("/Level/Path/Point");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];

            Point p = new Point(
                int.Parse(node.Attributes["X"].Value),
                int.Parse(node.Attributes["Y"].Value));

            level.Path.Add(p);
        }

        nodes = doc.SelectNodes("/Level/Rounds/Round");
        for (int i = 0; i < nodes.Count; i++)
        {
            XmlNode node = nodes[i];

            Round r = new Round(
                    int.Parse(node.Attributes["Enemy"].Value),
                    int.Parse(node.Attributes["Count"].Value)
                );

            level.Rounds.Add(r);
        }

    }

    //保存关卡
    public static void SaveLevel(string fileName, Level level)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<Level>");

        sb.AppendLine(string.Format("<Name>{0}</Name>", level.Name));
        sb.AppendLine(string.Format("<Background>{0}</Background>", level.Background));
        sb.AppendLine(string.Format("<Road>{0}</Road>", level.Road));
        sb.AppendLine(string.Format("<InitScore>{0}</InitScore>", level.InitScore));

        sb.AppendLine("<Holder>");
        for (int i = 0; i < level.Holder.Count; i++)
        {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Holder[i].X, level.Holder[i].Y));
        }
        sb.AppendLine("</Holder>");

        sb.AppendLine("<Path>");
        for (int i = 0; i < level.Path.Count; i++)
        {
            sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Path[i].X, level.Path[i].Y));
        }
        sb.AppendLine("</Path>");

        sb.AppendLine("<Rounds>");
        for (int i = 0; i < level.Rounds.Count; i++)
        {
            sb.AppendLine(string.Format("<Round Enemy=\"{0}\" Count=\"{1}\"/>", level.Rounds[i].Enemy, level.Rounds[i].Count));
        }
        sb.AppendLine("</Rounds>");

        sb.AppendLine("</Level>");

        string content = sb.ToString();

        StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
        sw.Write(content);
        sw.Flush();
        sw.Dispose();
    }

    //加载图片
    public static IEnumerator LoadImage(string url, SpriteRenderer render)
    {
         WWW www = new WWW(url);

         while (!www.isDone)
             yield return www;

        Texture2D texture = Resources.Load<Texture2D>(url);
        Sprite sp = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        render.sprite = sp;
    }

    public static IEnumerator LoadImage(string url, Image image)
    {
        WWW www = new WWW(url);

        while (!www.isDone)
            yield return www;

        Texture2D texture = Resources.Load<Texture2D>(url);
        Sprite sp = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        image.sprite = sp;
    }


}
