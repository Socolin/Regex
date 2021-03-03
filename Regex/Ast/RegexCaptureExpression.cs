using System;
using System.Text;

namespace RE
{
    public class RegexCaptureExpression : RegexExpression, IEquatable<RegexCaptureExpression>
    {
        private RegexExpression Expression { get; set; }
        private CaptureGroupInfo CaptureGroupInfo { get; set; }
        public override bool IsSingleElement => false;

        public RegexCaptureExpression(RegexExpression expression, CaptureGroupInfo captureGroupInfo)
        {
            Expression = expression;
            CaptureGroupInfo = captureGroupInfo;
        }

        protected override RegexExpression CloneImpl()
        {
            return new RegexCaptureExpression(Expression, CaptureGroupInfo.Clone());
        }

        public override CharFA<TAccept> ToFA<TAccept>(TAccept accept)
        {
            return CharFA<TAccept>.Capture(Expression.ToFA(accept), CaptureGroupInfo, accept);
        }

        protected internal override void AppendTo(StringBuilder sb)
        {
            sb.Append('(');
            if (CaptureGroupInfo.GroupNumber == 0)
                sb.Append("?:");
            else if (CaptureGroupInfo.CaptureName != null)
                sb.Append("?<").Append(CaptureGroupInfo.CaptureName).Append('>');
            sb.Append(')');
        }

        public bool Equals(RegexCaptureExpression other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(Expression, other.Expression) && Equals(CaptureGroupInfo, other.CaptureGroupInfo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((RegexCaptureExpression) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Expression != null ? Expression.GetHashCode() : 0) * 397) ^ (CaptureGroupInfo != null ? CaptureGroupInfo.GetHashCode() : 0);
            }
        }
    }
}