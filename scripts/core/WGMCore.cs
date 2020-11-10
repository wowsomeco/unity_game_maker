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

        if (t != null && component.Object.DebugMode) {
          Print.Log(string.Format("{0}|RECEIVES|{1}", component.Info, t.data.Flatten()), "cyan");
        }

        return t != null;
      }

      bool Compare(string lhs, string rhs) {
        const string indicator = "*";
        string l = lhs.Standardize();
        string r = rhs.Standardize();

        // check query whether it starts or ends with the special character
        bool startsWith = l.StartsWith(indicator);
        bool endsWith = l.EndsWith(indicator);

        if (startsWith || endsWith) {
          string normal = l.Replace(indicator, "");

          if (startsWith) return r.StartsWith(normal);
          else if (endsWith) return r.EndsWith(normal);
          else if (startsWith && endsWith) return r.Contains(normal);
        }

        // no special char found, compare normally
        return r == l;
      }
    }

    /// <summary>
    /// The base class of the Receiver event. 
    /// </summary>
    [Serializable]
    public class ReceiverEv {
      /// <summary>
      /// The query that will be compared to the SenderEv.
      /// the idea is the query might contain the special character '*' e.g.
      /// '*tw' means starts with comparison
      /// 'tw*' means ends with
      /// '*tw*' means contains in any
      /// 
      /// when there is no '*' given, the comparison will be performed normally.   
      /// TODO: we might need add more queries later eventually.
      /// </summary>
      public string query;
      /// <summary>
      /// The generic data that might vary accordingly.
      /// e.g. 
      /// for WGMTween, the list of data indicates the tween ids that will be looked into and played whenever the event gets triggered
      /// </summary>
      public List<string> data = new List<string>();
    }
  }
}