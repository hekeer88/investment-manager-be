namespace Base.Domain;

public class LangStr : Dictionary<string, string>
{

    private const string DefaultCulture = "et";

    public LangStr()
    {
        
    }

    public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
        
    }

    public LangStr(string value, string culture)
    {
        this[culture] = value;
    }

    public string? Translate(string? culture = null)
    {
        if (Count == 0) return null;

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        if (ContainsKey(culture))
        {
            return this[culture];
        }

        var neutralCulture = culture.Split("-")[0];
        
        if (ContainsKey(neutralCulture))
        {
            return this[neutralCulture];
        }
        
        if (ContainsKey(DefaultCulture))
        {
            return this[DefaultCulture];
        }

        return null;
    }

    public void SetTranslation(string value)
    {
        this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    }
    
    public override string ToString()
    {
        return Translate() ?? "No Translation";
    }

    public static implicit operator string(LangStr? langStr) => langStr?.ToString() ?? "null";
    
    public static implicit operator LangStr(string value) => new (value);

}