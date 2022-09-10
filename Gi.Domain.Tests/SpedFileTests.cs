namespace Gi.Domain.Tests;

public class SpedFileTests
{
    [Fact]
    public void Test1()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "|0001|L02C01|L02C02|L02C03|L02C04|",
            ""
        };

        var spedFile = new SpedFile(lines);
        Assert.Equal(2, spedFile.Lines.Count);
        Assert.Equal(1, spedFile.Lines.First().Key);
    }

    [Fact]
    public void Test2()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "0001|L02C01|L02C02|L02C03|L02C04|",
            ""
        };
        var ex = Assert.Throws<ArgumentException>(() => new SpedFile(lines));
        Assert.Equal("Linha 2: Problema de abertura da linha", ex.Message);
    }

    [Fact]
    public void Test3()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "|000|L02C01|L02C02|L02C03|L02C04|",
            ""
        };
        var ex = Assert.Throws<ArgumentException>(() => new SpedFile(lines));
        Assert.Equal("Linha 2: Problema de quantidade de caracteres no registro", ex.Message);
    }

    [Fact]
    public void Test4()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "|0000|L02C01|L02C02|L02C03|L02C04",
            ""
        };
        var ex = Assert.Throws<ArgumentException>(() => new SpedFile(lines));
        Assert.Equal("Linha 2: Problema de fechamento de linha", ex.Message);
    }

    [Fact]
    public void Test5()
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

        var taxAssessments = spedFile.TaxAssessmentReader.AllTaxAssessment;

        Assert.Equal(2, taxAssessments.Count());

        Assert.Equal("1", taxAssessments.First().Name);
        Assert.Equal("", taxAssessments.First().DecreeNumber);
        Assert.False(taxAssessments.First().IsIncentivized);
        Assert.Equal(3, taxAssessments.First().Items.Count());

        Assert.Equal("2", taxAssessments.Last().Name);
        Assert.Equal("41560", taxAssessments.Last().DecreeNumber);
        Assert.True(taxAssessments.Last().IsIncentivized);
        Assert.Equal(4, taxAssessments.Last().Items.Count());
    }

    [Fact]
    public void Test6()
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

        Assert.Equal(4, spedFile.TaxAssessmentReader.IncentivizedItems.Count());
        Assert.Equal(3, spedFile.TaxAssessmentReader.NonIncentivizedItems.Count());
    }
}