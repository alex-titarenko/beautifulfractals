using System;
using System.Collections.Generic;


namespace TAlex.BeautifulFractals.KeyGenerator
{
    public interface IKeyGenerator
    {
        object Generate(IDictionary<string, string> inputs);
    }
}
