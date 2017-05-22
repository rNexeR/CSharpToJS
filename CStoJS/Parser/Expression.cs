using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        void Expression(){
            printDebug("Expression");
            ConditionalExpression();
        }

        private void ConditionalExpression()
        {
            printDebug("Conditional Expression");
            NullCoalescingExpression();
            ConditionalExpressionPrime();
        }

        private void ConditionalExpressionPrime()
        {
            printDebug("Conditional Expression Prime");
            if(ConsumeOnMatch(TokenType.OP_TERNARY)){
                Expression();
                MatchExactly(TokenType.OP_HIERARCHY);
                Expression();
            }else{
                //EPSILON
            }
        }

        private void NullCoalescingExpression()
        {
            printDebug("Null Coalescing Expression");
            ConditionalOrExpression();
            NullCoalescingExpressionPrime();
        }

        private void NullCoalescingExpressionPrime()
        {
            printDebug("Null Coalescing Expression Prime");
            if(ConsumeOnMatch(TokenType.OP_NULL_COALESCING)){
                NullCoalescingExpression();
            }else{
                //EPSILON
            }
        }

        private void ConditionalOrExpression()
        {
            printDebug("Conditional Or Expression");
            ConditionalAndExpression();
            ConditionalOrExpressionPrime();
        }

        private void ConditionalOrExpressionPrime()
        {
            printDebug("Conditional Or ExpressionPrime");
            if(ConsumeOnMatch(TokenType.OP_CONDITIONAL_OR)){
                ConditionalAndExpression();
                ConditionalOrExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void ConditionalAndExpression()
        {
            printDebug("Conditional And Expression");
            InclusiveOrExpression();
            ConditionalAndExpressionPrime();
        }

        private void InclusiveOrExpression()
        {
            printDebug("Inclusive Or Expression");
            ExclusiveOrExpression();
            InclusiveOrExpressionPrime();
        }

        private void ConditionalAndExpressionPrime()
        {
            printDebug("Conditional And Expression Prime");
            if(ConsumeOnMatch(TokenType.OP_CONDITIONAL_AND)){
                InclusiveOrExpression();
                ConditionalAndExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void InclusiveOrExpressionPrime()
        {
            printDebug("Inclusive Or Expression Prime");
            if(ConsumeOnMatch(TokenType.OP_BITS_OR)){
                ExclusiveOrExpression();
                InclusiveOrExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void ExclusiveOrExpression()
        {
            printDebug("Exclusive Or Expression");
            AndExpression();
            ExclusiveOrExpressionPrime();
        }

        private void ExclusiveOrExpressionPrime()
        {
            printDebug("Exclusive Or Expression Prime");
            if(ConsumeOnMatch(TokenType.OP_BITS_XOR)){
                AndExpression();
                ExclusiveOrExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void AndExpression()
        {
            printDebug("And Expression");
            EquialityExpression();
            AndExpressionPrime();
        }

        private void AndExpressionPrime()
        {
            printDebug("And Expression Prime");
            if(ConsumeOnMatch(TokenType.OP_BITS_AND)){
                EquialityExpression();
                AndExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void EquialityExpression()
        {
            printDebug("Equality Expression");
            RelationalExpression();
            EquialityExpressionPrime();
        }

        private void EquialityExpressionPrime()
        {
            printDebug("Equality Expression Prime");
            if( MatchAndComsumeAny(this.equality_operators) ){
                RelationalExpression();
                EquialityExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void RelationalExpression()
        {
            printDebug("Relational Expression");
            ShiftExpression();
            RelationalExpressionPrime();
        }

        private void RelationalExpressionPrime()
        {
            printDebug("Relational Expression Prime");
            if( MatchAndComsumeAny(this.relational_operators) ){
                ShiftExpression();
                RelationalExpressionPrime();
            }else if( MatchAndComsumeAny(this.is_as_operators) ){
                Type();
                RelationalExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void ShiftExpression()
        {
            printDebug("Shift Expression");
            AdditiveExpression();
            ShiftExpressionPrime();
        }

        private void ShiftExpressionPrime()
        {
            printDebug("Shift Expression Prime");
            if( MatchAndComsumeAny(this.shift_operators) ){
                AdditiveExpression();
                ShiftExpressionPrime();
            }else{
                //EPSILON
            }
        }

        private void AdditiveExpression()
        {
            printDebug("Additive Expression");
            MultiplicativeExpression();
            AdditiveExpressionPrime();
        }

        private void AdditiveExpressionPrime()
        {
            printDebug("Additive Expression Prime");
            if( MatchAndComsumeAny(this.additive_operators) ){
                MultiplicativeExpression();
                AdditiveExpressionPrime();
            }else{
                //epsilon
            }
        }

        private void MultiplicativeExpression()
        {
            printDebug("Multiplicative Expression");
            UnaryExpression();
            MultiplicativeExpressionFactorized();
        }

        private void MultiplicativeExpressionFactorized()
        {
            printDebug("Multiplicative Expression Factorized");
            if( MatchAndComsumeAny(this.assignment_operators) ){
                Expression();
                MultiplicativeExpressionPrime();
            }else{
                MultiplicativeExpressionPrime();
            }
        }

        private void MultiplicativeExpressionPrime()
        {
            printDebug("Multiplicative Expression Prime");
             if( MatchAndComsumeAny(this.multiplicative_operators) ){
                UnaryExpression();
                MultiplicativeExpressionPrime();
            }else{
                //epsilon
            }
        }

        void OptionalExpression(){
            if( MatchAny(this.expression_operators) ){
                Expression();
            }else{
                //epsilon
            }
        }
        void Literals(){
            printDebug("Literlas");
            if( MatchAny(this.literals) ){
                ConsumeToken();
            }else{
                ThrowSyntaxException("Literal Expected");
            }
        }
    }
}