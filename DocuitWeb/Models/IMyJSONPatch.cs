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
        IEnumerable list = new List();

        public string PatchDocument()
        {
            throw new NotImplementedException();
        }
         
        public void Values(string field, string value)
        {
            throw new NotImplementedException();
        }
    }

    internal class JSONPair
    {
        string field { get; set; }
        string value { get; set; }

        public JSONPair()
        {
             
        }
    }
}
