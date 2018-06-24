// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Collections.Generic;

namespace IQToolkit
{
   public class ScopedDictionary<TKey, TValue>
   {
      private readonly Dictionary<TKey, TValue> map;
      private readonly ScopedDictionary<TKey, TValue> previous;

      public ScopedDictionary(ScopedDictionary<TKey, TValue> previous)
      {
         this.previous = previous;
         map = new Dictionary<TKey, TValue>();
      }

      public ScopedDictionary(ScopedDictionary<TKey, TValue> previous, IEnumerable<KeyValuePair<TKey, TValue>> pairs)
         : this(previous)
      {
         foreach (KeyValuePair<TKey, TValue> p in pairs) map.Add(p.Key, p.Value);
      }

      public void Add(TKey key, TValue value) => map.Add(key, value);

      public bool TryGetValue(TKey key, out TValue value)
      {
         for (ScopedDictionary<TKey, TValue> scope = this; scope != null; scope = scope.previous)
            if (scope.map.TryGetValue(key, out value))
               return true;
         value = default;
         return false;
      }

      public bool ContainsKey(TKey key)
      {
         for (ScopedDictionary<TKey, TValue> scope = this; scope != null; scope = scope.previous)
            if (scope.map.ContainsKey(key))
               return true;
         return false;
      }
   }
}
