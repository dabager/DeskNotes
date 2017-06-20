using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DeskNotes
{
    public static class Settings
    {
        public static List<Page> Pages { get; set; }

        public static int PageCount
        {
            get
            {
                return (Pages ?? new List<Page>()).Count;
            }
        }

        public static Size DefaultLocation
        {
            get
            {
                Rectangle resolution = Screen.PrimaryScreen.Bounds;
                return new Size((resolution.Width - 300)/2, (resolution.Height- 400) / 2);
            }
        }
    }

    public class Page
    {
        public int pageNumber;
        public string title;
        public string content;
        public string titleColor;
        public string backColor;
        public Size location;
        public Size size;
    }
}
