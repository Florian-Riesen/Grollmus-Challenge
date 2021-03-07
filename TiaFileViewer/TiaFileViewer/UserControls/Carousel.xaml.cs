using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
