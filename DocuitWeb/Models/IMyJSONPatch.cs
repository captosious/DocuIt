using System;
using System.Collections;
using System.Collections.Generic;

namespace DocuitWeb.Models
{
    public interface IMyJSONPatch
    {
        public string PatchDocument { get; }
        public void Values(string field, string value);
        public void Clear();
    }

    public class MyJSONPatch : IMyJSONPatch
    {
        string IMyJSONPatch.PatchDocument => throw new NotImplementedException();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public string PatchDocument()
        {
            string patchFile = "{'Company': [{'CompanyId': 1}],'Patching': [";

            foreach (var keyValue in dictionary)
            {
                patchFile = patchFile + "{'op':'replace','path':'" + keyValue.Key + "', 'value':'" + keyValue.Value + "'}";
            }
            patchFile = patchFile + "]}";
            return patchFile;
        }
        
        public void Values(string field, string value)
        {
            dictionary.Add(field, value);
        }

        public void Clear()
        {
            dictionary.Clear();
        }
    }
}
