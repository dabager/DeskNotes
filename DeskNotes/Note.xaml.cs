using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeskNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Note : Window
    {
        Thumb thumb = new Thumb {Width = 0, Height = 0};

        public Note()
        {
            InitializeComponent();
            thumb.DragDelta += Thumb_DragDelta;
            ContentCanvas.Children.Add(thumb);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            thumb.RaiseEvent(e);
        }

        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Storyboard)TryFindResource("Storyboard.FadeOut")).Begin();
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Storyboard)TryFindResource("Storyboard.FadeIn")).Begin();
        }
    }
}
