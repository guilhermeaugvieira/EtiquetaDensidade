Console.WriteLine(" ======= Localizar e Substituir Etiqueta ======== ");
Console.WriteLine();

Console.WriteLine("👉🏻 Insira o que será buscado:");
var findValue = Console.ReadLine();

Console.WriteLine($"👀 Confirma o valor: [{findValue}]?");
var confirmValue = Console.ReadLine();
confirmValue ??= string.Empty;

if (!confirmValue.Equals("s", StringComparison.InvariantCultureIgnoreCase))
    return;

Console.WriteLine();
Console.WriteLine("👉🏻 Insira o novo valor:");
var replaceValue = Console.ReadLine();

Console.WriteLine($"👀 Confirma o valor: [{replaceValue}]?");
confirmValue = Console.ReadLine();
confirmValue ??= string.Empty;

if (!confirmValue.Equals("s", StringComparison.InvariantCultureIgnoreCase))
    return;
    
Console.WriteLine();
var allFiles = Directory.GetFiles(".", "*.fmt", SearchOption.TopDirectoryOnly);
var numberOfFiles = allFiles.Length;
var numberofModifiedFiles = 0;

if (numberOfFiles == 0)
{
    Console.WriteLine($"😿 Não foram encontrados arquivos FMT neste diretório!");
    return;
}

for (var i = 0; i < numberOfFiles; i++)
{
    var file = allFiles[i];
    var fileLines = File.ReadLines(file);
    
    var newFileLines = new List<string>();
    var modifiedFile = false;
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
        Console.WriteLine($"👉🏻 Arquivo Alterado: ${file}");
        continue;
    }
    
    Console.WriteLine($"👉🏻 Arquivo Nao Alterado: ${file}");
}

Console.WriteLine($"✅ Arquivos Encontrados: {numberOfFiles}");
Console.WriteLine($"✅ Arquivos Alterados: {numberofModifiedFiles}");

Console.WriteLine();
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();