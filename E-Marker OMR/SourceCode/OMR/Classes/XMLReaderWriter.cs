using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using OMR;

namespace OMR.XML
{
    /// <summary>
    /// Reads/writes xml files 
    /// </summary>
    public static class XMLReaderWriter
    {
        /// <summary>
        /// Reads a string value at a depth of 2. i.e Document >> Element1 >> Value
        /// </summary>
        /// <param name="file">XML File</param>
        /// <param name="firstElement">Element 1</param>
        /// <param name="e1attrib1Name">Element 1's first attribute's Name, can't be empty</param>
        /// <param name="e1attrib1Value">Element 1's first attribute's Value, can't be empty</param>
        /// <param name="e1attrib2Name">Element 1's second attribute's Name, can't be empty</param>
        /// <param name="e1attrib2Value">Element 1's second attribute's Value, can't be empty</param>
        /// <param name="valueName">Value's Name to be Aquired</param>
        /// <returns></returns>
        public static string ReadValueD1(
               string file, string firstElement,
               string e1attrib1Name, string e1attrib1Value, string e1attrib2Name, string e1attrib2Value, string valueName)
        {
            XmlReader xr = XmlReader.Create("Sheets.xml");
            while (xr.Read() && !xr.EOF)
            {
                if (xr.NodeType == XmlNodeType.Element && xr.Name == firstElement &&
                    xr.GetAttribute(e1attrib1Name) == e1attrib1Value &&
                    xr.GetAttribute(e1attrib2Name) == e1attrib2Value)
                {
                    xr.Read();
                    while (xr.Name != valueName)
                        xr.Read();
                    while (xr.NodeType != XmlNodeType.Text)
                    {
                        xr.Read();
                    }
                    string val = xr.Value;
                    xr.Close();
                    return val;
                }
            }
            throw new Exception("Value not Found.");
        }
        /// <summary>
        /// Reads a string value at a depth of 2. i.e Document >> Element1 >> Element 2 >> Value
        /// </summary>
        /// <param name="file">XML File</param>
        /// <param name="firstElement">Element 1</param>
        /// <param name="e1attrib1Name">Element 1's first attribute's Name, can't be empty</param>
        /// <param name="e1attrib1Value">Element 1's first attribute's Value, can't be empty</param>
        /// <param name="e1attrib2Name">Element 1's second attribute's Name, can't be empty</param>
        /// <param name="e1attrib2Value">Element 1's second attribute's Value, can't be empty</param>
        /// <param name="secondElement">Element 2</param>
        /// <param name="valueName">Value's Name to be Aquired</param>
        /// <returns></returns>
        public static string ReadValueD2(
            string file, string firstElement,
            string e1attrib1Name, string e1attrib1Value, string e1attrib2Name, string e1attrib2Value,
            string secondElement, string valueName)
        {
            XmlReader xr = XmlReader.Create("Sheets.xml");
            while (xr.Read() && !xr.EOF)
            {
                if (xr.NodeType == XmlNodeType.Element && xr.Name == firstElement &&
                    xr.GetAttribute(e1attrib1Name) == e1attrib1Value &&
                    xr.GetAttribute(e1attrib2Name) == e1attrib2Value)
                {
                    xr.Read();
                    while (xr.Name != secondElement && xr.Name != "EndOfProperties")
                    {
                        xr.Read();
                    }
                    if (xr.Name == secondElement)
                    {
                        while (xr.Name != valueName)
                            xr.Read();
                        while (xr.NodeType != XmlNodeType.Text)
                            xr.Read();
                        string val = xr.Value;
                        xr.Close();
                        return val;
                    }
                }
            }
            throw new Exception("Value not Found.");
        }
    }

