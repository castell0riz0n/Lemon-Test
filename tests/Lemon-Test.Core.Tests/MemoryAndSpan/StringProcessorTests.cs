using Lemon_Test.Core.MemoryAndSpan;

namespace Lemon_Test.Core.Tests.MemoryAndSpan;

public class StringProcessorTests
{
    [Fact]
    public void GetFirstWord_WithMultipleWords_ReturnsFirstWord()
    {
        // Arrange
        var processor = new StringProcessor();
        var text = "Hello world test".AsSpan();

        // Act
        var firstWord = processor.GetFirstWord(text);

        // Assert - Span string operations
        Assert.True("Hello".AsSpan().SequenceEqual(firstWord));
        Assert.Equal(5, firstWord.Length);
    }

    [Fact]
    public void GetFirstWord_WithSingleWord_ReturnsEntireText()
    {
        // Arrange
        var processor = new StringProcessor();
        var text = "Hello".AsSpan();

        // Act
        var firstWord = processor.GetFirstWord(text);

        // Assert - Single word span
        Assert.True(text.SequenceEqual(firstWord));
        Assert.Equal(text.Length, firstWord.Length);
    }

    [Fact]
    public void ContainsExclamation_WithExclamation_ReturnsTrue()
    {
        // Arrange
        var processor = new StringProcessor();
        var text = "Hello world!".AsSpan();

        // Act
        var hasExclamation = processor.ContainsExclamation(text);

        // Assert - Character search in spans
        Assert.True(hasExclamation);
    }

    [Fact]
    public void ReverseInPlace_WithValidText_ReversesCharacters()
    {
        // Arrange
        var processor = new StringProcessor();
        var chars = "Hello".ToCharArray();
        var span = chars.AsSpan();

        // Act
        processor.ReverseInPlace(span);

        // Assert - In-place span modification
        Assert.True("olleH".AsSpan().SequenceEqual(span));
        
        // Verify original array was modified
        Assert.Equal("olleH", new string(chars));
    }

    [Fact]
    public void SplitIntoLines_WithMultilineText_ReturnsSeparateLines()
    {
        // Arrange
        var processor = new StringProcessor();
        var text = "Line 1\nLine 2\r\nLine 3".AsMemory();

        // Act
        var lines = processor.SplitIntoLines(text);

        // Assert - Memory-based line splitting
        Assert.Equal(3, lines.Count);
        
        Assert.True("Line 1".AsSpan().SequenceEqual(lines[0].Span));
        Assert.True("Line 2".AsSpan().SequenceEqual(lines[1].Span));
        Assert.True("Line 3".AsSpan().SequenceEqual(lines[2].Span));
    }

    [Fact]
    public void SplitIntoLines_WithEmptyText_ReturnsEmptyList()
    {
        // Arrange
        var processor = new StringProcessor();
        var emptyText = ReadOnlyMemory<char>.Empty;

        // Act
        var lines = processor.SplitIntoLines(emptyText);

        // Assert - Empty memory handling
        Assert.Empty(lines);
    }

    [Fact]
    public void DataCompressor_WithRepeatedBytes_CompressesData()
    {
        // Arrange
        var compressor = new DataCompressor();
        var data = new byte[] { 1, 1, 1, 2, 2, 3 }; // 3x1, 2x2, 1x3
        var readOnlyData = new ReadOnlyMemory<byte>(data);

        // Act
        var compressed = compressor.Compress(readOnlyData);

        // Assert - Memory-based compression
        Assert.False(compressed.IsEmpty);
        Assert.True(compressed.Length > 0);
        
        // Verify compression format (count, value pairs)
        var compressedSpan = compressed.Span;
        Assert.Equal(3, compressedSpan[0]); // Count of 1s
        Assert.Equal(1, compressedSpan[1]); // Value 1
        Assert.Equal(2, compressedSpan[2]); // Count of 2s
        Assert.Equal(2, compressedSpan[3]); // Value 2
    }

    [Fact]
    public void DataCompressor_WithEmptyData_ReturnsEmptyMemory()
    {
        // Arrange
        var compressor = new DataCompressor();
        var emptyData = ReadOnlyMemory<byte>.Empty;

        // Act
        var compressed = compressor.Compress(emptyData);

        // Assert - Empty memory handling
        Assert.True(compressed.IsEmpty);
        Assert.Equal(0, compressed.Length);
    }

    // TODO: Add more memory and span test cases:
    // - Test ReadOnlyMemory vs Memory behavior differences
    // - Practice span slicing with various ranges
    // - Test memory allocation patterns and reuse
    // - Use SequenceEqual for comparing different memory types
    // - Test performance characteristics of span operations
}
