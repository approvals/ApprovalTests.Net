
namespace ApprovalUtilities.SimpleLogger.Writers
{
    public class MultiWriter : IAppendable
    {
        private readonly IAppendable[] writers;

        public MultiWriter(params IAppendable[] writers)
        {
            this.writers = writers;
        }

        public void AppendLine(string text)
        {
            foreach (var writer in writers)
            {
                writer.AppendLine(text);
            }
        }
    }
}