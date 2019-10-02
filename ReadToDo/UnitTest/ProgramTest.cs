using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadToDo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace UnitTest
{
    [TestClass]
    public class ProgramTest
    {
        const string TEST_FILES_FOLDER = "TestFiles";
        const string TODO = "Todo";
        const string WITHOUTTODO = "WithoutToDo";


        [TestMethod]
        public void GetFiles_RootFolder_Test()
        {
            // Arrange
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TEST_FILES_FOLDER);

            // Act
            List<string> result = Program.GetFiles(filePath);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetFiles_SubFolder_Case1_Test()
        {
            // Arrange
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TEST_FILES_FOLDER, TODO);

            // Act
            List<string> result = Program.GetFiles(filePath);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetFiles_SubFolder_Case2_Test()
        {
            // Arrange
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TEST_FILES_FOLDER, WITHOUTTODO);

            // Act
            List<string> result = Program.GetFiles(filePath);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void GetFiles_ArgumentException_Test()
        {
            // Act
            List<string> result = Program.GetFiles(null);
        }

        [ExpectedException(typeof(DirectoryNotFoundException))]
        [TestMethod]
        public void GetFiles_IOException_Test()
        {
            // Arrange
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Invalid Folder");

            // Act
            List<string> result = Program.GetFiles(filePath);
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TEST_FILES_FOLDER);

            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath,true);
            }
        }
    }
}
