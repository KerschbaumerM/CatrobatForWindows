﻿using System.Collections.ObjectModel;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.Core
{
    public sealed class CatrobatContextDesign : CatrobatContextBase
    {
        #region Private members

        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                if (_currentProject == value) return;

                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        public ObservableCollection<ProjectDummyHeader> LocalProjects { get; protected set; }
        public ObservableCollection<OnlineProjectHeader> OnlineProjects { get; protected set; }

        #endregion


        public CatrobatContextDesign()
        {
            InitCurrentProject();
            InitLocalProjects();
            InitOnlineProjects();

            LocalSettings = new LocalSettings
            {
                CurrentLanguageString = "en",
                CurrentProjectName = "DefaultProject",
                CurrentThemeIndex = 0,
                CurrentToken = "DummyToken",
                CurrentUserEmail = "dummy@somedomain.com"
            };
        }

        private void InitCurrentProject()
        {
            var project = new Project
            {
                ProjectHeader = new ProjectHeader(false)
                {
                    ApplicationBuildName = "",
                    ApplicationBuildNumber = 0,
                    ApplicationName = "Pocket Code",
                    ApplicationVersion = "0.8.2",
                    CatrobatLanguageVersion = (float)0.8,
                    DateTimeUpload = "",
                    Description = "Dies ist eine Test Anwendung.",
                    DeviceName = "Device 1",
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    Platform = "Windows Phone",
                    PlatformVersion = "8.0",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    RemixOf = "",
                    ScreenHeight = 1280,
                    ScreenWidth = 720,
                    Tags = "",
                    Url = "http://pocketcode.org/details/871",
                    UserHandle = "Username"
                }
            };

            project.SetProgramName("Project 1 with very very very very very long name");

            project.VariableList = new VariableList
            {
                ObjectVariableList = new ObjectVariableList
                {
                    ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>()
                },
                ProgramVariableList = new ProgramVariableList
                {
                    UserVariables = new ObservableCollection<UserVariable>()
                },
            };


            var sprites = new SpriteList();
            var sprite = new Sprite();
            sprite.Name = "Object 1";

            sprite.Costumes = new CostumeList();
            var costume = new Costume("Cat");
            var image = new byte[0]; //new BitmapImage(new Uri(costume.FileName, UriKind.Relative)); // TODO: fix me
            //costume.Image = image;
            sprite.Costumes.Costumes.Add(costume);

            sprite.Sounds = new SoundList();
            var sound = new Sound("Miau_Sound");
            sprite.Sounds.Sounds.Add(sound);

            sprite.Scripts = new ScriptList();
            Script startScript = new StartScript();

            startScript.Bricks = new BrickList();
            var setCostumeBrick = new SetCostumeBrick();
            var costumeRef = new CostumeReference();
            costumeRef.Costume = costume;
            //setCostumeBrick.Costume = costumeRef;

            //TODO: Add more Bricks if you need them

            sprites.Sprites.Add(sprite);
            project.SpriteList = sprites;

            CurrentProject = project;
        }

        private void InitLocalProjects()
        {
            LocalProjects = new ObservableCollection<ProjectDummyHeader>();

            // TODO: add sample images

            var project1 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 2 with very very very very very long name"
            };

            var project2 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 3"
            };

            var project3 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 4"
            };


            LocalProjects.Add(project1);
            LocalProjects.Add(project2);
            LocalProjects.Add(project3);
        }

        private void InitOnlineProjects()
        {
            OnlineProjects = new ObservableCollection<OnlineProjectHeader>();

            var project1 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 1 with very very very very very long name",
                Description = "That is a nice description of Project 1. That is a nice description of Project 1. That is a nice description of Project 1"
            };

            var project2 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 2",
                Description = "That is a nice description of Project 2."
            };

            var project3 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 3",
                Description = "That is a nice description of Project 3."
            };

            OnlineProjects.Add(project1);
            OnlineProjects.Add(project2);
            OnlineProjects.Add(project3);
        }

    }
}