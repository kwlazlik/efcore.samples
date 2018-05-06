using System;

namespace ConsoleApp1.Domain
{
   public class BoxDocumentationTypeInfo : DocumentationTypeInfo<BoxDocumentationTypeInfo>
   {
      public static BoxDocumentationTypeInfo New(DocumentationType type, DateTime from, DateTime to) => new BoxDocumentationTypeInfo
      {
         Type = type,
         ValidFrom = from,
         ValidTo = to
      };
   }
}