    /// <summary>
    /// Writes specifications to a given xml file 
    /// </summary>
    public static class OMRSheetSpecsWriter
    {
        public static void WriteSheetsToFile(string file)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(file, settings);
            writer.WriteStartDocument();
            #region A550
            writer.WriteStartElement("OMRSheets");
            writer.WriteStartElement("OMRSheet");
            writer.WriteAttributeString("SheetSize", "A5");
            writer.WriteAttributeString("OMarks", "50");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.MinBlobRatio.ToString(), "0.0001");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.MaxBlobRatio.ToString(), "0.005");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.StartingContrast.ToString(), "0");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.BackGroundFill.ToString(), "0");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.MinBlobToDetect.ToString(), "3");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.ImageMatch.ToString(), "54");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.threshValue.ToString(), "44");


            writer.WriteElementString(OMREnums.OMRProperty.NumOfBlocks.ToString(), "4");
            writer.WriteElementString(OMREnums.OMRProperty.TensBlock.ToString(), "True");

            writer.WriteStartElement(OMREnums.OMRProperty.RegNumBlock.ToString());
            writer.WriteElementString("X", "234");
            writer.WriteElementString("Y", "64");
            writer.WriteElementString("Width", "404");
            writer.WriteElementString("Height", "133");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.RegNumBlock.ToString());
            writer.WriteElementString("X", "234");
            writer.WriteElementString("Y", "64");
            writer.WriteElementString("Width", "404");
            writer.WriteElementString("Height", "133");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.SheetSize.ToString());
            writer.WriteElementString("X", "0");
            writer.WriteElementString("Y", "0");
            writer.WriteElementString("Width", "1500");
            writer.WriteElementString("Height", "978");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.CandidateSignature.ToString());
            writer.WriteElementString("X", "1049");
            writer.WriteElementString("Y", "272");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "150");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.SuperVisorSignature.ToString());
            writer.WriteElementString("X", "1049");
            writer.WriteElementString("Y", "74");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "150");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.Name.ToString());
            writer.WriteElementString("X", "200");
            writer.WriteElementString("Y", "208");
            writer.WriteElementString("Width", "800");
            writer.WriteElementString("Height", "110");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.Dated.ToString());
            writer.WriteElementString("X", "200");
            writer.WriteElementString("Y", "302");
            writer.WriteElementString("Width", "800");
            writer.WriteElementString("Height", "110");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock1.ToString());
            writer.WriteElementString("X", "127");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock2.ToString());
            writer.WriteElementString("X", "488");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock3.ToString());
            writer.WriteElementString("X", "848");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock4.ToString());
            writer.WriteElementString("X", "1203");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement("EndOfProperties");
            writer.WriteEndElement();

            writer.WriteEndElement();
            #endregion
            #region A555
            writer.WriteStartElement("OMRSheet");
            writer.WriteAttributeString("SheetSize", "A5");
            writer.WriteAttributeString("OMarks", "55");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.MinBlobRatio.ToString(), "0.0001");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.MaxBlobRatio.ToString(), "0.005");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.StartingContrast.ToString(), "0");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.BackGroundFill.ToString(), "0");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.MinBlobToDetect.ToString(), "3");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.ImageMatch.ToString(), "54");
            writer.WriteElementString(OMREnums.OMRImageProcessVariables.threshValue.ToString(), "44");


            writer.WriteElementString(OMREnums.OMRProperty.NumOfBlocks.ToString(), "4");
            writer.WriteElementString(OMREnums.OMRProperty.TensBlock.ToString(), "True");

            writer.WriteStartElement(OMREnums.OMRProperty.sheetNumBlock.ToString());
            writer.WriteElementString("X", "728");
            writer.WriteElementString("Y", "107");
            writer.WriteElementString("Width", "204");
            writer.WriteElementString("Height", "43");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.RegNumBlock.ToString());
            writer.WriteElementString("X", "234");
            writer.WriteElementString("Y", "64");
            writer.WriteElementString("Width", "404");
            writer.WriteElementString("Height", "133");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.SheetSize.ToString());
            writer.WriteElementString("X", "0");
            writer.WriteElementString("Y", "0");
            writer.WriteElementString("Width", "1500");
            writer.WriteElementString("Height", "978");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.CandidateSignature.ToString());
            writer.WriteElementString("X", "1049");
            writer.WriteElementString("Y", "272");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "150");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.SuperVisorSignature.ToString());
            writer.WriteElementString("X", "1049");
            writer.WriteElementString("Y", "74");
            writer.WriteElementString("Width", "365");
            writer.WriteElementString("Height", "150");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.Name.ToString());
            writer.WriteElementString("X", "200");
            writer.WriteElementString("Y", "208");
            writer.WriteElementString("Width", "800");
            writer.WriteElementString("Height", "110");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.Dated.ToString());
            writer.WriteElementString("X", "200");
            writer.WriteElementString("Y", "302");
            writer.WriteElementString("Width", "800");
            writer.WriteElementString("Height", "110");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock1.ToString());
            writer.WriteElementString("X", "127");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock2.ToString());
            writer.WriteElementString("X", "488");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock3.ToString());
            writer.WriteElementString("X", "848");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement(OMREnums.OMRProperty.tensBlock4.ToString());
            writer.WriteElementString("X", "1203");
            writer.WriteElementString("Y", "502");
            writer.WriteElementString("Width", "200");
            writer.WriteElementString("Height", "462");
            writer.WriteEndElement();

            writer.WriteStartElement("EndOfProperties");
            writer.WriteEndElement();

            writer.WriteEndElement();
            #endregion
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }

    /// <summary>
    /// Reads Sheets specifications form a given xml file 
    /// </summary>
    public static class OMRSheetReader
    {
        public static Rectangle GetSheetPropertyLocation(string file, OMREnums.OMRSheet sheet, OMREnums.OMRProperty property)
        {
            Rectangle rect = new Rectangle(
            Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
                property.ToString(), "X")),
                Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
                property.ToString(), "Y")),
                Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
                property.ToString(), "Width")),
                Convert.ToInt32(XML.XMLReaderWriter.ReadValueD2(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2),
                property.ToString(), "Height")));
            return rect;
        }
        public static bool GetSheetPropertyBool(string file, OMREnums.OMRSheet sheet, OMREnums.OMRProperty var)
        {
            return Convert.ToBoolean(XML.XMLReaderWriter.ReadValueD1(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2), var.ToString()));
        }
        public static int GetSheetPropertyInt(string file, OMREnums.OMRSheet sheet, OMREnums.OMRProperty var)
        {
            return Convert.ToInt32(XML.XMLReaderWriter.ReadValueD1(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2), var.ToString()));
        }
        public static double getProcessVariableD(string file, OMREnums.OMRSheet sheet, OMREnums.OMRImageProcessVariables var)
        {
            return Convert.ToDouble(XML.XMLReaderWriter.ReadValueD1(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2), var.ToString()));
        }
        public static int getProcessVariableI(string file, OMREnums.OMRSheet sheet, OMREnums.OMRImageProcessVariables var)
        {
            return Convert.ToInt32(XML.XMLReaderWriter.ReadValueD1(
                file, "OMRSheet", "SheetSize", sheet.ToString().Substring(0, 2), "OMarks", sheet.ToString().Substring(2, 2), var.ToString()));
        }

    }

    public struct OMREnums
    {

        public enum OMRSheet
        {
            A550 = 1,
            A555 = 2
        }
        public enum OMRProperty : int
        {
            NumOfBlocks = 11,
            TensBlock = 12,
            SheetSize = 13,
            CandidateSignature = 14,
            SuperVisorSignature = 15,
            RegNumBlock = 16,
            Name = 17,
            Dated = 18,
            sheetNumBlock = 19,
            tensBlock1 = 1,
            tensBlock2 = 2,
            tensBlock3 = 3,
            tensBlock4 = 4,
            tensBlock5 = 5,
            twentiesBlock1 = 6,
            twentiesBlock2 = 7,
            twentiesBlock3 = 8,
            twentiesBlock4 = 9,
            twentiesBlock5 = 10,
        }
        public enum OMRImageProcessVariables
        {
            MinBlobRatio,
            MaxBlobRatio,
            StartingContrast,
            ImageMatch,
            BackGroundFill,
            MinBlobToDetect,
            threshValue
        }
    }
}
