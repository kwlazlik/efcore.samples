using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{



   public class Order : Entity<Order>
   {
      public string Description { get; set; }
   }

   public class OrderForHireStatus : Enumeration<OrderForHireStatus, string>
   {
      public static OrderForHireStatus New => new OrderForHireStatus("new", "nowy");
      public static OrderForHireStatus InProgress => new OrderForHireStatus("in-progress", "in progress");
      public static OrderForHireStatus Finished => new OrderForHireStatus("finished", "zakonczony");

      protected OrderForHireStatus(string value) : base(value)
      {
      }

      protected OrderForHireStatus(string value, string displayName) : base(value, displayName)
      {
      }
   }

   public class OrderForScansStatus : Enumeration<OrderForScansStatus, string>
   {
      public static OrderForScansStatus New => new OrderForScansStatus("new", "nowy");
      public static OrderForScansStatus InProgress => new OrderForScansStatus("in-progress", "in progress");
      public static OrderForScansStatus Rejected => new OrderForScansStatus("rejected", "odrzucone");
      public static OrderForScansStatus Finished => new OrderForScansStatus("finished", "zakonczony");

      protected OrderForScansStatus(string value) : base(value)
      {
      }

      protected OrderForScansStatus(string value, string displayName) : base(value, displayName)
      {
      }
   }

   public class OrderForHire : Order
   {
      public OrderForHireStatus Status { get; set; }
   }

   public class OrderForScans : Order
   {
      public OrderForScansStatus Status { get; set; }
   }
}
