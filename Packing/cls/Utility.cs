using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Packing
{
    public class Utility
    {
        public static Dictionary<int, string> DIC_WARNING = new Dictionary<int, string>(){
        {0,"急停报警"},
        {1,"一号无料或卡死"},
        {2,"二号无料或卡死"},
        {3,"三号无料或卡死"},
        {4,"四号无料或卡死"},
        {5,"五号无料或卡死"},
        {6,"无纸报警"},
        {7,"空袋报警"},
        {8,"光标不到位"},
        {9,"上位连接失败"},
        {10,"备用"}
        };

        public static T RaadFile<T>(string fileName)
        {
            XmlSerializer fommatter = new XmlSerializer(typeof(T));
            using (XmlReader xr = XmlReader.Create(fileName))
            {
                return (T)fommatter.Deserialize(xr);//反序列化对象 

            }
        }

        public static void WriteFile<T>(string fileName, T data)
        {
            XmlSerializer fommatter = new XmlSerializer(typeof(T));
            using (Stream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fommatter.Serialize(fs, data);//序列化对象
            }
        }
    }
}
