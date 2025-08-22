using Ejercicio5;

namespace ReadingFileTests
{
    [TestClass]
    public class CSharpRefactoringTests
    {
        private const string TestFolder = "TestRefactorFolder";

        [TestInitialize]
        public void Setup()
        {
            // Creo un directorio temporal para las pruebas
            if (Directory.Exists(TestFolder))
            {
                Directory.Delete(TestFolder, true);
            }
            Directory.CreateDirectory(TestFolder);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Elimina el directorio temporal después de cada prueba
            if (Directory.Exists(TestFolder))
            {
                Directory.Delete(TestFolder, true);
            }
        }

        [TestMethod]
        public void Refactor_ShouldAddAsyncSuffix()
        {
            // Arrange
            var filePath = Path.Combine(TestFolder, "TestService.cs");
            File.WriteAllText(filePath, "public async Task GetOrder() { }");

            // Act
            ReadingFile.RefactorCSharpFiles(TestFolder);

            // Assert
            var newContent = File.ReadAllText(filePath);
            StringAssert.Contains(newContent, "public async Task GetOrderAsync()");
        }

        [TestMethod]
        public void Refactor_ShouldRenameVmDtoSuffixes()
        {
            // Arrange
            var filePath = Path.Combine(TestFolder, "TestVmDto.cs");
            File.WriteAllText(filePath, "public class UserVm { } public class ProductDto { }");

            // Act
            ReadingFile.RefactorCSharpFiles(TestFolder);

            // Assert
            var newContent = File.ReadAllText(filePath);
            StringAssert.Contains(newContent, "public class UserVM { }");
            StringAssert.Contains(newContent, "public class ProductDTO { }");
        }

        [TestMethod]
        public void Refactor_ShouldAddBlankLineBetweenMethods()
        {
            // Arrange
            var filePath = Path.Combine(TestFolder, "TestMethods.cs");
            var content = @"
    public class TestClass
    {
        public void MethodA() { }
        public void MethodB() { }
    }";

            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(content);
            }

            // Act
            ReadingFile.RefactorCSharpFiles(TestFolder);

            // Assert
            var newContent = File.ReadAllText(filePath);
            var expectedContent =
                @"public class TestClass
    {
        public void MethodA() { }

public void MethodB() { }
    }";

            Assert.AreEqual(expectedContent.Trim(), newContent.Trim());
        }
    }
}

