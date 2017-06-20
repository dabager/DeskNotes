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
        private Page page;
        private bool initialized = false;

        public Note()
        {
            InitializeComponent();
            baseConstructor();
        }

        public Note(int pageNumber)
        {
            InitializeComponent();
            baseConstructor();
            page = Settings.Pages.Where(w => w.pageNumber == pageNumber).FirstOrDefault();
            txtTitle.Text = page.title;
            txtContent.Text = page.content;
            this.Top = page.location.Height;
            this.Left = page.location.Width;
            this.Width = page.size.Width;
            this.Height = page.size.Height;
            initialized = true;
        }

        private void baseConstructor()
        {
            thumb.DragDelta += Thumb_DragDelta;
            ContentCanvas.Children.Add(thumb);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
            if (initialized) page.location = new System.Drawing.Size((int)this.Left, (int)this.Top);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            thumb.RaiseEvent(e);
        }

        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized) page.title = txtTitle.Text;
        }

        private void txtContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized) page.content = txtContent.Text;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Storyboard)TryFindResource("Storyboard.FadeOut")).Begin();
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Storyboard)TryFindResource("Storyboard.FadeIn")).Begin();
        }
        
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Settings.Pages.Remove(page);
            this.Close();
            if(Settings.PageCount == 0)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Page p = new Page();
            p.backColor = "";
            p.titleColor = "";
            p.pageNumber = Settings.Pages.Max(m => m.pageNumber) + 1;
            p.title = "Notes - " + p.pageNumber;
            p.content = "";
            p.location = Settings.DefaultLocation;
            p.size = new System.Drawing.Size(300, 400);
            Settings.Pages.Add(p);
            Note note = new Note(p.pageNumber);
            note.Show();
        }
    }
}
