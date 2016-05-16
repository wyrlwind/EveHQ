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
        private Dictionary<long, Colony> _colonies;
        private List<Colony> _coloniesSelected;

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
            listViewCharacterSkills.Items.Clear();
            listViewCapabilities.Items.Clear();
            objectListViewColonies.Items.Clear();
            objectListViewPins.Items.Clear();
            objectListViewLinks.Items.Clear();
            objectListViewRoutes.Items.Clear();
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
            if (_colonies != null)
            {
                _colonies.Clear();
            }
            if (_coloniesSelected != null)
            {
                _coloniesSelected.Clear();
            }
            objectListViewColonies.BeginUpdate();
            objectListViewColonies.Items.Clear();
            objectListViewPins.Items.Clear();
            objectListViewLinks.Items.Clear();
            objectListViewRoutes.Items.Clear();
            string accountName = _pilot.Account;
            if (HQ.Settings.Accounts.ContainsKey(accountName))
            {
                EveHQAccount pilotAccount;
                HQ.Settings.Accounts.TryGetValue(accountName, out pilotAccount);

                EveServiceResponse<IEnumerable<PlanetaryColony>> coloniesResponse =
                        HQ.ApiProvider.Character.PlanetaryColonies(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID));

                if (coloniesResponse.IsSuccess)
                {
                    Dictionary<long, Colony> colonies = new Dictionary<long, Colony>();
                    foreach (PlanetaryColony colony in coloniesResponse.ResultData)
                    {
                        Colony newPlanet = new Colony(colony);
                        colonies[newPlanet.PlanetID] = newPlanet;
                    }
                    _colonies = colonies;
                    foreach (KeyValuePair<long, Colony> colony in colonies)
                    {
                        EveServiceResponse<IEnumerable<PlanetaryPin>> pinsResponse =
                                HQ.ApiProvider.Character.PlanetaryPins(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID), colony.Value.PlanetID);

                        if (pinsResponse.IsSuccess)
                        {
                            foreach (PlanetaryPin pin in pinsResponse.ResultData)
                            {
                                colony.Value.addInstallation(pin);
                            }
                        }

                        EveServiceResponse<IEnumerable<PlanetaryLink>> linksResponse =
                                HQ.ApiProvider.Character.PlanetaryLinks(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID), colony.Value.PlanetID);

                        if (linksResponse.IsSuccess)
                        {
                            foreach (PlanetaryLink link in linksResponse.ResultData)
                            {
                                colony.Value.addLink(link);
                            }
                        }

                        EveServiceResponse<IEnumerable<PlanetaryRoute>> routesResponse =
                                HQ.ApiProvider.Character.PlanetaryRoutes(pilotAccount.UserID, pilotAccount.APIKey, Convert.ToInt32(_pilot.ID), colony.Value.PlanetID);

                        if (routesResponse.IsSuccess)
                        {
                            foreach (PlanetaryRoute route in routesResponse.ResultData)
                            {
                                colony.Value.addRoute(route);
                            }
                        }
                    }
                    if (_coloniesSelected == null)
                    {
                        _coloniesSelected = new List<Colony>();
                    }
                    foreach (KeyValuePair<long, Colony> colony in _colonies)
                    {
                        _coloniesSelected.Add(colony.Value);
                    }
                    objectListViewColonies.AddObjects(_coloniesSelected);
                    objectListViewColonies.EndUpdate();
                    showInstallations(_coloniesSelected);
                    showLinks(_coloniesSelected);
                    showRoutes(_coloniesSelected);
                }
            }
        }

        private void objectListViewColonies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_coloniesSelected != null)
            {
                _coloniesSelected.Clear();
            }
            foreach (var selectedObject in objectListViewColonies.SelectedObjects)
            {
                if (_coloniesSelected == null)
                {
                    _coloniesSelected = new List<Colony>();
                }
                _coloniesSelected.Add((Colony)selectedObject);
            }
            if (_coloniesSelected.Count == 0)
            {
                foreach (KeyValuePair<long, Colony> colony in _colonies)
                {
                    _coloniesSelected.Add(colony.Value);
                }
            }
            showInstallations(_coloniesSelected);
            showLinks(_coloniesSelected);
            showRoutes(_coloniesSelected);
        }

        private void showInstallations(List<Colony> colonies)
        {
            objectListViewPins.Items.Clear();
            objectListViewPins.BeginUpdate();
            foreach (Colony colony in colonies)
            {
                objectListViewPins.AddObjects(colony.Installations);
            }
            objectListViewPins.EndUpdate();
        }

        private void showLinks(List<Colony> colonies)
        {
            objectListViewLinks.Items.Clear();
            objectListViewLinks.BeginUpdate();
            foreach (Colony colony in colonies)
            {
                objectListViewLinks.AddObjects(colony.Links);
            }
            objectListViewLinks.EndUpdate();
        }

        private void showRoutes(List<Colony> colonies)
        {
            objectListViewRoutes.Items.Clear();
            objectListViewRoutes.BeginUpdate();
            foreach (Colony colony in colonies)
            {
                objectListViewRoutes.AddObjects(colony.Routes);
            }
            objectListViewRoutes.EndUpdate();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if ((_coloniesSelected != null) && (_coloniesSelected.Count != 0) && objectListViewColonies.Visible)
            {
                showInstallations(_coloniesSelected);
            }
        }
    }
}
