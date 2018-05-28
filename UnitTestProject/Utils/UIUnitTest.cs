using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paint.Utils;

namespace UnitTestProject.Utils
{
    [TestClass]
    public class UIUnitTest
    {
        [TestMethod()]
        public void CreateOpenFileDialogTest()
        {
            Assert.IsNotNull(UI.CreateOpenFile());
        }

        [TestMethod()]
        public void CreateInformationWindowTest()
        {
            Assert.IsNotNull(UI.CreateInformationWindow());

        }

        [TestMethod()]
        public void CreateSaveFileDialogTest()
        {
            Assert.IsNotNull(UI.CreateSaveFile());
        }
    }
}
