namespace Gi.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var path = "../../../resources/01.txt";
        Assert.True(File.Exists(path));
        var lines = File.ReadAllLines(path);

        foreach (var readedLine in lines)
        {
            var line = new Line(readedLine);

            Assert.True(line.Content.Length == 4);
        }
    }

    [Fact]
    public void Test2()
    {
        var path = "../../../resources/01.txt";

        var lines = File.ReadAllLines(path);

        var line = new Line(lines[0]);
        Assert.Equal("0000", line.Register);
        Assert.Equal("L01C01", line.Content.First());
        Assert.Equal("L01C04", line.Content.Last());
    }

    [Fact]
    public void Test3()
    {
        var path = "../../../resources/01.txt";
        
        var lines = File.ReadAllLines(path);
        var spedFile = new SpedFile(lines);

        Assert.True(spedFile.Lines.Count == 2);
    }

    public struct SpedFile
    {
        public SpedFile(IEnumerable<string> lines)
        {
            Lines = new();
            var lineCount = 1;
            foreach (var line in lines)
                Lines.Add(lineCount++, new Line(line));
        }
        public Dictionary<int, Line> Lines { get; }
    }
    public struct Line
    {
        public Line(string line)
        {
            if (!(line.StartsWith("|") && line.EndsWith("|")))
                throw new ArgumentException(nameof(line));

            var content = line.Split("|");

            Register = content[1];

            if (Register.Length != 4)
                throw new ArgumentException(nameof(Register));

            Content = content[2..^1];

        }

        public string Register { get; }
        public string[] Content { get; }
    }
}

