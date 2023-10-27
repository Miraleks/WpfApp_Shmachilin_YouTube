using System.Collections.Generic;
using System.Drawing;

namespace WpfApp_Shmachilin_YouTube.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }
        public Point Location { get; set; }

        public IEnumerable<ConfirmedCount> Counts { get; set; }


    }



}
