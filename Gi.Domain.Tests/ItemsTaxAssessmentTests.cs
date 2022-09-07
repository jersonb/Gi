namespace Gi.Domain.Tests;

public class ItemsTaxAssessmentTests
{
    [Fact]
    public void Test1()
    {
        var readedlines = new List<string>
        {
            "|8505|0||||1|",
            "|8515|1|",
            "|8525|1127.02.00985|CAIXA PAPELAO 00 350x180x220  FBC-212|48191000|PC||0000|",
            "|8525|STS-5509|BOMBAS SUBM. COMPLETAS REF. ST-5509 (SUBWELL)|84137010|PC||0000|",
            "|8525|STS-8014|BOMBAS SUBM. COMPLETAS REF. ST-8014 (SUBWELL)|84137010|PC||0000|",
            "|8505|1|41560|18032015|0|1|",
            "|8515|2|",
            "|8525|97X.01.246072|BOMBA THA-16 AL 3,0 CV MONOF.IP21.127/220-254V.|84137080|PC|0,00|0000|",
            "|8525|97X.01.9780222|BOMBA TJ-12/30 AL 1/2 CV MONOF.IP21.WPUMP-220V.|84137080|PC||0000|",
            "|8525|97X.01.9780222|BOMBA TJ-12/30 AL 1/2 CV MONOF.IP21.WPUMP-220V.|84137080|PC|75,00|0000|",
            "|8525|97X.01.9780222|BOMBA TJ-12/30 AL 1/2 CV MONOF.IP21.WPUMP-220V.|84137080|PC|75,00|0000|",
        };
        var spedFile = new SpedFile(readedlines);
        
        var lines = spedFile.Lines;
        
        var result = lines
        .Where(l => l.Value.Register == "8515")
        .Select(l => new
        {
            Position = l.Key,
            Assessment = l.Value.Content[1],
            LineApuration = lines[l.Key - 1],
            Items = lines.Where(x => x.Key > l.Key).TakeWhile(x => x.Value.Register == "8525")
        });

        Assert.Equal(2, result.Count());
        Assert.Equal(2, result.First().Position);
        Assert.Equal("1", result.First().Assessment);
        Assert.Equal(7, result.Last().Position);
        Assert.Equal("2", result.Last().Assessment);
        Assert.Equal("8505", result.First().LineApuration.Register);
        Assert.Equal("8505", result.Last().LineApuration.Register);
        Assert.Equal("", result.First().LineApuration.Content[2]);
        Assert.Equal("41560", result.Last().LineApuration.Content[2]);
        Assert.Equal(3, result.First().Items.Count());
        Assert.Equal(4, result.Last().Items.Count());
    }
}
