using System;
using System.Collections;
using System.Collections.Generic;

namespace DocuitWeb.Models
{
    public interface IMyJSONPatch
    {
        public string PatchDocument { get; }
        public void Values(string field, string value);
    }

    public class MyJSONPatch : IMyJSONPatch
    {
        string IMyJSONPatch.PatchDocument => throw new NotImplementedException();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public string PatchDocument()
        {
            string patchFile = "";

            foreach (var keyValue in dictionary)
            {
                keyValue.Key = "";
                keyValue.Value = "";
            }
        }
        
        public void Values(string field, string value)
        {
            dictionary.Add(field, value);
        }
    }
}
