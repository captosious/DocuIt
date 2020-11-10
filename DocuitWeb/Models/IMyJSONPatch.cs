using System;
using System.Collections;
using System.Collections.Generic;

namespace DocuitWeb.Models
{
    public interface IMyJSONPatch
    {
        //public string PatchDocument { set; get; }
        public void Keys(string key, string value);
        public void Values(string field, string value);
        public void ClearValues();
        public void ClearKeys();
    }

    public class MyJSONPatch : IMyJSONPatch
    {
        Dictionary<string, string> valuesDictionary = new Dictionary<string, string>();
        Dictionary<string, string> keysDictionary = new Dictionary<string, string>();

        public string PatchDocument(string table)
        {
            string patchHeader = "{'" + table + "': [" + "{'CompanyId': 1}],'Patching': [";
            string patchFile = "{'" + table + "': [{'CompanyId': 1}],'Patching': [";

            foreach (var keyValue in valuesDictionary)
            {
                patchHeader = patchHeader + "{'op':'replace','path':'" + keyValue.Key + "', 'value':'" + keyValue.Value + "'}";
            }

            foreach (var keyValue in valuesDictionary)
            {
                patchFile = patchFile + "{'op':'replace','path':'" + keyValue.Key + "', 'value':'" + keyValue.Value + "'}";
            }
            patchFile = patchFile + "]}";

            if (valuesDictionary.Count > 0 & keysDictionary.Count >0)
            {
                return null;
            }
            else
            {
                return patchFile;
            }
        }
        
        public void Values(string field, string value)
        {
            valuesDictionary.Add(field, value);
        }

        public void ClearValues()
        {
            valuesDictionary.Clear();
        }

        public void Keys(string key, string value)
        {
            keysDictionary.Add(key, value);
        }

        public void ClearKeys()
        {
            keysDictionary.Clear();
        }
    }
}
