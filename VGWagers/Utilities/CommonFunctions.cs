using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VGWagers.Utilities
{
    public static class CommonFunctions
    {
        public static bool IsImage(HttpPostedFile postedFile)
        {
            try  
            {
            using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {                        
                        return !bitmap.Size.IsEmpty;
                }
            }
            catch (Exception)
            {
                return false;
            }
                
        }
    }
}