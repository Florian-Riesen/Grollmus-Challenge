using System.Windows;
using System.Windows.Controls;

namespace TiaFileViewer.UserControls
{
    /// <summary>
    /// Interaction logic for Carousel.xaml
    /// </summary>
    public partial class Carousel : UserControl
    {
        public Carousel()
        {
            InitializeComponent();
        }

        private void ScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            NodeScrollViewer.LineLeft();
            NodeScrollViewer.LineLeft();
            NodeScrollViewer.LineLeft();
        }
        private void ScrollRight_Click(object sender, RoutedEventArgs e)
        {
            NodeScrollViewer.LineRight();
            NodeScrollViewer.LineRight();
            NodeScrollViewer.LineRight();
        }
    }
}
