using System;
using System.Runtime.Serialization;

namespace RookDilemma.Solution
{
    public class ProblemSolverException : Exception
    {
        public ProblemSolverException()
        {
        }

        public ProblemSolverException(string message) : base(message)
        {
        }

        public ProblemSolverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProblemSolverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}