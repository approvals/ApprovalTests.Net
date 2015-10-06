using ApprovalTests;
using ApprovalTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ackara
{
    public class DDTWriter : ApprovalTextWriter
    {
        public DDTWriter(string data, TestContext context)
            : base(data)
        {
            _context = context;
        }

        public override string GetReceivedFilename(string basename)
        {
            return String.Format("{0}.received[{1}]{2}", basename, GetRowNumber(), ExtensionWithDot);
        }

        public override string GetApprovalFilename(string basename)
        {
            return String.Format("{0}.approved[{1}]{2}", basename,  GetRowNumber(), ExtensionWithDot);
        }

        private TestContext _context;

        public int GetRowNumber()
        {
            return _context.DataRow.Table.Rows.IndexOf(_context.DataRow);
        }
    }
}
