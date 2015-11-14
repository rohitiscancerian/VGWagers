using System.ComponentModel.DataAnnotations;

namespace VGWagers.Models.JCrop
{
    /// <summary>
    /// ViewModel for jCrop Profile view.
    /// </summary>
    public class ImageViewModel
    {
        [UIHint("ProfileImage")]
        public string ImageUrl { get; set; }
    }
}