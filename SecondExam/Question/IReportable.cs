namespace SecondExam.Question
{
    /// <summary>
    /// Interface for generating summary reports of transactions
    /// </summary>
    public interface IReportable
    {
        /// <summary>
        /// Returns a formatted summary of the transaction
        /// </summary>
        /// <returns>String containing transaction summary</returns>
        string GetSummary();
    }
}