using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LemonEngine.RenderLogic
{
    public partial class RenderTools : Form
    {
        public RenderTools()
        {
            InitializeComponent();
        }

        public EventHandler<ToggleEventArgs> LightChanged;
        public EventHandler<ToggleEventArgs> SmoothChanged;
        public EventHandler<ToggleEventArgs> ColorMaterialChanged;
        public EventHandler<ToggleEventArgs> TexturesChanged;

        private void checkBoxLight_CheckedChanged(object sender, EventArgs e)
        {
            LightChanged?.Invoke(this, new ToggleEventArgs(checkBoxLight.Checked));
        }

        private void checkBoxSmooth_CheckedChanged(object sender, EventArgs e)
        {
            SmoothChanged?.Invoke(this, new ToggleEventArgs(checkBoxSmooth.Checked));
        }

        private void checkBoxColorMaterial_CheckedChanged(object sender, EventArgs e)
        {
            ColorMaterialChanged?.Invoke(this, new ToggleEventArgs(checkBoxColorMaterial.Checked));
        }

        private void checkBoxTextures_CheckedChanged(object sender, EventArgs e)
        {
            TexturesChanged?.Invoke(this, new ToggleEventArgs(checkBoxTextures.Checked));
        }
    }

    public class ToggleEventArgs : EventArgs
    {
        public ToggleEventArgs(bool state)
        {
            State = state;
        }
        public bool State { get; }
    }
}
