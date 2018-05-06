using System.Collections.Generic;

namespace ConsoleApp1.Domain
{
   public class Box : Entity<Box>
   {
      private readonly List<BoxDocumentationTypeInfo> _documentationTypeInfos = new List<BoxDocumentationTypeInfo>();

      public CidNumber Cid { get; private set; }

      public BoxStatus Status { get; set; }

      public bool IsSealed { get; set; }

      public IReadOnlyList<BoxDocumentationTypeInfo> DocumentationTypeInfos => _documentationTypeInfos;

      public static Result<Box> New(string cid)
      {
         Box box = new Box
         {
            Status = BoxStatus.New
         };
         Result result = box.UpdateCid(cid);
         if (result)
            return Result.Ok(box);
         else
            return Result.Fail<Box>(result.Error);
      }

      public Result UpdateCid(string cid)
      {
         Result<CidNumber> cidNumberResult = CidNumber.New(cid);

         if (cidNumberResult)
         {
            Cid = cidNumberResult.Value;
         }

         return cidNumberResult;
      }

      public void AddDocumentationTypeInfo(BoxDocumentationTypeInfo info)
      {
         _documentationTypeInfos.Add(info);
      }

   }


   public sealed class BoxStatus : Enumeration<BoxStatus, string>
   {
      public static BoxStatus New { get; } = new BoxStatus("New", "Nowy");
      public static BoxStatus Received { get; } = new BoxStatus("InUse", "Używany");

      private BoxStatus(string value) : base(value) { }

      private BoxStatus(string value, string displayName) : base(value, displayName) { }
   }

}
