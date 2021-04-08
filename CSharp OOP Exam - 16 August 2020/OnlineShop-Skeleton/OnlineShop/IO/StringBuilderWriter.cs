using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.IO
{
    public class StringBuilderWriter : IWriter
    {
        private StringBuilder sb;

        public StringBuilderWriter()
        {
            sb = new StringBuilder();
        }
        public void CustomWrite(string text)
        {
            sb.Append(text);
        }

        public void CustomWriteLine(string text)
        {
            sb.AppendLine(text);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
