using System;
using UnityEngine;

public interface IParameter
{
}

public interface IParameter<out TParameterTypes> : IParameter
{
    TParameterTypes Type { get; }
}

public interface IParameter<out TValue, TParameterTypes> : IParameter<TParameterTypes>
{
    TValue Value { get; }
}

[Serializable]
public abstract class Parameter<TValue, TParameterTypes> : IParameter<TValue, TParameterTypes>
{
    public TParameterTypes Type => _type;
    public TValue Value => _value;

    [SerializeField]
    private TParameterTypes _type;

    [SerializeField]
    private TValue _value;

    public Parameter()
    {
    }

    public Parameter(TParameterTypes type, TValue value)
    {
        _type = type;
        _value = value;
    }
}