using PipeWire.Spa;
using Xunit;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Tests;

public unsafe class PipeWirePropertiesTests
{
    public PipeWirePropertiesTests() => PipeWireLibrary.Init();

    [Fact]
    public void TypedGettersParseTheStoredStrings()
    {
        using var properties = PipeWireProperties.From(
            "object.serial", "1234567890123",
            "node.id", "42",
            "node.latency", "-7",
            "node.autoconnect", "true");

        Assert.Equal(1234567890123ul, properties.GetUInt64("object.serial"));
        Assert.Equal(1234567890123L, properties.GetInt64("object.serial"));
        Assert.Equal(42u, properties.GetUInt32("node.id"));
        Assert.Equal(-7, properties.GetInt32("node.latency"));
        Assert.True(properties.GetBool("node.autoconnect"));
    }

    [Fact]
    public void TypedGettersAcceptTheUtf8KeyConstants()
    {
        using var properties = PipeWireProperties.From("object.serial", "99");

        Assert.Equal(99ul, properties.GetUInt64(PW_KEY_OBJECT_SERIAL));
        Assert.True(properties.TryGetUInt64(PW_KEY_OBJECT_SERIAL, out ulong serial));
        Assert.Equal(99ul, serial);
        Assert.Equal("99", properties.Get(PW_KEY_OBJECT_SERIAL));
    }

    [Fact]
    public void MissingAndUnparsableValuesFallBackToTheDefault()
    {
        using var properties = PipeWireProperties.From("object.serial", "not-a-number");

        Assert.False(properties.TryGetUInt64("object.serial", out _));
        Assert.Equal(7ul, properties.GetUInt64("object.serial", 7));

        Assert.False(properties.TryGetUInt64("missing.key", out _));
        Assert.Equal(0ul, properties.GetUInt64("missing.key"));
        Assert.Equal(5u, properties.GetUInt32("missing.key", 5));
        Assert.True(properties.GetBool("missing.key", true));
    }

    [Fact]
    public void ABorrowedViewDoesNotOwnItsHandle()
    {
        using var owner = PipeWireProperties.From("object.serial", "1");

        PipeWireProperties view = PipeWireProperties.Borrow(owner.Handle);

        Assert.True(owner.IsOwned);
        Assert.False(view.IsOwned);
        Assert.True(view.IsReadOnly);
        Assert.Equal(1ul, view.GetUInt64(PW_KEY_OBJECT_SERIAL));

        view.Dispose();

        Assert.Equal(1ul, owner.GetUInt64(PW_KEY_OBJECT_SERIAL));
    }

    [Fact]
    public void ABorrowedViewSeesTheOwnersUpdates()
    {
        using var owner = PipeWireProperties.From("object.serial", "1");
        PipeWireProperties view = PipeWireProperties.Borrow(owner.Handle);

        owner.Set("object.serial", "2");

        Assert.Equal(2ul, view.GetUInt64(PW_KEY_OBJECT_SERIAL));
    }

    [Fact]
    public void ABorrowedViewRejectsWrites()
    {
        using var owner = PipeWireProperties.From("object.serial", "1");
        PipeWireProperties view = PipeWireProperties.Borrow(owner.Handle);

        Assert.Throws<InvalidOperationException>(() => view.Set("object.serial", "2"));
    }

    [Fact]
    public void EntriesBuildPropertiesFromTheUtf8KeyConstants()
    {
        using var properties = PipeWireProperties.FromEntries(
            new(PW_KEY_MEDIA_TYPE, "Audio"),
            new(PW_KEY_MEDIA_CATEGORY, "Playback"),
            new(PW_KEY_OBJECT_SERIAL, "1234567890123"));

        Assert.Equal(3, properties.Count);
        Assert.Equal("Audio", properties.Get(PW_KEY_MEDIA_TYPE));
        Assert.Equal("Playback", properties["media.category"]);
        Assert.Equal(1234567890123ul, properties.GetUInt64(PW_KEY_OBJECT_SERIAL));
    }

    [Fact]
    public void EntriesMixUtf8AndStringKeys()
    {
        using var properties = PipeWireProperties.FromEntries(
            new(PW_KEY_MEDIA_TYPE, "Audio"),
            new("custom.key", "value"),
            new(PW_KEY_MEDIA_ROLE, "Music"));

        Assert.Equal("Audio", properties["media.type"]);
        Assert.Equal("value", properties["custom.key"]);
        Assert.Equal("Music", properties["media.role"]);
    }

    [Fact]
    public void AnEntryStripsATrailingNulFromAUtf8Key()
    {
        var entry = new SpaDictionaryEntry("media.type\0"u8, "Audio");

        Assert.Equal("media.type", entry.Key);
        Assert.Equal(10, entry.KeyUtf8.Length);

        using var properties = PipeWireProperties.FromEntries(entry);
        Assert.Equal("Audio", properties["media.type"]);
    }

    [Fact]
    public void ANullEntryValueRoundTripsAsNull()
    {
        var entry = new SpaDictionaryEntry(PW_KEY_MEDIA_TYPE, null);

        Assert.Equal("media.type", entry.Key);
        Assert.Null(entry.Value);
        Assert.Equal("media.type=", entry.ToString());
    }

    [Fact]
    public void SetAcceptsAUtf8Key()
    {
        using var properties = new PipeWireProperties();

        Assert.True(properties.Set(PW_KEY_OBJECT_SERIAL, "5"));
        Assert.Equal(5ul, properties.GetUInt64(PW_KEY_OBJECT_SERIAL));
    }

    [Fact]
    public void ADictionaryBuiltFromEntriesMatchesTheStringPath()
    {
        using SpaDictionary fromEntries = SpaDictionary.FromEntries(
            new SpaDictionaryEntry(PW_KEY_MEDIA_TYPE, "Audio"),
            new SpaDictionaryEntry("node.name", "test"));

        using SpaDictionary fromStrings = SpaDictionary.From("media.type", "Audio", "node.name", "test");

        Assert.Equal(SpaDictionary.ToDictionary(fromStrings.Handle), SpaDictionary.ToDictionary(fromEntries.Handle));
    }
}
