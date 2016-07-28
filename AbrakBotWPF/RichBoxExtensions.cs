using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AbrakBotWPF
{
    static class RichBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, string color)
        {
            System.Windows.Media.BrushConverter bc = new System.Windows.Media.BrushConverter();
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            tr.Text = text;
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString(color));
        }
    }
}
