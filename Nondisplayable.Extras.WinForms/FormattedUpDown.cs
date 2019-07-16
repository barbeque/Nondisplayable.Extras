using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nondisplayable.Extras.WinForms
{
    /// <summary>
    /// A variant of <see cref="NumericUpDown"/> that has a format string for display.
    /// </summary>
    public class FormattedUpDown : NumericUpDown
    {
        public FormattedUpDown()
        {
            Format = @"{0}";
        }

        protected override void UpdateEditText()
        {
            if (string.IsNullOrWhiteSpace(Format))
            {
                base.UpdateEditText();
            }
            else
            {
                base.Text = string.Format(Format, (int)Value);
            }
        }

        [Description("The format string for the display of the value")]
        public string Format
        {
            get; set;
        }
    }
}
