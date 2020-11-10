using System;
using System.Collections.Generic;

namespace Wowsome {
  namespace GameMaker {
    [Serializable]
    public class SenderEv {
      public string id;
      public List<string> data = new List<string>();

      public SenderEv(string theId, List<string> d) {
        id = theId;
        data = d;
      }

      public SenderEv(string theId, string d) : this(theId, new List<string> { d }) { }

      public bool Matches<T>(WGMComponent component, List<T> other, out T t) where T : ReceiverEv {
        t = other.Find(x => {
          string[] splitQuery = x.query.Trim().Split(',');
          // dont do anything if it's nothing
          if (splitQuery.Length == 0) return false;
          // it means that the query only contains single string e.g. 'done'
          bool matches = Compare(splitQuery[0], id);
          if (splitQuery.Length == 1) return matches;
          // this handles the comma separated query.
          // e.g. lg,done where 'lg' is the id and 'done' is the event          
          return matches && Compare(splitQuery[1], data[0]);
        });

        if (t != null) {
          Print.Log(string.Format("{0}|RECEIVES|{1}", component.Info, t.data.Flatten()), "cyan");
        }

        return t != null;
      }

      bool Compare(string lhs, string rhs) {
        const string indicator = "*";
        string l = lhs.Standardize();
        string r = rhs.Standardize();

        if (l.StartsWith(indicator)) return r.StartsWith(l.Replace(indicator, ""));
        if (l.EndsWith(indicator)) return r.EndsWith(l.Replace(indicator, ""));

        return r == l;
      }
    }

    [Serializable]
    public class ReceiverEv {
      public string query;
      public List<string> data = new List<string>();
    }
  }
}