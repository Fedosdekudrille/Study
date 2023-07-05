using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form6 : Form
    {
        Form1 form;
        public Form6(Form1 form)
        {
            InitializeComponent();
            this.form = form;
            this.button5.Click += new System.EventHandler(form.button5_Click);
            this.button6.Click += new System.EventHandler(form.button6_Click);
        }
    }
}
