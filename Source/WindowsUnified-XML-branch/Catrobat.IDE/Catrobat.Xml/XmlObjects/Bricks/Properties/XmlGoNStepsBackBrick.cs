﻿using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlGoNStepsBackBrick : XmlBrick
    {
        public XmlFormula Steps { get; set; }

        public XmlGoNStepsBackBrick()
        {
        }

        public XmlGoNStepsBackBrick(XElement xElement) : base(xElement)
        {
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            Steps = new XmlFormula(xRoot.Element(XmlConstants.Steps));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlGoNStepsBackBrickType);
            
            var xElement = Steps.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Steps);
            xRoot.Add(xElement);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Steps != null)
                Steps.LoadReference();
        }
    }
}