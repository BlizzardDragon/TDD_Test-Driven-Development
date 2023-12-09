using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class CharacterStatsComponent : IParameterComponent<CharacterStatsTypes>
{
    [SerializeReference]
    private List<IParameter> _parameters;

    public CharacterStatsComponent()
    {
        _parameters = new List<IParameter>();
    }

    public CharacterStatsComponent(params IParameter[] parameters)
    {
        _parameters = new List<IParameter>(parameters);
    }


    public TValue GetParameter<TValue>(CharacterStatsTypes type)
    {
        for (int i = 0, count = _parameters.Count; i < count; i++)
        {
            var parameter = (IParameter<CharacterStatsTypes>)_parameters[i];
            if (parameter.Type == type && parameter is IParameter<TValue, CharacterStatsTypes> tParameter)
            {
                return tParameter.Value;
            }
        }

        throw new Exception($"Stats {type} is not found!");
    }

    public bool TryGetParameter<TValue>(CharacterStatsTypes type, out TValue value)
    {
        for (int i = 0, count = _parameters.Count; i < count; i++)
        {
            var parameter = (IParameter<CharacterStatsTypes>)_parameters[i];
            if (parameter.Type == type && parameter is IParameter<TValue, CharacterStatsTypes> tParameter)
            {
                value = tParameter.Value;
                return true;
            }
        }

        value = default;
        return false;
    }
}