using System;

[Serializable]
public sealed class IntCharacterStats : Parameter<int, CharacterStatsTypes>
{
    public IntCharacterStats()
    {
    }

    public IntCharacterStats(CharacterStatsTypes type, int value) : base(type, value)
    {
    }
}

[Serializable]
public sealed class FloatCharacterStats : Parameter<float, CharacterStatsTypes>
{
    public FloatCharacterStats()
    {
    }

    public FloatCharacterStats(CharacterStatsTypes type, float value) : base(type, value)
    {
    }
}

[Serializable]
public sealed class StringCharacterStats : Parameter<string, CharacterStatsTypes>
{
    public StringCharacterStats()
    {
    }

    public StringCharacterStats(CharacterStatsTypes type, string value) : base(type, value)
    {
    }
}
