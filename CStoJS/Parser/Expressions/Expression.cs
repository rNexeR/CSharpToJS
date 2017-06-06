using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using CStoJS.Tree;
using System.Collections.Generic;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        ExpressionNode Expression()
        {
            printDebug("Expression");
            return ConditionalExpression();
            // return null;
        }

        private ExpressionNode ConditionalExpression()
        {
            printDebug("Conditional Expression");
            var first = NullCoalescingExpression();
            return ConditionalExpressionPrime(ref first);
        }

        private ExpressionNode ConditionalExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Conditional Expression Prime");
            if (ConsumeOnMatch(TokenType.OP_TERNARY))
            {
                var truth = Expression();
                MatchExactly(TokenType.OP_HIERARCHY);
                var lie = Expression();
                return new TernaryExpressionNode(left, truth, lie);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode NullCoalescingExpression()
        {
            printDebug("Null Coalescing Expression");
            var first = ConditionalOrExpression();
            return NullCoalescingExpressionPrime(ref first);
        }

        private ExpressionNode NullCoalescingExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Null Coalescing Expression Prime");
            if (Match(TokenType.OP_NULL_COALESCING))
            {
                var operador = ConsumeToken();
                var right = NullCoalescingExpression();
                return BinaryNodeDetector(left, operador, right);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode ConditionalOrExpression()
        {
            printDebug("Conditional Or Expression");
            var first = ConditionalAndExpression();
            return ConditionalOrExpressionPrime(ref first);
        }

        private ExpressionNode ConditionalOrExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Conditional Or ExpressionPrime");
            if (Match(TokenType.OP_CONDITIONAL_OR))
            {
                var operador = ConsumeToken();
                var right = ConditionalAndExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return ConditionalOrExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode ConditionalAndExpression()
        {
            printDebug("Conditional And Expression");
            var first = InclusiveOrExpression();
            return ConditionalAndExpressionPrime(ref first);
        }

        private ExpressionNode InclusiveOrExpression()
        {
            printDebug("Inclusive Or Expression");
            var first = ExclusiveOrExpression();
            return InclusiveOrExpressionPrime(ref first);
        }

        private ExpressionNode ConditionalAndExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Conditional And Expression Prime");
            if (Match(TokenType.OP_CONDITIONAL_AND))
            {
                var operador = ConsumeToken();
                var right = InclusiveOrExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return ConditionalAndExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode InclusiveOrExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Inclusive Or Expression Prime");
            if (Match(TokenType.OP_BITS_OR))
            {
                var operador = ConsumeToken();
                var right = ExclusiveOrExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return InclusiveOrExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode ExclusiveOrExpression()
        {
            printDebug("Exclusive Or Expression");
            var first = AndExpression();
            return ExclusiveOrExpressionPrime(ref first);
        }

        private ExpressionNode ExclusiveOrExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Exclusive Or Expression Prime");
            if (Match(TokenType.OP_BITS_XOR))
            {
                var operador = ConsumeToken();
                var right = AndExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return ExclusiveOrExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode AndExpression()
        {
            printDebug("And Expression");
            var first = EquialityExpression();
            return AndExpressionPrime(ref first);
        }

        private ExpressionNode AndExpressionPrime(ref ExpressionNode left)
        {
            printDebug("And Expression Prime");
            if (Match(TokenType.OP_BITS_AND))
            {
                var operador = ConsumeToken();
                var right = EquialityExpression();
                var first = new BitwiseExpressionNode(left, operador, right) as ExpressionNode;
                return AndExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode EquialityExpression()
        {
            printDebug("Equality Expression");
            var first = RelationalExpression();
            return EquialityExpressionPrime(ref first);
        }

        private ExpressionNode EquialityExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Equality Expression Prime");
            if (MatchAny(this.equality_operators))
            {
                var operador = ConsumeToken();
                var right = RelationalExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return EquialityExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode RelationalExpression()
        {
            printDebug("Relational Expression");
            var first = ShiftExpression();
            return RelationalExpressionPrime(ref first);
        }

        private ExpressionNode RelationalExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Relational Expression Prime");
            if (MatchAny(this.relational_operators))
            {
                var operador = ConsumeToken();
                var right = ShiftExpression();
                var first = new ConditionalRelationalExpressionNode(left, operador, right) as ExpressionNode;
                return RelationalExpressionPrime(ref first);
            }
            else if (Match(TokenType.IS_KEYWORD))
            {
                var operador = ConsumeToken();
                var type = Type();
                var first = new ConditionalIsExpressionNode(left, operador, type) as ExpressionNode;
                return RelationalExpressionPrime(ref first);
            }
            else if (Match(TokenType.AS_KEYWORD))
            {
                var operador = ConsumeToken();
                var type = Type();
                var new_left = new List<ExpressionNode>();
                new_left.Add(left);
                var first = new CastingExpressionNode(type, new_left) as ExpressionNode;
                return RelationalExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode ShiftExpression()
        {
            printDebug("Shift Expression");
            var first = AdditiveExpression();
            return ShiftExpressionPrime(ref first);
        }

        private ExpressionNode ShiftExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Shift Expression Prime");
            if (MatchAny(this.shift_operators))
            {
                var operador = ConsumeToken();
                var right = AdditiveExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return ShiftExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode AdditiveExpression()
        {
            printDebug("Additive Expression");
            var first = MultiplicativeExpression();
            return AdditiveExpressionPrime(ref first);
        }

        private ExpressionNode AdditiveExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Additive Expression Prime");
            if (MatchAny(this.additive_operators))
            {
                var operador = ConsumeToken();
                var right = MultiplicativeExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return AdditiveExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        private ExpressionNode MultiplicativeExpression()
        {
            printDebug("Multiplicative Expression");
            var first = UnaryExpression();
            return MultiplicativeExpressionFactorized(ref first);
        }

        private ExpressionNode MultiplicativeExpressionFactorized(ref ExpressionNode left)
        {
            printDebug("Multiplicative Expression Factorized");
            if (MatchAny(this.assignment_operators))
            {
                var operador = ConsumeToken();
                var right = Expression();
                var first = new AssignationExpressionNode(left, operador, right) as ExpressionNode;
                return MultiplicativeExpressionPrime(ref first);
            }
            else
            {
                return MultiplicativeExpressionPrime(ref left);
            }
        }

        private ExpressionNode MultiplicativeExpressionPrime(ref ExpressionNode left)
        {
            printDebug("Multiplicative Expression Prime");
            if (MatchAny(this.multiplicative_operators))
            {
                var operador = ConsumeToken();
                var right = UnaryExpression();
                var first = BinaryNodeDetector(left, operador, right) as ExpressionNode;
                return MultiplicativeExpressionPrime(ref first);
            }
            else
            {
                return left;
            }
        }

        ExpressionNode OptionalExpression()
        {
            if (MatchAny(this.expression_operators))
            {
                return Expression();
            }
            else
            {
                return null;
            }
        }
        ExpressionNode Literals()
        {
            printDebug("Literlas");
            if (MatchAny(this.literals))
            {
                var literal = ConsumeToken();
                return new LiteralExpressionNode(literal);
            }
            else
            {
                ThrowSyntaxException("Literal Expected");
                return null;
            }
        }
    }
}