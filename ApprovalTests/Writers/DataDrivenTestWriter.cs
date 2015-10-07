using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ApprovalTests.Writers
{
    public class DataDrivenTestWriter : ApprovalTextWriter
    {
        public DataDrivenTestWriter(string data, TestContext context)
            : base(data)
        {
            this._context = context;
        }

        public override string GetReceivedFilename(string basename)
        {
            return String.Format("{0}[{1}].received{2}", basename, GetRowNumber(), ExtensionWithDot);
        }

        public override string GetApprovalFilename(string basename)
        {
            return String.Format("{0}[{1}].approved{2}", basename, GetRowNumber(), ExtensionWithDot);
        }

        #region Private Members

        private TestContext _context;

        private int GetRowNumber()
        {
            return _context.DataRow.Table.Rows.IndexOf(_context.DataRow);
        }

        #endregion Private Members
    }
}