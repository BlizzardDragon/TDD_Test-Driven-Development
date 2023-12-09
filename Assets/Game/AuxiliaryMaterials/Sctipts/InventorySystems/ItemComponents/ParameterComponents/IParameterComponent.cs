public interface IParameterComponent <TParameterTypes>
{
    TValue GetParameter<TValue>(TParameterTypes type);

    bool TryGetParameter<TValue>(TParameterTypes type, out TValue value);
}