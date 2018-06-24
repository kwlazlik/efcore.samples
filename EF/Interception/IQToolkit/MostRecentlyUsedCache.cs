﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace IQToolkit
{
   /// <summary>
   ///    Implements a cache over a most recently used list
   /// </summary>
   public class MostRecentlyUsedCache<T>
   {
      private readonly Func<T, T, bool> fnEquals;
      private readonly List<T> list;
      private readonly int maxSize;
      private readonly ReaderWriterLockSlim rwlock;
      private int version;

      public MostRecentlyUsedCache(int maxSize)
         : this(maxSize, EqualityComparer<T>.Default) { }

      public MostRecentlyUsedCache(int maxSize, IEqualityComparer<T> comparer)
         : this(maxSize, (x, y) => comparer.Equals(x, y)) { }

      public MostRecentlyUsedCache(int maxSize, Func<T, T, bool> fnEquals)
      {
         list = new List<T>();
         this.maxSize = maxSize;
         this.fnEquals = fnEquals;
         rwlock = new ReaderWriterLockSlim();
      }

      public int Count
      {
         get
         {
            rwlock.EnterReadLock();
            try
            {
               return list.Count;
            }
            finally
            {
               rwlock.ExitReadLock();
            }
         }
      }

      public void Clear()
      {
         rwlock.EnterWriteLock();
         try
         {
            list.Clear();
            version++;
         }
         finally
         {
            rwlock.ExitWriteLock();
         }
      }

      public bool Lookup(T item, bool add, out T cached)
      {
         cached = default;
         int cacheIndex = -1;

         rwlock.EnterReadLock();
         int version = this.version;
         try
         {
            FindCachedItem(item, out cached, out cacheIndex);
         }
         finally
         {
            rwlock.ExitReadLock();
         }

         // now update item in the list (only if we need to change its position or add it)
         if (cacheIndex != 0 && add)
         {
            rwlock.EnterWriteLock();
            try
            {
               // if list has changed find it again
               FindCachedItem(item, out cached, out cacheIndex);

               if (cacheIndex == -1)
               {
                  // this is first time in list, put at start
                  list.Insert(0, item);
                  cached = item;
               }
               else
               {
                  if (cacheIndex > 0)
                  {
                     // if item is not at start, move it to the start
                     list.RemoveAt(cacheIndex);
                     list.Insert(0, item);
                  }
               }

               // drop any items beyond max
               if (list.Count > maxSize) list.RemoveAt(list.Count - 1);

               this.version++;
            }
            finally
            {
               rwlock.ExitWriteLock();
            }
         }

         return cacheIndex >= 0;
      }

      private void FindCachedItem(T item, out T cached, out int index)
      {
         for (int i = 0, n = list.Count; i < n; i++)
         {
            cached = list[i];

            if (fnEquals(cached, item))
            {
               index = i;
               return;
            }
         }

         cached = default;
         index = -1;
      }
   }
}
