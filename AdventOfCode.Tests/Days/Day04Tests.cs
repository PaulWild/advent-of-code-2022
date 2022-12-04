using AdventOfCode.Days;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Tests.Days;


public class Day04Tests
{
    private readonly ISolution _sut = new Day04();
    
    private readonly string[] _testData =
    {
        "2-4,6-8",
        "2-3,4-5",
        "5-7,7-9",
        "2-8,3-7",
        "6-6,4-6",
        "2-6,4-8"
    };


    [Fact]
    public void PartOne_WhenCalled_DoesNotThrowNotImplementedException()
    {
        Action act = () => _sut.PartOne(_sut.Input());

        act.Should().NotThrow<NotImplementedException>();
    }
    
    [Fact]
    public void PartOne_WhenCalled_ReturnsCorrectTestAnswer()
    {
        var actual = _sut.PartOne(_testData);

        actual.Should().Be("2");
    }
    
    [Fact]
    public void PartOne_WithAllEdgeCases_ReturnsCorrectTestAnswer()
    {
        var testData = new[] {
            "1-1,1-1",
            "2-10,2-10",
            "2-4,2-5",
            "2-5,2-4",
            "2-10,6-10",
            "6-10,2-10",
            "2-10,2-2",
            "2-2,2-10",
            "2-10,10-10",
            "10-10,2-10",
        };
        var actual = _sut.PartOne(testData);

        actual.Should().Be("10");
    }
    
    [Fact]
    public void PartOne_WithAllNegativeEdgeCases_ReturnsCorrectTestAnswer()
    {
        var testData = new[] {
            "1-1,2-2",
            "23-45,30-47",
            "30-47,23-45",
            "15-40,40-41"
        };
        var actual = _sut.PartOne(testData);

        actual.Should().Be("0");
    }


    [Fact]
    public void PartTwo_WhenCalled_DoesNotThrowNotImplementedException()
    {
        Action act = () => _sut.PartTwo(_sut.Input());

        act.Should().NotThrow<NotImplementedException>();
    }
    
    [Fact]
    public void PartTwo_WhenCalled_ReturnsCorrectTestAnswer()
    {
        var actual = _sut.PartTwo(_testData);

        actual.Should().Be("SomeString");
    }
}