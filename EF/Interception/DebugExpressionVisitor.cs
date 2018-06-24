using System.Linq.Expressions;

namespace EFC.Interception
{
   public class DebugExpressionVisitor : ExpressionVisitor
   {
      public override Expression Visit(Expression node) => base.Visit(node);

      protected override Expression VisitBinary(BinaryExpression node) => base.VisitBinary(node);

      protected override Expression VisitBlock(BlockExpression node) => base.VisitBlock(node);

      protected override CatchBlock VisitCatchBlock(CatchBlock node) => base.VisitCatchBlock(node);

      protected override Expression VisitConditional(ConditionalExpression node) => base.VisitConditional(node);

      protected override Expression VisitConstant(ConstantExpression node) => base.VisitConstant(node);

      protected override Expression VisitDebugInfo(DebugInfoExpression node) => base.VisitDebugInfo(node);

      protected override Expression VisitDefault(DefaultExpression node) => base.VisitDefault(node);

      protected override Expression VisitDynamic(DynamicExpression node) => base.VisitDynamic(node);

      protected override ElementInit VisitElementInit(ElementInit node) => base.VisitElementInit(node);

      protected override Expression VisitExtension(Expression node) => base.VisitExtension(node);

      protected override Expression VisitGoto(GotoExpression node) => base.VisitGoto(node);

      protected override Expression VisitIndex(IndexExpression node) => base.VisitIndex(node);

      protected override Expression VisitInvocation(InvocationExpression node) => base.VisitInvocation(node);

      protected override Expression VisitLabel(LabelExpression node) => base.VisitLabel(node);

      protected override LabelTarget VisitLabelTarget(LabelTarget node) => base.VisitLabelTarget(node);

      protected override Expression VisitLambda<T>(Expression<T> node) => base.VisitLambda(node);

      protected override Expression VisitListInit(ListInitExpression node) => base.VisitListInit(node);

      protected override Expression VisitLoop(LoopExpression node) => base.VisitLoop(node);

      protected override Expression VisitMember(MemberExpression node) => base.VisitMember(node);

      protected override MemberAssignment VisitMemberAssignment(MemberAssignment node) => base.VisitMemberAssignment(node);

      protected override MemberBinding VisitMemberBinding(MemberBinding node) => base.VisitMemberBinding(node);

      protected override Expression VisitMemberInit(MemberInitExpression node) => base.VisitMemberInit(node);

      protected override MemberListBinding VisitMemberListBinding(MemberListBinding node) => base.VisitMemberListBinding(node);

      protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node) => base.VisitMemberMemberBinding(node);

      protected override Expression VisitMethodCall(MethodCallExpression node) => base.VisitMethodCall(node);

      protected override Expression VisitNew(NewExpression node) => base.VisitNew(node);

      protected override Expression VisitNewArray(NewArrayExpression node) => base.VisitNewArray(node);

      protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(node);

      protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node) => base.VisitRuntimeVariables(node);

      protected override Expression VisitSwitch(SwitchExpression node) => base.VisitSwitch(node);

      protected override SwitchCase VisitSwitchCase(SwitchCase node) => base.VisitSwitchCase(node);

      protected override Expression VisitTry(TryExpression node) => base.VisitTry(node);

      protected override Expression VisitTypeBinary(TypeBinaryExpression node) => base.VisitTypeBinary(node);

      protected override Expression VisitUnary(UnaryExpression node) => base.VisitUnary(node);
   }
}
