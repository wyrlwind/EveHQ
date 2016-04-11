using EveHQ.Core;
using EveHQ.NewEveApi;
using EveHQ.NewEveApi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveHQ.PlanetaryInteraction
{
    public partial class FrmPI : Form
    {
        EveHQPilot _pilot = null;

        public FrmPI()
        {
            InitializeComponent();
        }

        private void FrmPI_Load(object sender, EventArgs e)
        {
            InitCharacterList();
        }

        private void InitCharacterList()
        {
            listViewCharacterSelector.BeginUpdate();
            listViewCharacterSelector.Items.Clear();
            foreach (EveHQAccount account in HQ.Settings.Accounts.Values)
            {
                if (account.APIKeyType != APIKeyTypes.Corporation)
                {
                    foreach (string character in account.Characters)
                    {
                        listViewCharacterSelector.Items.Add(character);
                    }
                }
            }
            listViewCharacterSelector.Sort();
            listViewCharacterSelector.EndUpdate();
        }

        private void listViewCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pilot = null;
            if (listViewCharacterSelector.SelectedItems.Count != 0)
            {
                foreach (EveHQAccount account in HQ.Settings.Accounts.Values)
                {
                    foreach (string character in account.Characters)
                    {
                        if (character == listViewCharacterSelector.SelectedItems[0].Text)
                        {
                            HQ.Settings.Pilots.TryGetValue(character, out _pilot);
                            break;
                        }
                    }
                    if (_pilot != null)
                    {
                        break;
                    }
                }
            }
            if (_pilot != null)
            {
                InitCharacterPicture();
                InitCharacterSkills();
                InitCharacterColonies();
            }
        }

        private void InitCharacterPicture()
        {
            pictureBoxCharacterImage.Image = ImageHandler.GetPortraitImage(_pilot.ID);
        }

        private void InitCharacterSkills()
        {
            listViewCharacterSkills.BeginUpdate();
            listViewCharacterSkills.Items.Clear();
            foreach (EveSkill skill in HQ.SkillListName.Values)
            {
                if (skill.GroupID == 1241)
                {
                    int skillLevel = SkillFunctions.CalcCurrentLevel(_pilot, skill);
                    ListViewItem item = new ListViewItem(new[] { skill.Name, skillLevel.ToString() });
                    listViewCharacterSkills.Items.Add(item);
                }
            }
            listViewCharacterSkills.EndUpdate();
        }

        private void InitCharacterColonies()
        {
            string accountName = _pilot.Account;
            if (HQ.Settings.Accounts.ContainsKey(accountName))
            {
                EveHQAccount pilotAccount;
                HQ.Settings.Accounts.TryGetValue(accountName, out pilotAccount);

                EveServiceResponse<IEnumerable<PlanetaryColonies>> jobsResponse =
                        HQ.ApiProvider.Character.PlanetaryColonies(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID));

                if (jobsResponse.IsSuccess)
                {
                    List<PlanetaryColonies> colonyList = new List<PlanetaryColonies>();
                    foreach (PlanetaryColonies colony in jobsResponse.ResultData)
                    {
                        PlanetaryColonies newColony = new PlanetaryColonies();
                        newColony.SolarSystemID = colony.SolarSystemID;
                        newColony.SolarSystemName = colony.SolarSystemName;
                        newColony.PlanetID = colony.PlanetID;
                        newColony.PlanetName = colony.PlanetName;
                        newColony.PlanetTypeID = colony.PlanetTypeID;
                        newColony.PlanetTypeName = colony.PlanetTypeName;
                        newColony.OwnerID = colony.OwnerID;
                        newColony.OwnerName = colony.OwnerName;
                        newColony.LastUpdate = colony.LastUpdate;
                        newColony.UpgradeLevel = colony.UpgradeLevel;
                        newColony.NumberOfPins = colony.NumberOfPins;
                        colonyList.Add(newColony);
                    }
                    var _bind = colonyList.Select(a => new
                    {
                        ColumnSolarSystemID = a.SolarSystemID,
                        ColumnSolarSystemName = a.SolarSystemName,
                        ColumnPlanetID = a.PlanetID,
                        ColumnPlanetName = a.PlanetName,
                        ColumnPlanetTypeID = a.PlanetTypeID,
                        ColumnPlanetTypeName = a.PlanetTypeName,
                        ColumnOwnerID = a.OwnerID,
                        ColumnOwnerName = a.OwnerName,
                        ColumnLastUpdate = a.LastUpdate,
                        ColumnUpgradeLevel = a.UpgradeLevel,
                        ColumnNumberOfPins = a.NumberOfPins
                    });
                    dataGridViewPlanetaryColonies.DataSource = _bind;
                }
            }
        }

    }
}
