using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Asteroids3D
{
    class ObjReader
    {
        public void readObj(string filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string objData = streamReader.ReadToEnd();
            streamReader.Close();

            Regex.Split(objData, "vertex positions");

            //Regex.Regex.Matches
        }
    }
}
