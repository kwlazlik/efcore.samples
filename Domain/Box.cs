using System.Collections.Generic;

namespace Domain
{
   public class Box : Entity<Box>
   {
      private readonly List<BoxDocumentationTypeInfo> _documentationTypeInfos = new List<BoxDocumentationTypeInfo>();

      public CidNumber Cid { get; private set; }

      public BoxStatus Status { get; set; }

      public bool IsSealed { get; set; }

      public IReadOnlyList<BoxDocumentationTypeInfo> DocumentationTypeInfos => _documentationTypeInfos;

      public Result UpdateCid(string cid)
      {
         return CidNumber.Create(cid).OnSuccess(cn => Cid = cn);
      }

      public void UpdateCid(CidNumber cid)
      {
         Cid = cid;
      }

      public void AddDocumentationTypeInfo(BoxDocumentationTypeInfo info)
      {
         _documentationTypeInfos.Add(info);
      }

      public static Box Create(CidNumber cid, BoxStatus status)
      {
         return new Box
         {
            Cid = cid,
            Status = status
         };
      }

      public static Result<Box> Create(string cid, BoxStatus status)
      {
         Box box = new Box
         {
            Status = status
         };

         Result<CidNumber> cidNumbeResult = CidNumber.Create(cid);

         return cidNumbeResult.OnSuccess(cn =>
         {
            box.UpdateCid(cn);
            return box;
         });
      }
   }
}
