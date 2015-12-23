using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using EveHQSharp.SDEConverter;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace EveHQSharp.SDEConverter
{
    public partial class SDEConverterWindow : Form
    {
        /// <summary>
        /// Name of the cache folder
        /// </summary>
        private const String cacheFolderName = "StaticData";

        static String coreCacheFolder;

        // key = iconId, Value = iconFile
        static SortedList<int, String> yamlIcons;
        // Key = typeId
        static SortedList<int, YAMLType> yamlTypes;
        // key = certId
        static SortedList<int, YAMLCert> yamlCerts;

        public SDEConverterWindow()
        {
            InitializeComponent();
        }

        private void btnGenerateCache_Click(object sender, EventArgs e)
        {
            //Check for existence of a core cache folder in the application directory
            coreCacheFolder = Path.Combine(Application.StartupPath, cacheFolderName);

            if (!Directory.Exists(coreCacheFolder))
            {
                //Create the cache folder if it doesn't exist
                Directory.CreateDirectory(coreCacheFolder);
            }

            // Parse the YAML files
            parseYamlFiles();

            // Create and write core cache data
            //loadAllData();
            //createCoreCache();

            // Create and write HQF cache data
            //generateHQFCacheData();

            MessageBox.Show("Creation of cache data complete!");

        }

        #region "YAML Parsing Routines"

        /// <summary>
        /// Parse all required yamlFiles
        /// </summary>
        private void parseYamlFiles()
        {
            yamlIcons = new SortedList<int, string>();
            yamlTypes = new SortedList<int, YAMLType>();
            yamlCerts = new SortedList<int, YAMLCert>();

            parseIconYAMLFile();
            //parseTypesYAMLFile();
            parseCertsYAMLFile();
        }

        /// <summary>
        /// Parse the iconIDs Yaml File,
        /// Fill the yamlIcons attribute
        /// </summary>
        private void parseIconYAMLFile()
        {
            MemoryStream stream = new MemoryStream(EveHQSharp.SDEConverter.Properties.Resources.iconIDs);
            StreamReader reader = new StreamReader(stream);
            YamlStream yaml = new YamlStream();

            yaml.Load(reader);

            if (yaml.Documents.Any())
            {
                // Should be only one document, so go with the first
                YamlDocument yamlDoc = yaml.Documents[0];

                // Cycle through the keys, which will be the typeIDs
                YamlMappingNode root = (YamlMappingNode)yamlDoc.RootNode;

                foreach (KeyValuePair<YamlNode, YamlNode> entry in root.Children)
                {
                    // Parse the typeID
                    int iconID = Int32.Parse(((YamlScalarNode)entry.Key).Value);
                    // Parse anything else underneath
                    foreach (KeyValuePair<YamlNode, YamlNode> subEntry in ((YamlMappingNode)entry.Value))
                    {
                        // Get the key and value of the sub entry
                        String keyName = ((YamlScalarNode)subEntry.Key).Value;

                        // Do something based on the key
                        switch (keyName)
                        {
                            case "iconFile":
                                // Pre-process the icon name to make it easier later on
                                String iconName = ((YamlScalarNode)subEntry.Value).Value.Trim();
                                // Get the filename if the fullname start with "res:"
                                if (iconName.StartsWith("res", StringComparison.Ordinal))
                                {
                                    iconName = iconName.Split("/".ToCharArray()).Last();
                                }

                                // Set the icon item
                                yamlIcons.Add(iconID, iconName);

                                break;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse the certificates Yaml File,
        /// Fill the yamlCerts attribute
        /// </summary>
        private void parseCertsYAMLFile()
        {
            MemoryStream stream = new MemoryStream(EveHQSharp.SDEConverter.Properties.Resources.certificates);
            StreamReader reader = new StreamReader(stream);
            YamlStream yaml = new YamlStream();

            yaml.Load(reader);

            if (yaml.Documents.Any())
            {
                // Should be only one document, so go with the first
                YamlDocument yamlDoc = yaml.Documents[0];

                // Cycle through the keys, which will be the certIDs
                YamlMappingNode root = (YamlMappingNode)yamlDoc.RootNode;

                foreach (KeyValuePair<YamlNode, YamlNode> entry in root.Children)
                {
                    int certId = Int32.Parse(((YamlScalarNode)entry.Key).Value);
                    YAMLCert cert = new YAMLCert();

                    cert.recommandedFor = new List<int>();
                    cert.certId = certId;

                    YamlMappingNode children = entry.Value as YamlMappingNode;

                    if(children != null)
                    {
                        // Parse anything else underneath
                        foreach (KeyValuePair<YamlNode, YamlNode> subEntry in ((YamlMappingNode)entry.Value))
                        {
                            // Get the key of the sub entry
                            String keyName = ((YamlScalarNode)subEntry.Key).Value;

                            // Do something based on the key
                            switch (keyName)
                            {
                                case "description" :
                                    // Set the description
                                    cert.description = ((YamlScalarNode)subEntry.Value).Value;
                                    break;
                                case "groupID" :
                                    // Set the groupID
                                    cert.groupId = Int32.Parse(((YamlScalarNode)subEntry.Value).Value);
                                    break;
                                case "name" :
                                    // Set the name
                                    cert.name = ((YamlScalarNode)subEntry.Value).Value;
                                    break;
                                case "recommendedFor" :
                                    // Set the type recommendations
                                    cert.recommandedFor = ((YamlSequenceNode)subEntry.Value).Children.Select(
                                        e => Convert.ToInt32(((YamlScalarNode)e).Value)
                                    );
                                    break;
                                case "skillTypes" :
                                    // Set the required skills
                                    YamlMappingNode skillMaps = (YamlMappingNode)subEntry.Value;
                                    List<CertReqSkill> reqSkills = new List<CertReqSkill>();

                                    if (skillMaps != null)
                                    {
                                        foreach (KeyValuePair<YamlNode, YamlNode> skillMap in ((YamlMappingNode)skillMaps).Children)
                                        {
                                            CertReqSkill reqSkill = new CertReqSkill();

                                            reqSkill.skillId = Int32.Parse(((YamlScalarNode)skillMap.Key).Value);
                                            reqSkill.skillLevels = new Dictionary<String, int>();

                                            foreach (KeyValuePair<YamlNode, YamlNode> level in ((YamlMappingNode)skillMap.Value).Children)
                                            {
                                                reqSkill.skillLevels.Add(
                                                    ((YamlScalarNode)level.Key).Value, 
                                                    Int32.Parse(((YamlScalarNode)level.Value).Value)
                                                );
                                            }
                                            reqSkills.Add(reqSkill);
                                        }
                                    }

                                    cert.requiredSkills = reqSkills;
                                    break;
                            }
                        }

                        yamlCerts.Add(cert.certId, cert);
                    }
                }
            }
        }
        
        #endregion

        #region "MSSQL Data Conversion Routines"
        private void btnCheckDB_Click(object sender, EventArgs e)
        {
            if (DatabaseAdapter.checkCustomDatabaseConnection())
            {
                alterDatabase();
            }

        }

        /// <summary>
        /// Check the SQL database
        /// </summary>
        private void alterDatabase()
        {
            DataSet evehqData = DatabaseAdapter.getStaticData("SELECT attributeGroup FROM dgmAttributeTypes");

            if (evehqData == null)
            {
                // We seem to be missing the data so lets add it in! 
                // Todo: WTF IS THIS, Try to understand once translating is done

                SQLiteConnection connection = new SQLiteConnection(
                    DatabaseAdapter.getSqlLiteConnectionString()
                );

                try
                {

                    connection.Open();
                    addSqlAttributeGroupColumn(connection);
                    correctSqlEveUnits(connection);

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(
                        exception.Message,
                        "An error occured during the SQL check : " + exception.Message,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                finally 
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                MessageBox.Show("SQL Database has been successfully alterated");
            }

        }


        /// <summary>
        /// Add the attributeGroup Column to cher dgmAttributeTypes, but .. why ?
        /// </summary>
        /// <param name="connection">SQLiteConnection the connection to the SQLite Database</param>
        private void addSqlAttributeGroupColumn(SQLiteConnection connection)
        {
            String sql = "ALTER TABLE dgmAttributeTypes ADD attributeGroup INTEGER DEFAULT 0;";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();

            sql = "UPDATE dgmAttributeTypes SET attributeGroup = 0;";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();

            String attributeFile = EveHQSharp.SDEConverter.Properties.Resources.attributeGroups.Replace("\r\n", ((char)13).ToString());
            String[] lines = attributeFile.Split((char)13);

            foreach (String line in lines)
            {
                if (line.StartsWith("attributeID", StringComparison.Ordinal) == false && line != "") 
                {
                    String[] fields = line.Split(",".ToCharArray());
                    sql = "UPDATE dgmAttributeTypes SET attributeGroup=" + fields[1] + " WHERE attributeID =" + fields[0] + ";";
                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Correct some Eve Unit in the DB there unitID is null
        /// </summary>
        /// <param name="connection">SQLiteConnection the connection to the SQLite Database</param>
        private void correctSqlEveUnits(SQLiteConnection connection)
        {
            String sql = "UPDATE dgmAttributeTypes SET unitID=122 WHERE unitID IS NULL;";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        #endregion


    }
}
