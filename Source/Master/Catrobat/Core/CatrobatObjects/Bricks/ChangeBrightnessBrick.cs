﻿using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Catrobat.Core.CatrobatObjects.Formulas;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class ChangeBrightnessBrick : Brick
    {
        protected Formula _changeBrightness;
        public Formula ChangeBrightness
        {
            get { return _changeBrightness; }
            set
            {
                _changeBrightness = value;
                RaisePropertyChanged();
            }
        }


        public ChangeBrightnessBrick() { }

        public ChangeBrightnessBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            _changeBrightness = new Formula(xRoot.Element("changeBrightness"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("changeBrightnessByNBrick");

            var xVariable = new XElement("changeBrightness");
            xVariable.Add(_changeBrightness.CreateXML());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ChangeBrightnessBrick();
            newBrick._changeBrightness = _changeBrightness.Copy() as Formula;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ChangeBrightnessBrick;

            if (otherBrick == null)
                return false;

            return ChangeBrightness.Equals(otherBrick.ChangeBrightness);
        }
    }
}