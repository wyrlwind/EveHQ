using EveHQ.Core;
using EveHQ.NewEveApi;
using EveHQ.NewEveApi.Entities;
using System;
using System.Collections.Generic;
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
            int maxColonies = 1;
            int lyScanned = 0;
            int commandCenter = 1;

            listViewCharacterSkills.BeginUpdate();
            listViewCharacterSkills.Items.Clear();
            foreach (EveSkill skill in HQ.SkillListName.Values)
            {
                if (skill.GroupID == 1241)
                {
                    int skillLevel = SkillFunctions.CalcCurrentLevel(_pilot, skill);
                    ListViewItem item = new ListViewItem(new[] { skill.Name, skillLevel.ToString() });
                    listViewCharacterSkills.Items.Add(item);
                    if (skill.ID == 2495)
                    {
                        // Interplanetary Consolidation - number of planets
                        maxColonies += skillLevel;
                    }
                    if (skill.ID == 13279)
                    {
                        if (skillLevel != 0)
                        {
                            // Remote sensing - lightyears scanned 1,3,5,7,9
                            lyScanned += (skillLevel * 2) - 1;
                        }
                    }
                    if (skill.ID == 2505)
                    {
                        // Command center upgrades - command center level
                        commandCenter += skillLevel;
                    }
                }
            }
            listViewCharacterSkills.EndUpdate();

            listViewCapabilities.BeginUpdate();
            listViewCapabilities.Items.Clear();
            ListViewItem capability = new ListViewItem(new[] { "Max number of colonies", maxColonies.ToString() });
            listViewCapabilities.Items.Add(capability);
            capability = new ListViewItem(new[] { "Scanning distance (lightyears)", lyScanned.ToString() });
            listViewCapabilities.Items.Add(capability);
            capability = new ListViewItem(new[] { "Max command center level", commandCenter.ToString() });
            listViewCapabilities.Items.Add(capability);
            listViewCapabilities.EndUpdate();
        }

        private void InitCharacterColonies()
        {
            objectListViewColonies.BeginUpdate();
            objectListViewColonies.Items.Clear();
            objectListViewPins.Items.Clear();
            string accountName = _pilot.Account;
            if (HQ.Settings.Accounts.ContainsKey(accountName))
            {
                EveHQAccount pilotAccount;
                HQ.Settings.Accounts.TryGetValue(accountName, out pilotAccount);

                EveServiceResponse<IEnumerable<PlanetaryColony>> coloniesResponse =
                        HQ.ApiProvider.Character.PlanetaryColonies(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID));

                if (coloniesResponse.IsSuccess)
                {
                    List<Colony> colonies = new List<Colony>();
                    foreach (PlanetaryColony colony in coloniesResponse.ResultData)
                    {
                        Colony newPlanet = new Colony(colony);
                        colonies.Add(newPlanet);
                    }
                    objectListViewColonies.AddObjects(colonies);
                    objectListViewColonies.EndUpdate();
                    objectListViewPins.BeginUpdate();
                    foreach (Colony colony in colonies)
                    {
                        EveServiceResponse<IEnumerable<PlanetaryPin>> pinsResponse =
                                HQ.ApiProvider.Character.PlanetaryPins(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID), colony.PlanetID);

                        if (pinsResponse.IsSuccess)
                        {
                            foreach (PlanetaryPin pin in pinsResponse.ResultData)
                            {
                                colony.addInstallation(pin);
                            }
                            objectListViewPins.AddObjects(colony.Installations);
                            objectListViewPins.EndUpdate();
                        }

                        EveServiceResponse<IEnumerable<PlanetaryLink>> linksResponse =
                                HQ.ApiProvider.Character.PlanetaryLinks(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID), colony.PlanetID);

                        if (linksResponse.IsSuccess)
                        {
                            foreach (PlanetaryLink link in linksResponse.ResultData)
                            {
                                colony.addLink(link);
                            }
                        }

                        EveServiceResponse<IEnumerable<PlanetaryRoute>> routesResponse =
                                HQ.ApiProvider.Character.PlanetaryRoutes(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID), colony.PlanetID);

                        if (routesResponse.IsSuccess)
                        {
                            foreach (PlanetaryRoute route in routesResponse.ResultData)
                            {
                                colony.addRoute(route);
                            }
                        }
                    }
                }
            }
        }

    }
}
