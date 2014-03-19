﻿using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects.Sounds
{
    public class SoundReference : DataObject
    {
        internal string _reference;

        private Sound _sound;
        public Sound Sound
        {
            get { return _sound; }
            set
            {
                if (_sound == value)
                {
                    return;
                }

                _sound = value;
                RaisePropertyChanged();
            }
        }

        public SoundReference()
        {
        }

        public SoundReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Sound == null)
                Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newSoundInfoRef = new SoundReference();
            newSoundInfoRef.Sound = _sound;

            return newSoundInfoRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as SoundReference;

            if (otherReference == null)
                return false;

            if (Sound.Name != otherReference.Sound.Name)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}