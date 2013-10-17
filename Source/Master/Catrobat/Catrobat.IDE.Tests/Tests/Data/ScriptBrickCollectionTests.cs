﻿using System;
using System.Collections;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.Data
{
    [TestClass]
    public class ScriptBrickCollectionTests
    {
        [TestInitialize]
        public void Initialize()
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void ScriptBrickEnumeratorTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();


            {
                Sprite sprite = project.SpriteList.Sprites[0];
                ScriptBrickCollection collection = new ScriptBrickCollection();
                collection.Update(sprite);
                IEnumerator enumerator = collection.GetEnumerator();

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is StartScript);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            }

            {
                Sprite sprite = project.SpriteList.Sprites[1];
                ScriptBrickCollection collection = new ScriptBrickCollection();
                collection.Update(sprite);
                IEnumerator enumerator = collection.GetEnumerator();

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is StartScript);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is WhenScript);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is WaitBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is WaitBrick);
                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is SetCostumeBrick);

                enumerator.MoveNext();
                Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

                Assert.IsFalse(enumerator.MoveNext());
            }
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void ScriptBrickCollectionRemoveTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            Sprite sprite = project.SpriteList.Sprites[1];
            ScriptBrickCollection collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WhenScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);


            collection.RemoveAt(4);
            collection.Remove(enumerator.Current);

            enumerator.Reset();
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WhenScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void ScriptBrickCollectionInsertTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            Sprite sprite = project.SpriteList.Sprites[1];
            ScriptBrickCollection collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            Brick newBrick1 = new ChangeGhostEffectBrick();
            collection.Insert(4, newBrick1);
            Brick newBrick2 = new ChangeXByBrick();
            collection.Insert(4, newBrick2);

            //Script newScript1 = new BroadcastScript(); // TODO: test adding scripts
            //collection.Insert(5, newScript1);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WhenScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeXByBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeGhostEffectBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void ScriptBrickCollectionAddTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            Sprite sprite = project.SpriteList.Sprites[1];
            ScriptBrickCollection collection = new ScriptBrickCollection();
            collection.Update(sprite);

            Script newScript1 = new BroadcastScript();
            collection.Add(newScript1);

            Brick newBrick1 = new ChangeGhostEffectBrick();
            collection.Add(newBrick1);
            Brick newBrick2 = new ChangeXByBrick();
            collection.Add(newBrick2);

            Script newScript2 = new WhenScript();
            collection.Add(newScript2);

            IEnumerator enumerator = collection.GetEnumerator();

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WhenScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is BroadcastScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeGhostEffectBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeXByBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WhenScript);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void ScriptBrickCollectionContainsTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            Sprite sprite = project.SpriteList.Sprites[1];
            ScriptBrickCollection collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
                if (!(enumerator.Current is EmptyDummyBrick))
                    Assert.IsTrue(collection.Contains(enumerator.Current));

            Brick notContainedBrick = new BroadcastBrick();
            Assert.IsFalse(collection.Contains(notContainedBrick));

            Script notContainedScript = new BroadcastScript();
            Assert.IsFalse(collection.Contains(notContainedScript));
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void ScriptBrickCollectionIndexIfTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            Sprite sprite = project.SpriteList.Sprites[1];
            ScriptBrickCollection collection = new ScriptBrickCollection();
            collection.Update(sprite);
            IEnumerator enumerator = collection.GetEnumerator();

            int referenceIndex = 0;
            while (enumerator.MoveNext())
            {
                if (!(enumerator.Current is EmptyDummyBrick))
                    Assert.AreEqual(referenceIndex, collection.IndexOf(enumerator.Current));

                referenceIndex++;
            }

            Brick notContainedBrick = new BroadcastBrick();
            Assert.AreEqual(-1, collection.IndexOf(notContainedBrick));

            Script notContainedScript = new BroadcastScript();
            Assert.AreEqual(-1, collection.IndexOf(notContainedScript));
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void ScriptBrickCollectionAddBrickTest()
        {
            ITestProjectGenerator projectGenerator = new ProjectGeneratorForScriptBrickCollectionTests();
            var project = projectGenerator.GenerateProject();

            Sprite sprite = project.SpriteList.Sprites[1];
            ScriptBrickCollection collection = new ScriptBrickCollection();
            collection.Update(sprite);


            Brick insertedBrick1 = new ChangeBrightnessBrick();
            Brick insertedBrick2 = new MoveNStepsBrick();
            collection.AddScriptBrick(insertedBrick1, 4, 8);
            collection.AddScriptBrick(insertedBrick2, 1, 6);


            IEnumerator enumerator = collection.GetEnumerator();

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is StartScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            //enumerator.MoveNext();
            //Assert.IsTrue(enumerator.Current is MoveNStepsBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WhenScript);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is WaitBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is SetCostumeBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is ChangeBrightnessBrick);

            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is MoveNStepsBrick);
            enumerator.MoveNext();
            Assert.IsTrue(enumerator.Current is EmptyDummyBrick);

            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}