Console.WriteLine(" ======= Localizar e Substituir Etiqueta ======== ");
Console.WriteLine();

Console.WriteLine(" ==> Insira o que será buscado:");
var findValue = Console.ReadLine();

Console.WriteLine();
Console.WriteLine(" ==> Insira o novo valor:");
var replaceValue = Console.ReadLine();
    
Console.WriteLine();
var allFiles = Directory.GetFiles(".", "*.fmt", SearchOption.TopDirectoryOnly);

var numberOfFiles = allFiles.Length;
var numberofModifiedFiles = 0;
var logLines = new List<string>();

if (numberOfFiles == 0)
{
    Console.WriteLine($" ==> Não foram encontrados arquivos FMT neste diretório!");
    return;
}

for (var i = 0; i < numberOfFiles; i++)
{
    var file = allFiles[i];
    var fileLines = File.ReadLines(file);
    
    var newFileLines = new List<string>();
    var modifiedFile = false;
    string logLine;
    
    foreach (var line in fileLines)
    {
        if (!line.StartsWith(findValue ?? string.Empty))
        {
            newFileLines.Add(line);
            continue;
        }

        newFileLines.Add(line.Replace(findValue!, replaceValue));
        modifiedFile = true;
    }
    
    if (modifiedFile)
    {
        File.WriteAllLines(file, newFileLines);
        numberofModifiedFiles++;
        
        logLine = $" ==> Arquivo Alterado: ${file}";
        Console.WriteLine(logLine);
        logLines.Add(logLine);
        
        continue;
    }
    
    logLine = $" ==> Arquivo Nao Alterado: ${file}";
    Console.WriteLine(logLine);
    logLines.Add(logLine);
}

logLines.Sort();
File.WriteAllLines("./EtiquetaDensidade.log.txt", logLines);

Console.WriteLine($" ==> Arquivos Encontrados: {numberOfFiles}");
Console.WriteLine($" ==> Arquivos Alterados: {numberofModifiedFiles}");

Console.WriteLine();
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();