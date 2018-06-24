// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System.Collections.Generic;
using System.Linq;

namespace IQToolkit
{
   public struct DeferredValue<T> : IDeferLoadable
   {
      private IEnumerable<T> source;
      private T value;

      public DeferredValue(T value)
      {
         this.value = value;
         source = null;
         IsLoaded = true;
      }

      public DeferredValue(IEnumerable<T> source)
      {
         this.source = source;
         IsLoaded = false;
         value = default;
      }

      public void Load()
      {
         if (source != null)
         {
            value = source.SingleOrDefault();
            IsLoaded = true;
         }
      }

      public bool IsLoaded { get; private set; }

      public bool IsAssigned => IsLoaded && source == null;

      private void Check()
      {
         if (!IsLoaded) Load();
      }

      public T Value
      {
         get
         {
            Check();
            return value;
         }

         set
         {
            this.value = value;
            IsLoaded = true;
            source = null;
         }
      }
   }
}
