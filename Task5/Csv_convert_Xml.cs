using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace Task5
{
    public class Csv_convert_Xml
    {
        public Students st = new Students();
        public List<Students> students = new List<Students>();
        public string fileName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\students.xml";

        public struct Students
        {
            public string name;
            public string univercity;
            public string faculty;
            public string department;
            public int age;
            public int course;
            public string city;
        }

        public void readCsv(string path)
        {
            StreamReader sr = new StreamReader(path);

            while (!sr.EndOfStream)
            {
                string[] s = sr.ReadLine().Split(';');

                students.Add(st);
                st.name = s[1] + " " + s[0];
                st.univercity = s[2];
                st.faculty = s[3];
                st.department = s[4];
                st.age = Int32.Parse(s[5]);
                st.course = Int32.Parse(s[6]);
                st.city = s[8];
            }

            sr.Close();
        }

        public void convertCsv()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Students>));
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            xmlFormat.Serialize(fStream, students);
            fStream.Close();
        }
    }
}
