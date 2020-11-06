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

      public bool Matches<T>(List<T> other, out T t) where T : ReceiverEv {
        t = other.Find(x => {
          string[] splitQuery = x.query.Trim().Split(',');
          if (splitQuery.Length == 0) return false;
          if (splitQuery.Length <= 1) return id == splitQuery[0];
          // ugly but leave it for now
          return splitQuery[0] == id && splitQuery[1] == data[0];
        });
        return t != null;
      }
    }

    public class ReceiverEv {
      public string query;
      public List<string> data = new List<string>();
    }
  }
}