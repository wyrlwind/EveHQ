using EveHQ.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveHQ.PlanetaryInteraction
{
    public class PlugInData : IEveHQPlugIn
    {
        #region "Plug-in Interface Properties and Functions"

        EveHQPlugIn IEveHQPlugIn.GetEveHQPlugInInfo()
        {
            EveHQPlugIn eveHQPlugIn = new EveHQPlugIn();
            eveHQPlugIn.Name = "EveHQ Planetary Colonies";
            eveHQPlugIn.Description = "Monitor planetary colonies";
            eveHQPlugIn.Author = "EveHQ Team";
            eveHQPlugIn.MainMenuText = "EveHQ Planetary Colonies";
            eveHQPlugIn.RunAtStartup = true;
            eveHQPlugIn.RunInIGB = true;
            eveHQPlugIn.MenuImage = Properties.Resources.NeoComPlanets;
            eveHQPlugIn.Version = Application.ProductVersion.ToString();
            return eveHQPlugIn;
        }

        Boolean IEveHQPlugIn.EveHQStartUp()
        {
            return true;
        }

        String IEveHQPlugIn.IGBService(HttpListenerContext igbContext)
        {
            return "";
        }

        Form IEveHQPlugIn.RunEveHQPlugIn()
        {
            return new FrmPI();
        }

        Object IEveHQPlugIn.GetPlugInData(Object data, int dataType)
        {
            return null;
        }

        Boolean IEveHQPlugIn.SaveAll()
        {
            return false;
        }

        #endregion

    }
}
