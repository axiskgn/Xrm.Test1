using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Xrm.Test1.WpfUi.Controls
{
    /// <summary>
    /// Interaction logic for ResumeDetailView.xaml
    /// </summary>
    public partial class ResumeDetailView : UserControl
    {
        public ResumeDetailView()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
