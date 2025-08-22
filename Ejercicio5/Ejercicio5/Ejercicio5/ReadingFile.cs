using System.Text.RegularExpressions;
namespace Ejercicio5
{
    public static class ReadingFile
    {
        public static void RefactorCSharpFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException($"No existe el directorio: {folderPath}");
            }

            var csFiles = Directory.EnumerateFiles(folderPath, "*.cs", SearchOption.AllDirectories);

            foreach (var file in csFiles)
            {
                var content = File.ReadAllText(file);
                var originalContent = content;

                // a) Se renombran los metodos asincronicos que no contengan "Async" como sufijo
                content = RenameAsyncMethods(content);

                // b) Se renombran palabras que terminan en Vm, Vms, Dto, Dtos
                content = RenameSuffixes(content);

                // c) Se agregan línea en blanco entre métodos
                content = AddBlankLineBetweenMethods(content);

                if (content != originalContent)
                {
                    File.WriteAllText(file, content);
                    Console.WriteLine($"Refactorizado: {file}");
                }
            }
        }

        private static string RenameAsyncMethods(string content)
        {
            // Se implementa una expresion regular para encontrar metodos que retornen Task o Task<T>
            // que no terminan con async, se captura el nombre del metodo y se le agrega "Async"
            
            var regex = new Regex(@"\b(public|private|protected|internal)?\s+async\s+(Task|Task<.+?>)\s+([a-zA-Z_]\w*)\s*\(");
            return regex.Replace(content, match =>
            {
                var methodName = match.Groups[3].Value;
                if (!methodName.EndsWith("Async"))
                {
                    return match.Value.Replace(methodName, $"{methodName}Async");
                }
                return match.Value;
            });
        }

        private static string RenameSuffixes(string content)
        {
            // Se realiza la buqueda de cualquier palabra que termine en 
            // Vm, Vms, Dto o Dtos y se reemplaza por VM, VMs, DTO, DTOs
            content = Regex.Replace(content, @"\b(\w+)Vm\b", "$1VM");
            content = Regex.Replace(content, @"\b(\w+)Vms\b", "$1VMs");
            content = Regex.Replace(content, @"\b(\w+)Dto\b", "$1DTO");
            content = Regex.Replace(content, @"\b(\w+)Dtos\b", "$1DTOs");
            return content;
        }

        private static string AddBlankLineBetweenMethods(string content)
        {
            // Mediante una expresion regular se busca la declaracion de un metodo, seguida por otra
            // sin existir espacio en blanco de separacion entre metodos.
            // Se agrega un salto de linea.
            
            var regex = new Regex(@"(\s*})\s*\r?\n\s*(\[|public|private|protected|internal)");
            return regex.Replace(content, match =>
            {
                return match.Groups[1].Value + Environment.NewLine + Environment.NewLine + match.Groups[2].Value;
            });
        }
    }
}
