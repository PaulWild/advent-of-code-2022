namespace AdventOfCode.Days;

public class Day07 : ISolution
{
    public string PartOne(IEnumerable<string> input)
    {
        var fileSystem = ParseFileSystem(input);
        
        return fileSystem!.AllFileSizes().Where(x => x.size < 100000).Select(x => x.size).Sum().ToString();
    }

    private static Directory? ParseFileSystem(IEnumerable<string> input)
    {
        Directory? fileSystem = null;
        Directory? currentDirectory = null;
        foreach (var row in input)
        {
            var command = row.Split(" ");

            switch (command[0])
            {
                case "$" when command[1] == "cd" && command[2] == "/":
                    fileSystem = new Directory(null, "/");
                    currentDirectory = fileSystem;
                    break;
                case "$" when command[1] == "cd" && command[2] == "..":
                    currentDirectory = currentDirectory!.Parent;
                    break;
                case "$" when command[1] == "cd":
                    currentDirectory = currentDirectory!.Directories.First(x => x.Name == command[2]);
                    break;
                case "$":
                    // can ignore ls for now
                    break;
                case "dir":
                    currentDirectory!.Directories.Add(new Directory(currentDirectory, command[1]));
                    break;
                default:
                    currentDirectory!.Files.Add((command[1], long.Parse(command[0])));
                    break;
            }
        }

        return fileSystem;
    }

    public string PartTwo(IEnumerable<string> input)
    {
        var fileSystem = ParseFileSystem(input);

        const int totalFileSize = 70000000;
        const int required = 30000000;
        var free = totalFileSize - fileSystem!.FileSize();
        var toFree = required - free;
        
        var viable = fileSystem.AllFileSizes().Where(x => x.size > toFree);
        return viable.MinBy(x => x.size).size.ToString();
    }

    public int Day => 07;
}

public class Directory
{
    public Directory(Directory? parent, string name)
    {
        Parent = parent;
        Name = name;
        Files = new List<(string file, long size)>();
        Directories = new List<Directory>();

    }

    public List<(string file, long size)> Files { get; }

    public List<Directory> Directories { get; }

    public string Name { get; }

    public Directory? Parent { get; }

    public long FileSize()
    {
        var currentDirSize = Files.Select(x => x.size).Sum();
        var childDirectories = Directories.Select(x => x.FileSize()).Sum();

        return currentDirSize + childDirectories;

    }

    public IEnumerable<(string name, long size)> AllFileSizes()
    {
        var toReturn = new List<(string name, long size)> { (Name, FileSize()) };
        foreach (var directory in Directories)       
        {
            toReturn.AddRange(directory.AllFileSizes());
        }

        return toReturn;
    }
}