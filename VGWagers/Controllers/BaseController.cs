using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VGWagers.Common;

namespace VGWagers.Controllers
{
    public class BaseController : Controller
    {
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable,AlertStyles.Glyphiconsuccess);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable, AlertStyles.Glyphiconinfo);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable, AlertStyles.Glyphiconwarning);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable,AlertStyles.Glyphiconerror);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable, string glyphiconsign)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                GlyphIconSign = glyphiconsign
            });

            TempData[Alert.TempDataKey] = alerts;
        }
    }
}