using System.Collections.Generic;

namespace MyEncryption
{
    public interface IShift
    {
        void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText);
        void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text);
    }
}
