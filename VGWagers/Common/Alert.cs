using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VGWagers.Common
{
    public class Alert
    {
        public const string TempDataKey = "TempDataAlerts";

        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
        public string GlyphIconSign { get; set; }
    }

    public static class AlertStyles
    {
        public const string Success = "success";
        public const string Information = "info";
        public const string Warning = "warning";
        public const string Danger = "danger";

        public const string Glyphiconwarning = "glyphicon-warning-sign";
        public const string Glyphiconinfo = "glyphicon-info-sign";
        public const string Glyphiconerror = "glyphicon-remove";
        public const string Glyphiconsuccess = "glyphicon-ok";
        
    }
}