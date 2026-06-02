using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PetFamily.Domain.Shared;

public class ValueObjectList<T> : IReadOnlyList<T>
{
    public IReadOnlyList<T> Values { get; } = null!;
    public int Count => Values.Count;
    public T this[int index] => Values[index];
    
    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
    
    private ValueObjectList() {}
    private ValueObjectList(List<T> list) => Values = list.AsReadOnly();
    public ValueObjectList(IReadOnlyList<T> list)
    {
        Values = new List<T>(list).AsReadOnly();
    }
    
    public string Serialize() => JsonSerializer.Serialize(this);
    public static ValueObjectList<T>? Deserialize(string json) 
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;

        var value = JsonSerializer.Deserialize<List<T>>(json);
        
        if (value == null)
            return null;
        
        var result = new ValueObjectList<T>(value);
        return result;
    }
}