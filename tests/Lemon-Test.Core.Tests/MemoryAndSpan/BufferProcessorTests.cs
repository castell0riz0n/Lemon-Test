using Lemon_Test.Core.MemoryAndSpan;

namespace Lemon_Test.Core.Tests.MemoryAndSpan;

public class BufferProcessorTests
{
    [Fact]
    public void ProcessBuffer_WithValidData_DoublesAllValues()
    {
        // Arrange
        var processor = new BufferProcessor();
        var originalData = new byte[] { 1, 2, 3, 4, 5 };
        var buffer = new Memory<byte>(originalData);

        // Act
        processor.ProcessBuffer(buffer);

        // Assert - Memory content validation
        var expectedData = new byte[] { 2, 4, 6, 8, 10 };
        Assert.True(expectedData.AsSpan().SequenceEqual(buffer.Span));
        
        // Verify memory properties
        Assert.Equal(5, buffer.Length);
        Assert.False(buffer.IsEmpty);
    }

    [Fact]
    public void ExtractSubstring_WithValidRange_ReturnsCorrectSpan()
    {
        // Arrange
        var processor = new BufferProcessor();
        var text = "Hello, World!".AsMemory();

        // Act
        var substring = processor.ExtractSubstring(text, 7, 5); // "World"

        // Assert - Span content validation
        Assert.True("World".AsSpan().SequenceEqual(substring));
        Assert.Equal(5, substring.Length);
        Assert.False(substring.IsEmpty);
    }

    [Fact]
    public void CountWords_WithValidText_ReturnsCorrectCount()
    {
        // Arrange
        var processor = new BufferProcessor();
        var text = "Hello world this is a test".AsSpan();

        // Act
        var wordCount = processor.CountWords(text);

        // Assert
        Assert.Equal(6, wordCount);
    }

    [Fact]
    public void CountWords_WithEmptySpan_ReturnsZero()
    {
        // Arrange
        var processor = new BufferProcessor();
        var emptyText = ReadOnlySpan<char>.Empty;

        // Act
        var wordCount = processor.CountWords(emptyText);

        // Assert - Empty span handling
        Assert.Equal(0, wordCount);
    }

    [Fact]
    public void ContainsPattern_WithMatchingPattern_ReturnsTrue()
    {
        // Arrange
        var processor = new BufferProcessor();
        var data = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
        var pattern = new byte[] { 3, 4, 5 };

        // Act
        var contains = processor.ContainsPattern(data, pattern);

        // Assert - Pattern matching in spans
        Assert.True(contains);
    }

    [Fact]
    public void CreateWindows_WithValidInput_ReturnsCorrectWindows()
    {
        // Arrange
        var windowProcessor = new MemoryWindowProcessor();
        var data = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var memory = new Memory<int>(data);

        // Act
        var windows = windowProcessor.CreateWindows(memory, 3);

        // Assert - Memory window validation
        Assert.Equal(2, windows.Count); // 8 / 3 = 2 complete windows (with remainder)
        
        // Verify first window
        Assert.Equal(3, windows[0].Length);
        Assert.True(new int[] { 1, 2, 3 }.AsSpan().SequenceEqual(windows[0].Span));
        
        // Verify second window  
        Assert.Equal(3, windows[1].Length);
        Assert.True(new int[] { 4, 5, 6 }.AsSpan().SequenceEqual(windows[1].Span));
    }

    [Fact]
    public void CreateOverlappingWindows_WithValidInput_ReturnsOverlappingWindows()
    {
        // Arrange
        var windowProcessor = new MemoryWindowProcessor();
        var text = "ABCDEFGH".AsMemory();

        // Act
        var windows = windowProcessor.CreateOverlappingWindows(text, windowSize: 3, step: 2);

        // Assert - Overlapping memory windows
        Assert.Equal(3, windows.Count); // Starting at positions 0, 2, 4
        
        Assert.True("ABC".AsSpan().SequenceEqual(windows[0].Span));
        Assert.True("CDE".AsSpan().SequenceEqual(windows[1].Span));
        Assert.True("EFG".AsSpan().SequenceEqual(windows[2].Span));
    }

    // TODO: Add more memory and span test cases:
    // - Test memory slicing operations with Assert.Equal for content
    // - Practice ReadOnlyMemory vs Memory testing patterns
    // - Test span boundary conditions and empty spans
    // - Use SequenceEqual for comparing span contents
    // - Test memory reference equality vs content equality
}
