using System.Web.Mvc;

namespace VGWagers.Models.JCrop
{
    /// <summary>
    /// Input model for JCrop Editor view.
    /// </summary>
    public class EditorInputModel
    {

        public ImageViewModel Profile { get; set; }
        [HiddenInput]
        public double Top { get; set; }
        [HiddenInput]
        public double Bottom { get; set; }
        [HiddenInput]
        public double Left { get; set; }
        [HiddenInput]
        public double Right { get; set; }
        [HiddenInput]
        public double Width { get; set; }
        [HiddenInput]
        public double Height { get; set; }
    }
}