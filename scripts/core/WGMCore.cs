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
          if (splitQuery.Length == 1) return id.CompareStandard(splitQuery[0]);
          // this handles the comma separated query.
          // e.g. lg,done
          // TODO: need some more details re:this
          return id.CompareStandard(splitQuery[0]) && data[0].CompareStandard(splitQuery[1]);
        });

        if (t != null) {
          Print.Log(string.Format("{0}|RECEIVES|{1}", component.Info, t.data.Flatten()), "cyan");
        }

        return t != null;
      }
    }

    [Serializable]
    public class ReceiverEv {
      public string query;
      public List<string> data = new List<string>();
    }
  }
}