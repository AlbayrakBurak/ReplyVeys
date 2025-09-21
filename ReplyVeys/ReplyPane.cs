using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReplyVeys
{
    public partial class ReplyPane : UserControl
    {
        public event EventHandler ApplyClicked;
        public event EventHandler SendClicked;

        public ReplyPane()
        {
            InitializeComponent();
            btnApply.Click += (s, e) => ApplyClicked?.Invoke(this, EventArgs.Empty);
            btnSend.Click += (s, e) => SendClicked?.Invoke(this, EventArgs.Empty);
            SetEnabled(false);
        }

        public string DraftText
        {
            get => txtDraft.Text;
            set => txtDraft.Text = value;
        }

        public void SetEnabled(bool enabled)
        {
            txtDraft.Enabled = enabled;
            btnApply.Enabled = enabled;
            btnSend.Enabled = enabled;
        }
    }
}
