using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuerteControls.Test
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            var library = Assembly.Load("RuerteControls");
            var exportedTypes = library.GetExportedTypes();
            var controls = exportedTypes.Where(t => typeof(Control).IsAssignableFrom(t) && !typeof(Form).IsAssignableFrom(t) && !typeof(ToolStrip).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic && t.IsVisible && !t.IsGenericType);
            this.TypeComboBox.DataSource = controls.ToList();
            this.TypeComboBox.DisplayMember = "Name";
            this.TypeComboBox.SelectedIndex = 0;

            this.BackColor = StyleProvider.ActiveStyle.BackColor;
            StyleProvider.ActiveStyleChanged += (s, a) => this.BackColor = StyleProvider.ActiveStyle.BackColor;

            this.StyleComboBox.DataSource = new List<Style>() { Style.VisualStudioLightStyle, Style.VisualStudioDarkStyle };
            this.StyleComboBox.DisplayMember = "Name";
            this.StyleComboBox.SelectedIndex = 0;
        }
        
        private Control currentControl;
        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (this.currentControl != null) this.currentControl.Dispose();
                }
                catch
                {
                    //Ignore
                }

                var type = (Type)this.TypeComboBox.SelectedItem;
                this.splitContainer1.Panel1.Controls.Add(this.currentControl = (Control)Activator.CreateInstance(type));

                this.currentControl.Text = type.Name;
                this.currentControl.SizeChanged += (s, ea) => this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);
                this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);
                
                //To fix the issue with the button getting a white background color assigned randomly when being created.
                if (typeof(Button).IsAssignableFrom(type))
                {
                    (this.currentControl as Button).UseVisualStyleBackColor = true;
                }

                this.ControlPropertyGrid.SelectedObject = this.currentControl;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("An " + ex.GetType().Name + " occured: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StyleProvider.ActiveStyle = (Style)this.StyleComboBox.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StyledHeaderlessForm form = new StyledHeaderlessForm();
            form.ShadowProvider = new LayeredWindowGlowShadowProvider(form);
            form.Show/*Dialog*/();
            Application.DoEvents();
            return;
        }
    }
}
