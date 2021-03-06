﻿using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlStopAllSoundsBrick : XmlBrick
    {
        public XmlStopAllSoundsBrick() {}

        public XmlStopAllSoundsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlStopAllSoundsBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
